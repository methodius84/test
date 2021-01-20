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
        public override string ToString()
        {
            return $"{_userRole} {_userName}";
        }
        public string Username
        {
            get { return _userName; }
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
            _userRole = "user";
            Server.NewUser(this);

            AuthSuccess = true;
            return true;
        }
        private void handleCommand(string cmd)
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
                    if(currentCommand.Contains("yy"))
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
                        Server.SendGlobalMessage($"[{_userName}]: {Arguments[1]}","Black");

                        continue;
                    }
                    if(currentCommand.Contains("endsession"))
                    {
                        Server.EndUser(this);
                        return;
                    }
                    if(currentCommand.Contains("sendfileto"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        string TargetName = Arguments[1];
                        int FileSize = int.Parse(Arguments[2]);
                        string FileName = Arguments[3];
                        byte[] fileBuffer = new byte[FileSize];
                        _userHandle.Receive(fileBuffer);
                        User targetUser = Server.GetUser(TargetName);
                        if(targetUser == null)
                        {
                            SendMessage($"Пользователь {FileName} не найден!","Black");
                            continue;
                        }
                        Server.FileD newFile = new Server.FileD()
                        {
                            ID = Server.Files.Count+1,
                            FileName = FileName,
                            From = Username,
                            fileBuffer = fileBuffer,
                            FileSize = fileBuffer.Length
                        };
                        Server.Files.Add(newFile);
                        targetUser.SendFile(newFile,targetUser);
                    }
                    if(currentCommand.Contains("private"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        string TargetName = Arguments[1];
                        string Content = Arguments[2];
                        User targetUser = Server.GetUser(TargetName);
                        if(targetUser == null)
                        {
                            SendMessage($"Пользователь {TargetName} не найден!","Red");
                            continue;
                        }
                        SendMessage($"-[Отправлено][{TargetName}]: {Content}","Black");
                        targetUser.SendMessage($"-[Получено][{Username}]: {Content}","Black");
                        continue;
                    }
                    if (currentCommand.Contains("setname"))
                    {
                        Console.Write($"{_userName} changing nickname:");
                        
                        string[] Arguments = currentCommand.Split('|');
                        if (setName(Arguments[1]))
                        {
                            //Console.WriteLine($"{_userName}");
                            Send($"#newname#userlist|{_userName}");
                        }
                        else Send("#setnamefailed");
                        continue;
                    }
                    if (currentCommand.Contains("ban_tinkera"))
                    {
                        bool c=false;
                        for (int j = 0; j < Server.UserList.Count; j++)
                        {
                            if (Server.UserList[j].Userrole.Contains("admin"))
                                {
                                SendMessage($"sosi, u nas est' admin", "Red");
                                c = true;
                            }
                        }
                        if (c==false)
                        {
                            //this._userName = $"admin {_userName}";
                            this._userRole = "admin";
                            Send($"#newname#userlist|{_userName}");
                        }
                        
                        continue;
                    }
                    if (currentCommand.Contains("kick"))
                    {
                        if (this._userName.Contains("admin"))
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
                    /*if (currentCommand.Contains("bot_enter")
                    {
                        Ser
                    }*/
                    if (currentCommand.Contains("davai"))
                    {
                        Server.SendUserList();
                    }
                    continue;
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
            Console.WriteLine(content);
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
