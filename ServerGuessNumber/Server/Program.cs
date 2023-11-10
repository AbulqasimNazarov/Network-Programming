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
            int randomNumber = Random.Shared.Next(7, 14);
            while (true)
            {
                var size = await clientSocket.ReceiveAsync(buffer);
                //var size = await clientSocket.SendAsync(buffer);
                if (buffer[0] > (byte)randomNumber)
                {
                    var message = Encoding.Unicode.GetBytes("More!");
                    await clientSocket.SendAsync(message);
                }
                else if (buffer[0] < (byte)randomNumber)
                {
                    var message = Encoding.Unicode.GetBytes("Less!");
                    await clientSocket.SendAsync(message);
                }
                else 
                {
                    var message = Encoding.Unicode.GetBytes("WIN!!!");
                    await clientSocket.SendAsync(message);
                }
                Thread.Sleep(100);
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

