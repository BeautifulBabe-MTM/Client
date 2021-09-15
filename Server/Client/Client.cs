using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Client
    {

        static string ipAddress = "127.0.0.1";
        static int port = 8000;
        static void Main(string[] args)
        {
            try
            {
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(iPEndPoint);

                string s = Console.ReadLine();

                socket.Send(File.ReadAllBytes($"{s}.txt"));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

