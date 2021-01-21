/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class Bot
    {
        private Thread _botThread;
        //private string _botRole;
        private string _botName;
        //private bool AuthSuccess = false;
        private Socket _botHandle;
        private bool BotAuth = false;
        public Bot(Socket handle)
        {
            _botHandle = handle;
            _botName = Botname;
            //_botThread = new Thread(botlistner);
            //_botThread.IsBackground = true;
            //_botThread.Start();
        }
        public string Botname { get; set; } = "Moobot";
        /*public void botlistner()
        {
            try
            {
                while (_botHandle.Connected)
                {
                    byte[] buffer = new byte[2048];
                    int bytesReceive = _botHandle.Receive(buffer);
                    handlebotCommand(Encoding.Unicode.GetString(buffer, 0, bytesReceive));
                }
            }
            catch { Server.EndBot(this); }
        }
        private bool setBot(string set)
        {
            if (!set.Contains("bot_enter"))
            {
                return false;
            }  
            return true;
        }
        public void handlebotCommand(string cmd)
        {
            try
            {
                string[] commands = cmd.Split('#');
                int countCommands = commands.Length;
                for (int i = 0; i < countCommands; i++)
                {
                    string currentCommand = commands[i];
                    if (string.IsNullOrEmpty(currentCommand))
                        continue;
                    if (setBot(currentCommand))
                    {
                        BotAuth = true;
                        Bot Bot= new Bot(_botHandle);
                    }
                    if (BotAuth)
                    {
                        Server.SendGlobalMessage($"[{_botName}]: Hello", "Black");
                        //commands for bot here.
                    }
                    continue;
                }

            }
            catch (Exception exp) { Console.WriteLine("Error with handleCommand: " + exp.Message); }
        }
       /* public void Catch(string cmd1)
        {
            handlebotCommand(cmd1);
        }
        public void Send(string Buffer)
        {
            try
            {
                _botHandle.Send(Encoding.Unicode.GetBytes(Buffer));
            }
            catch { }
        }
        public void End()
        {
            try
            {
                _botHandle.Close();
            }
            catch { }

        }

    }
}*/
