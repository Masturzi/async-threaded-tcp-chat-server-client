using System.Net.Sockets;

// Private coding nite challenge output
// Author: Don P
// Date: December 7, 2022

namespace _Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            TcpClient client = new TcpClient("127.0.0.1", 8000);
            NetworkStream stream = client.GetStream();

            while (true)
            {
                Console.Write("Enter Message(type exit to close) > ");
                string message = Console.ReadLine();

                if (message == "exit")
                {
                    break;
                }

                byte[] messageBytes = System.Text.Encoding.ASCII.GetBytes(message);
                await stream.WriteAsync(messageBytes, 0, messageBytes.Length);

                byte[] responseBytes = new byte[1024];
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);

                string response = System.Text.Encoding.ASCII.GetString(responseBytes, 0, bytesRead);
                Console.WriteLine("Response from server: " + response);
            }
        }
    }
}
