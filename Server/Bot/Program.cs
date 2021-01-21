using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Bot
{
    class Program
    {
        static void Main(string[] args)
        {

            Thread.Sleep(5000);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect("127.0.0.1", 49675);
            if (server.Connected)
            {
                string greetingcommand = "#setname|Gaben";
                server.Send(Encoding.Unicode.GetBytes(greetingcommand));
                Console.WriteLine("hello world");
                byte[] data;
                while (true)
                {
                    if (server.Available>0)
                    {
                        data = new byte[256]; // буфер для получаемых данных
                        StringBuilder builder = new StringBuilder();
                        int bytes;
                            bytes = server.Receive(data);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        if (builder.ToString().Contains("techies"))
                        {
                            string message="";
                            string[] arg = builder.ToString().Split('|');
                            for (int j=1;j<arg.Length;j++)
                            {
                                if (arg[j] == "techies")
                                {
                                    arg[j] = "";
                                }
                                message += arg[j];
                            }
                            server.Send(Encoding.Unicode.GetBytes(message));
                        }
                        string[] messages = builder.ToString().Split('|');
                        string msg = "";
                            if (builder.ToString().Contains("gaben"))
                            {
                                if (builder.ToString().Contains("kak dela"))
                                {
                                    msg = "looting money";
                                Console.WriteLine(msg);
                                server.Send(Encoding.Unicode.GetBytes(msg));
                                    
                                }
                                if (builder.ToString().Contains("kogda skidki"))
                                { 
                                    msg = "donate first";
                                Console.WriteLine(msg);
                                server.Send(Encoding.Unicode.GetBytes(msg));
                                }
                            if (builder.ToString().Contains("i need film advice"))
                            {
                                msg = "go";
                                Console.WriteLine(msg);
                                server.Send(Encoding.Unicode.GetBytes(msg));
                            }
                            if (builder.ToString().Contains("what is happening"))
                            {
                                msg = "talk";
                                Console.WriteLine(msg);
                                server.Send(Encoding.Unicode.GetBytes(msg));
                            }
                            }
                        Console.WriteLine($"{builder}");
                    }
                }
            }

            Console.ReadLine();
        }
    }

}

