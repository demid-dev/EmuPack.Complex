using EmuPack.SimpleClient;

Console.ReadKey();
var tcpHttpClient = new EmulatorTcpClient("127.0.0.1", 8888, 100);
string response = await tcpHttpClient.SendRequestAsync("INC1M100000");
Console.WriteLine("Response: " + response);
Console.ReadKey();