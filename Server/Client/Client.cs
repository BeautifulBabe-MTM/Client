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

                StringBuilder stringBuilder = new StringBuilder();

                int bytes = 0;
                byte[] data = new byte[256];
                socket.Connect(iPEndPoint);
                string s = Console.ReadLine();
                socket.Send(File.ReadAllBytes($"{s}.txt"));

                do
                {
                    bytes = socket.Receive(data);
                    stringBuilder.Append(Encoding.Unicode.GetString(data));
                } while (socket.Available > 0);
                if (stringBuilder.ToString().StartsWith("GOOD"))
                {
                    Console.WriteLine("Enter full path to file");
                    string size = String.Empty;
                    string path = Console.ReadLine();
                    size = new FileInfo(path).Length.ToString();
                    data = File.ReadAllBytes(path);
                    socket.Send(data);
                }

                socket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

