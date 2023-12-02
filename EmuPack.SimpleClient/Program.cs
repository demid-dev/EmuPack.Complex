using EmuPack.SimpleClient;

Console.ReadKey();
var tcpClient = new EmulatorTcpClient("127.0.0.1", 8888, 100);
await tcpClient.SendRequestAsync("INC1M100000");
await tcpClient.SendRequestAsync("SRC1M100000");
await tcpClient.SendRequestAsync("MRC1M100005D0000");
await tcpClient.SendRequestAsync("PRC1M100044I00010101Ibuprophen                    00003");
await tcpClient.SendRequestAsync("FLC1M100015P0001A0MC010102");
await tcpClient.SendRequestAsync("FLC1M100015P0001B0MC010101");
await tcpClient.SendRequestAsync("MRC1M100005D0100");
await tcpClient.SendRequestAsync("SRC1M100000");
Console.ReadKey();