using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

const string ip = "localhost";
const int port = 8080;

IPAddress address = IPAddress.Parse(ip);
IPEndPoint endPoint = new IPEndPoint(address, 8080);

Socket clientSocket = new Socket(
    addressFamily: AddressFamily.InterNetwork,
    socketType: SocketType.Stream,
    protocolType: ProtocolType.Tcp);

//Console.WriteLine("Client");
await clientSocket.ConnectAsync(endPoint);
int answerInt = 0;
var buffer = new byte[1024];
var bufferForMessage = new byte[1024];
bool cap = true;
int i = 0;
while (cap)
{
    Console.WriteLine("Guess number from 7 to 14");
    //var size = await clientSocket.ReceiveAsync(buffer);

    
    
    
    while (cap)
    {
        //Console.WriteLine($"Correct answer: {randomNumber}");
        Console.Write("Answer: ");
        var answerClient = Console.ReadLine();


        if (int.TryParse(answerClient, out answerInt))
        {
            buffer[0] = (byte)answerInt;
            await clientSocket.SendAsync(buffer);
            Thread.Sleep(100);
            

        }
        else
        {
            Console.WriteLine("Incorrect input! ONY NUMBER");
        }

        var size = await clientSocket.ReceiveAsync(bufferForMessage);
        string message = Encoding.Unicode.GetString(bufferForMessage);
        Console.WriteLine(message);
        if (message.Contains("WIN!!!"))
        {

            cap = false;
        }
        else
        {
            if (i == 4)
            {
                Console.WriteLine("Lose!");
                cap = false;
            }
            i++;
        }
    }
    

   



   

}
