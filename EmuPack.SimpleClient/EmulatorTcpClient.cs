using System.Net.Sockets;
using System.Text;

namespace EmuPack.SimpleClient
{
    public class EmulatorTcpClient
    {
        private readonly string _serverIp;
        private readonly int _serverPort;
        private readonly double _timeoutInSeconds;

        public EmulatorTcpClient(string serverIp, int serverPort, double timeoutInSeconds)
        {
            _serverIp = serverIp;
            _serverPort = serverPort;
            _timeoutInSeconds = timeoutInSeconds;
        }

        public async Task<string> SendRequestAsync(string request)
        {
            using var client = new TcpClient(_serverIp, _serverPort);

            using var stream = client.GetStream();

            byte[] requestBytes = Encoding.ASCII.GetBytes(request);
            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
            var responseBuffer = new byte[1024];
            var task = stream.ReadAsync(responseBuffer, 0, responseBuffer.Length);
            var delayValue = TimeSpan.FromSeconds(_timeoutInSeconds);

            if (await Task.WhenAny(task, Task.Delay(delayValue)) == task)
            {
                string response = Encoding.ASCII.GetString(responseBuffer, 0, task.Result);
                return response;
            }
            else
            {
                throw new TimeoutException("Request timed out.");
            }
        }
    }
}