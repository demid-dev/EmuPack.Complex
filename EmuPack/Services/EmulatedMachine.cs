using EmuPack.Models.Commands;
using EmuPack.Models.Machine;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmuPack.Services
{
    public class EmulatedMachine
    {
        private const int END_BYTE_VALUE = 0;

        private readonly ILogger<EmulatedMachine> _logger;
        private readonly CommandHandler _commandHandler;
        private readonly ConcurrentQueue<string> _messagesQueue;

        private TcpClient _client;
        private NetworkStream _stream;
        private TcpListener _listener;

        public MachineState MachineState { get; private set; }

        public EmulatedMachine(ILogger<EmulatedMachine> logger)
        {
            _logger = logger;

            _commandHandler = new CommandHandler();
            _messagesQueue = new ConcurrentQueue<string>();
            MachineState = new MachineState();

            _client = new TcpClient();
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);

            Thread acceptConnectionThhread = new Thread(new ThreadStart(AcceptConnection));
            acceptConnectionThhread.Start();
            Task.Run(async () =>
            {
                await ParseQueue();
            });
        }

        private void AcceptConnection()
        {
            _listener.Start();
            while (true)
            {
                _client = _listener.AcceptTcpClient();
                _stream = _client.GetStream();
                Task.Run(async () =>
                {
                    await ReceiveMessageAsync();
                });
            }
        }

        private async Task ReceiveMessageAsync()
        {
            byte[] data = new byte[1024];
            try
            {
                while (true)
                {
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = await _stream.ReadAsync(data);
                        builder.Append(Encoding.ASCII.GetString(data, 0, bytes));
                    }
                    while (_stream.DataAvailable);

                    string message = builder.ToString();
                    if (bytes == END_BYTE_VALUE) break;
                    _logger.LogInformation($"Receive command: {message}");
                    _messagesQueue.Enqueue(message);
                }
            }
            finally
            {
                if (_stream != null)
                    _stream.Close();
                if (_client != null)
                    _client.Close();
            }
        }

        private async Task ParseQueue()
        {
            try
            {
                while (true)
                {
                    bool dequeued = _messagesQueue.TryDequeue(out string message);
                    if (dequeued)
                    {
                        string response = await GetResponseFromProcessedMessageAsync(message);
                        await _stream.WriteAsync(Encoding.ASCII.GetBytes(response).AsMemory(0, response.Length));
                        _logger.LogInformation($"Send command: {response}");
                    }
                }
            }
            finally
            {
                if (_stream != null)
                    _stream.Close();
                if (_client != null)
                    _client.Close();
            }
        }

        public async Task<string> GetResponseFromProcessedMessageAsync(string message)
        {
            var executedCommand = await _commandHandler.ExecuteCommand(MachineState, message);
            return executedCommand.Response;
        }
    }
}
