using System.Net;
using System.Net.Sockets;
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
bool cap = true;
int i = 0;
while (true)
{
    
    var size = await clientSocket.ReceiveAsync(buffer);

    int randomNumber = buffer[0];
    
    
    while (cap)
    {
        Console.WriteLine($"Correct answer: {randomNumber}");
        Console.Write("Answer: ");
        var answerClient = Console.ReadLine();


        if (int.TryParse(answerClient, out answerInt))
        {
            if (randomNumber == answerInt)
            {
                Console.WriteLine("WIN!!!");
                cap = false;
                break;
            }
            else
            {
                i++;
                if (i >= 3)
                {
                    Console.WriteLine("LOSE!");
                    
                    cap = false;
                }
                break;
            }
            
        }
        else
        {
            Console.WriteLine("Incorrect input!");
        }

        
    }
    

   



   

}
