using EmuPack.Models.Commands;
using EmuPack.Models.Machine;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmuPack.Services
{
    public class EmulatedMachine
    {
        private readonly ILogger<EmulatedMachine> _logger;

        private TcpClient _client;
        private NetworkStream _stream;
        private TcpListener _listener;
        private CommandHandler _commandHandler;

        public MachineState MachineState { get; private set; }

        public EmulatedMachine(ILogger<EmulatedMachine> logger)
        {
            _logger = logger;

            _commandHandler = new CommandHandler();
            MachineState = new MachineState();

            _client = new TcpClient();
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);

            Thread acceptConnectionThhread = new Thread(new ThreadStart(AcceptConnection));
            acceptConnectionThhread.Start();
        }

        public void AcceptConnection()
        {
            _listener.Start();
            while (true)
            {
                _client = _listener.AcceptTcpClient();
                _stream = _client.GetStream();
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start();
            }
        }

        public void ReceiveMessage()
        {
            byte[] data = new byte[99999];
            try
            {
                while (true)
                {
                    StringBuilder builder = new StringBuilder();

                    int bytes = 0;
                    do
                    {
                        bytes = _stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.ASCII.GetString(data, 0, bytes));
                    }
                    while (_stream.DataAvailable);

                    string message = builder.ToString();
                    _logger.LogInformation($"Receive command: {message}");
                    string response = GetResponseFromProcessedMessage(message);
                    _stream.Write(Encoding.ASCII.GetBytes(response), 0, response.Length);
                    _logger.LogInformation($"Send command: {response}");
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (_stream != null)
                    _stream.Close();
                if (_client != null)
                    _client.Close();
            }
        }

        public string GetResponseFromProcessedMessage(string message)
        {
            string messageToReturn = _commandHandler.ExecuteCommand(MachineState, message).Response;
            return messageToReturn;
        }
    }
}
