using System.Net;
using System.Net.Sockets;
using System.Text;


const string ip = "localhost"; 
const int port = 8080;

IPAddress address = IPAddress.Parse(ip);
IPEndPoint endPoint = new IPEndPoint(address, 8080);

Socket serverSocket = new Socket(
    addressFamily: AddressFamily.InterNetwork,
    socketType: SocketType.Stream,
    protocolType: ProtocolType.Tcp);



serverSocket.Bind(endPoint);
serverSocket.Listen(backlog: 5);

Console.WriteLine("Server started!");


var buffer = new byte[1024];


while (true)
{
    Socket clientSocket = await serverSocket.AcceptAsync();
    Console.WriteLine($"{clientSocket.RemoteEndPoint} connected");
    

    ThreadPool.QueueUserWorkItem(async (socket) =>
    {
        try
        {
            int randomNumber = Random.Shared.Next(12, 40);
            while (true)
            {
                await clientSocket.SendAsync(new byte[] { (byte)randomNumber });
                Thread.Sleep(1000);
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine($"{socket.RemoteEndPoint} disconnected!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"System error");
        }


    }, clientSocket, false);
}

