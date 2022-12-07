using System.Net;
using System.Net.Sockets;

// Private coding nite challenge output
// Author: Don P
// Date: December 7, 2022

namespace _Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 8000);
            listener.Start();

            Console.WriteLine("Server started. Waiting for clients...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                // thread pool for worker threads.
                Task.Run(() => HandleClientAsync(client, stream));
            }
        }

        static async Task HandleClientAsync(TcpClient client, NetworkStream stream)
        {
            byte[] buffer = new byte[1024];
            int bytesRead;

            while (true)
            {
                bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    break;
                }

                string message = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Message from client: " + message);

                string response = "Message Received: " + message;
                byte[] responseBytes = System.Text.Encoding.ASCII.GetBytes(response);
                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
            }
        }
    }
}
