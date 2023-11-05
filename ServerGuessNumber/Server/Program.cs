using System.Net;
using System.Net.Sockets;

Socket socket = new Socket(
    addressFamily: AddressFamily.InterNetwork,
    socketType: SocketType.Stream,
    protocolType: ProtocolType.Tcp);


IPAddress address = IPAddress.Parse("192.168.1.7");
IPEndPoint endPoint = new IPEndPoint(null, 8080);
socket.Bind();