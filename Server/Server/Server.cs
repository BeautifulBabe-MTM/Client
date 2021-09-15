using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Server
    {
        static int port = 8000;
        static void Main(string[] args)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Start server...");
            try
            {
                socket.Bind(iPEndPoint);
                socket.Listen(10);

                while (true)
                {
                    Socket socketClient = socket.Accept();

                    int bytes = 0;
                    byte[] data = new byte[256];


                    do
                    {
                        bytes = socketClient.Receive(data);
                    } while (socketClient.Available > 0);
                    socketClient.Send(Encoding.Unicode.GetBytes("GOOD"));
                    bytes = 0; data = new byte[256];
                    List<byte> size = new List<byte>();
                    do
                    {
                        bytes = socketClient.Receive(data);
                        size.AddRange(data);

                    } while (socketClient.Available > 0);

                    File.WriteAllBytes("less.txt", data);

                    File.WriteAllBytes("tolik.jpg", size.ToArray());

                    socketClient.Shutdown(SocketShutdown.Both);
                    socketClient.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}