using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            GuessNumberGame game = new GuessNumberGame();
            Thread.Sleep(1000);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect("127.0.0.1", 49675);
            if (server.Connected)
            {
                string greetingcommand = "#setname|Gaben";
                server.Send(Encoding.Unicode.GetBytes(greetingcommand));
                Console.WriteLine("hello world");
                byte[] data;
                int number;
                while (true)
                {
                    if (server.Available > 0)
                    {
                        data = new byte[256]; // буфер для получаемых данных
                        StringBuilder builder = new StringBuilder();
                        int bytes;
                        bytes = server.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        if (builder.ToString().Contains("techies"))
                        {
                            string message = "";
                            string[] arg = builder.ToString().Split('|');
                            for (int j = 1; j < arg.Length; j++)
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
                        if (builder.ToString().Contains("gaben") || builder.ToString().Contains("габэн"))
                        {
                            if (builder.ToString().Contains("kak dela") || builder.ToString().Contains("как дела"))
                            {
                                msg = "looting money";
                                Console.WriteLine(msg);
                                server.Send(Encoding.Unicode.GetBytes(msg));

                            }
                            if (builder.ToString().Contains("kogda skidki") || builder.ToString().Contains("когда скидки"))
                            {
                                msg = "donate first";
                                Console.WriteLine(msg);
                                server.Send(Encoding.Unicode.GetBytes(msg));
                            }
                            if (builder.ToString().Contains("i need film advice") || builder.ToString().Contains("посоветуй фильм"))
                            {
                                msg = "go";
                                Console.WriteLine(msg);
                                server.Send(Encoding.Unicode.GetBytes(msg));
                            }
                            if (builder.ToString().Contains("what is happening") || builder.ToString().Contains("что происходит"))
                            {
                                msg = "talk";
                                Console.WriteLine(msg);
                                server.Send(Encoding.Unicode.GetBytes(msg));
                            }
                            if (builder.ToString().Contains("хочу сыграть в рулетку") || builder.ToString().Contains("i wanna shoot myself"))
                            {
                                string participant;
                                string[] name = builder.ToString().Split('[', ']');
                                participant = name[1];
                                Console.WriteLine(builder + name[1]);
                                msg = "roulette|" + participant;
                                Console.WriteLine(msg);
                                server.Send(Encoding.Unicode.GetBytes(msg));
                                Thread.Sleep(5000);
                                msg = "start|" + participant;
                                server.Send(Encoding.Unicode.GetBytes(msg));
                                Thread.Sleep(5000);
                                msg = "making shot|" + participant;
                                server.Send(Encoding.Unicode.GetBytes(msg));
                                msg = "result|" + participant;
                                server.Send(Encoding.Unicode.GetBytes(msg));
                            }
                            if (builder.ToString().Contains("как погодка") || builder.ToString().Contains("how's the weather"))
                            {
                                msg = "weather";
                                Console.WriteLine(msg);
                                server.Send(Encoding.Unicode.GetBytes(msg));
                            }
                            if (builder.ToString().Contains("guess"))
                            {
                                msg = "guess my, 1 to 100? enter the number:";
                                game.Start();
                                Console.WriteLine(msg);
                                server.Send(Encoding.Unicode.GetBytes(msg));
                            }
                            if (builder.ToString().Contains("my number is"))
                            {
                                string[] guessgame = messages[1].Split('=');
                                number = Convert.ToInt32(guessgame[1]);
                                Console.WriteLine(number);
                                msg = game.MakeTurn(number);
                                server.Send(Encoding.Unicode.GetBytes(msg));
                            }
                        }
                        Console.WriteLine($"{builder}");
                    }
                }
            }

        }
    }
    public class GuessNumberGame
    {
        public bool IsPlaying;
        int Number;
        public void Start()
        {
            IsPlaying = true;
            Random random = new Random();
            Number = random.Next(1, 100);
            Console.WriteLine("guess my, 1 to 100");
        }
        public void Finish()
        {
            IsPlaying = false;
            Console.WriteLine("finished");
        }
        public string MakeTurn(int message)
        {
            string x = "";
            try
            {
                
                int guess = message;
                if (guess == Number)
                {
                    Console.WriteLine("u were right " + Number);
                    Finish();
                    x = $"u were right {Number}";
                }
                if (guess < Number)
                {
                    Console.WriteLine("mine number is bigger");
                    x = "mine is bigger";
                }
                if (guess > Number)
                {
                    Console.WriteLine("mine number is smaller");
                    x = "mine is smaller";
                }
            }
            catch (FormatException ex)
            { Console.WriteLine("enter a number");
                return "enter a number";
            }
            return x;
        }
    }

}

