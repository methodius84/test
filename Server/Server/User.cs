using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class User
    {
        private Thread _userThread;
        private string _userRole;
        private string _userName;
        private bool AuthSuccess = false;
        public string Username
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public string Userrole
        {
            get { return _userRole; }
            set { _userRole = value; }
        }
        private Socket _userHandle;
        public User(Socket handle)
        {
            _userHandle = handle;
            _userThread = new Thread(listner);
            _userThread.IsBackground = true;
            _userThread.Start();
        }
        private void listner()
        {
            try
            {
                while (_userHandle.Connected)
                {
                    byte[] buffer = new byte[2048];
                    int bytesReceive = _userHandle.Receive(buffer);
                    handleCommand(Encoding.Unicode.GetString(buffer, 0, bytesReceive));
                    //Bot bot1 = new Bot(_userHandle);
                    //bot1.handlebotCommand(Encoding.Unicode.GetString(buffer, 0, bytesReceive));
                }
            }
            catch { Server.EndUser(this); }
        }
        private bool setName(string Name)
        {
            
            for (int i=0;i<Name.Length;i++)
            {
                    bool c;
                   c = Char.IsLetterOrDigit(Name,i);
                if (c == false||Name.Length>255)
                {
                    return false;
                }
                for (int j=0;j<Server.UserList.Count;j++)
                {
                    if (Name==Server.UserList[j].Username)
                    {
                        return false;
                    }
                }
            }
            _userName = Name;
            if (_userName == "Gaben")
            {
                _userRole = "bot";
            }
            else _userRole = "user";
            Server.NewUser(this);
            AuthSuccess = true;
            return true;
        }
        private string Answer(string current)
        {
            string[] answers = new string[5];
            Random r = new Random();
            if (current.Contains("techies"))
            {
                
                answers[0] = "no god";
                answers[1] = "why tho";
                answers[2] = "ez -25";
                answers[3] = "1 hour game again";
                answers[4] = "amulet ready";
            }
            if (current.Contains("looting money"))
            {
                answers[0] = "looting money";
                answers[1] = "buzz off";
                answers[2] = "dont talk, buy compendium";
                answers[3] = "compendium krutitsya, lavekha mutitsya";
                answers[4] = "checkout steam sales";
            }
            if (current.Contains("donate first"))
            {
                answers[0] = "donate first";
                answers[1] = "more money first";
                answers[2] = "cant say yet";
                answers[3] = "new year";
                answers[4] = "i have enough money now";
            }
            if (current.Contains("go"))
            {
                answers[0] = "Борат 2";
                answers[1] = "Дом с паранормальными явлениями";
                answers[2] = "Человеческая многоножка(обе части)";
                answers[3] = "50 оттенков черного";
                answers[4] = "зачем фильмы, есть есть BadComedian";
            }
            if (current.Contains("talk"))
            {
                answers[0] = "Black Lives Matter!!!";
                answers[1] = "Джо Байден наш президент";
                answers[2] = "Владимир Путин молодец, построил он себе дворец";
                answers[3] = "нашли новый штамп covid-19, но я подожду pro версию";
                answers[4] = "Сессия. Душно. Откройте форточку.";
            }
            return answers[r.Next(0, 5)];
        }
        private void handleCommand(string cmd)
        {
            Console.WriteLine("Recieved the message "+_userRole + " "+_userName);
            try
            {
                string[] commands = cmd.Split('#');
                int countCommands = commands.Length;
                if (_userRole == "bot")
                {
                    for (int i = 0; i < countCommands; i++)
                    {
                        string currentCommand = commands[i];
                        if (currentCommand.Contains("techies"))
                        {
                            Server.SendGlobalMessage($"{_userName}: {Answer(currentCommand)}", "Red");
                        }
                        if (currentCommand.Contains("looting money"))
                        {
                            Server.SendGlobalMessage($"{_userName}: {Answer(currentCommand)}", "Red");
                        }
                        if (currentCommand.Contains("donate first"))
                        {
                            Server.SendGlobalMessage($"{_userName}: {Answer(currentCommand)}", "Red");
                        }
                        if (currentCommand.Contains("go"))
                        {
                            Server.SendGlobalMessage($"{_userName}: {Answer(currentCommand)}", "Red");
                        }
                        if (currentCommand.Contains("talk"))
                        {
                            Server.SendGlobalMessage($"{_userName}: {Answer(currentCommand)}", "Red");
                        }
                    }
                }
                else 
                {
                    for (int i = 0; i < countCommands; i++)
                        {
                        string currentCommand = commands[i];
                        if (string.IsNullOrEmpty(currentCommand))
                            continue;

                        if (!AuthSuccess)
                        {
                            if (currentCommand.Contains("setname"))
                            {
                                if (setName(currentCommand.Split('|')[1]))
                                    Send("#setnamesuccess");
                                else
                                    Send("#setnamefailed");
                            }
                            continue;
                        }
                        if (currentCommand.Contains("yy"))
                        {
                            string id = currentCommand.Split('|')[1];
                            Server.FileD file = Server.GetFileByID(int.Parse(id));
                            if (file.ID == 0)
                            {
                                SendMessage("Ошибка при передаче файла...", "1");
                                continue;
                            }
                            Send(file.fileBuffer);
                            Server.Files.Remove(file);
                        }
                        if (currentCommand.Contains("message"))
                        {
                            string[] Arguments = cmd.Split('|');
                            if (Arguments[1].Contains("gaben"))
                            { Server.SendGlobalMessage($"{Arguments[1]}", "Black"); }
                            else { Server.SendGlobalMessage($"[{_userName}]: {Arguments[1]}", "Black"); }
                            continue;
                        }
                        if (currentCommand.Contains("endsession"))
                        {
                            Server.EndUser(this);
                            return;
                        }
                        if (currentCommand.Contains("sendfileto"))
                        {
                            string[] Arguments = currentCommand.Split('|');
                            string TargetName = Arguments[1];
                            int FileSize = int.Parse(Arguments[2]);
                            string FileName = Arguments[3];
                            byte[] fileBuffer = new byte[FileSize];
                            _userHandle.Receive(fileBuffer);
                            User targetUser = Server.GetUser(TargetName);
                            if (targetUser == null)
                            {
                                SendMessage($"Пользователь {FileName} не найден!", "Black");
                                continue;
                            }
                            Server.FileD newFile = new Server.FileD()
                            {
                                ID = Server.Files.Count + 1,
                                FileName = FileName,
                                From = Username,
                                fileBuffer = fileBuffer,
                                FileSize = fileBuffer.Length
                            };
                            Server.Files.Add(newFile);
                            targetUser.SendFile(newFile, targetUser);
                        }
                        if (currentCommand.Contains("private"))
                        {
                            string[] Arguments = currentCommand.Split('|');
                            string TargetName = Arguments[1];
                            string Content = Arguments[2];
                            User targetUser = Server.GetUser(TargetName);
                            if (targetUser == null)
                            {
                                SendMessage($"Пользователь {TargetName} не найден!", "Red");
                                continue;
                            }
                            SendMessage($"-[Отправлено][{TargetName}]: {Content}", "Black");
                            targetUser.SendMessage($"-[Получено][{Username}]: {Content}", "Black");
                            continue;
                        }
                        if (currentCommand.Contains("setname"))
                        {
                            Console.Write($"{_userName} changing nickname:");

                            string[] Arguments = currentCommand.Split('|');
                            Console.WriteLine($"to {Arguments[1]}");
                            if (setName(Arguments[1]))
                            {
                                //Console.WriteLine($"{_userName}");
                                Send($"#newname|{_userName}");
                                //Server.SendUserList();
                            }
                            else Send("#setnamefailed");
                            continue;
                        }
                        if (currentCommand.Contains("ban_tinkera"))
                        {
                            bool c = false;
                            for (int j = 0; j < Server.UserList.Count; j++)
                            {
                                if (Server.UserList[j].Userrole.Contains("admin"))
                                {
                                    SendMessage($"There could be only 1 admin", "Red");
                                    c = true;
                                }
                            }
                            if (c == false)
                            {
                                this._userRole = "admin";
                                Server.SendGlobalMessage($"{this._userName} role switched to {this._userRole}", "blue");
                                Server.SendUserList();
                            }

                            continue;
                        }
                        if (currentCommand.Contains("kick"))
                        {
                            if (this._userRole.StartsWith("admin"))
                            {
                                string[] kick = cmd.Split('|');
                                for (int j = 0; j < Server.UserList.Count; j++)
                                {
                                    if (Server.UserList[j].Username == kick[2])
                                    {
                                        Server.EndUser(Server.UserList[j]);
                                    }
                                    else
                                    {
                                        Console.WriteLine("no users with such nickname");
                                    }
                                }

                            }
                            else Server.SendGlobalMessage("You are not an admin,bruv", "Red");
                        }
                        if (currentCommand.Contains("bot_enter"))
                        {
                            //Server.NewUser(new User(this._userHandle) { Username = "Moobot", Userrole="bot" });
                            //new Bot(handle_bot);
                        }
                        if (currentCommand.Contains("davai"))
                        {
                            Server.SendUserList();
                        }
                        continue;
                    }
                }

            }
            catch (Exception exp) { Console.WriteLine("Error with handleCommand: " + exp.Message); }
        }
        public void SendFile(Server.FileD fd, User To)
        {
            byte[] answerBuffer = new byte[48];
            Console.WriteLine($"Sending {fd.FileName} from {fd.From} to {To.Username}");
            To.Send($"#gfile|{fd.FileName}|{fd.From}|{fd.fileBuffer.Length}|{fd.ID}");
        }
        public void SendMessage(string content,string clr)
        {
            
            Send($"#msg|{content}|{clr}");
        }
        public void UpdateList(string content, string clr)
        {
            Console.WriteLine(content);
            Send($"{content}");
        }
        public void Send(byte[] buffer)
        {
            try
            {
                _userHandle.Send(buffer);
            }
            catch { }
        }
        public void Send(string Buffer)
        {
            try
            {
                _userHandle.Send(Encoding.Unicode.GetBytes(Buffer));
            }
            catch { }
        }
        public void End()
        {
            try
            {
                _userHandle.Close();
            }
            catch { }

        }
    }
    
}
