using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
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

                int bytes = 0;
                byte[] data = new byte[256];
                StringBuilder stringBuilder = new StringBuilder();

                do
                {
                    bytes = socket.Receive(data);
                    stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (socket.Available > 0);
                Console.WriteLine(stringBuilder.ToString());

                string nums = String.Empty;

                Console.Write("Enter fisrt value: ");
                int valueOne = int.Parse(Console.ReadLine());

                Console.Write("Enter second value: ");
                int valueTwo = int.Parse(Console.ReadLine());
                nums = $"{valueOne} {valueTwo}";
                data = Encoding.Unicode.GetBytes(nums);

                socket.Send(data);

                Console.WriteLine($"Values \"{nums}\" was send to SERVER [{ipAddress}]!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
