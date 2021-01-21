using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public static class Server
    {
        public static List<FileD> Files = new List<FileD>();
        public struct FileD
        {
            public int ID;
            public string FileName;
            public string From;
            public int FileSize;
            public byte[] fileBuffer;
        }
        public static int CountUsers = 0;
        public delegate void UserEvent(string Name,string Role);
        public delegate void BotEvent(string Name);
        public static event UserEvent UserConnected = (Username,Role) =>
        {
            Console.WriteLine($"{Role} {Username} connected.");
            CountUsers++;
            SendGlobalMessage($"Пользователь {Username}, статус {Role} подключился к чату. total users {CountUsers}", "Black");
            SendUserList();
        };
        public static event UserEvent UserDisconnected = (Username,Role) =>
        {
            Console.WriteLine($"User {Username} disconnected.");
            CountUsers--;
            SendGlobalMessage($"Пользователь {Username}, статус {Role} отключился от чата. Total users {CountUsers}","Black");
            SendUserList();
        };
        public static List<User> UserList = new List<User>();
        //public static List<Bot> BotList = new List<Bot>();
        public static Socket ServerSocket;
        public const string Host = "127.0.0.1";
        public const int Port = 49675;
        public static bool Work = true;

        public static FileD GetFileByID(int ID)
        {
            int countFiles = Files.Count;
            for(int i = 0;i < countFiles;i++)
            {
                if (Files[i].ID == ID)
                    return Files[i];
            }
            return new FileD() { ID = 0};
        }
        public static void NewUser(User usr)
        {
            if (UserList.Contains(usr))
                return;
            UserList.Add(usr);          
            UserConnected(usr.Username,usr.Userrole);
        }
        public static void EndUser(User usr)
        {
            if (!UserList.Contains(usr))
                return;
            UserList.Remove(usr);
            usr.End();
            UserDisconnected(usr.Username,usr.Userrole);

        }

        public static User GetUser(string Name)
        {
            for(int i = 0;i < CountUsers;i++)
            {
                if (UserList[i].Username == Name)
                    return UserList[i];
            }
            return null;
        }
        /*public static void NewBot(Bot bot)
        {
            if (BotList.Contains(bot))
                return;
            BotList.Add(bot);
            BotConnected(bot.Botname);
        }
        public static void EndBot(Bot bot)
        {
            if (!BotList.Contains(bot))
                return;
            BotList.Remove(bot);
            bot.End();
            BotDisconnected(bot.Botname);

        }

        public static Bot GetBot(string Name)
        {
            for (int i = 0; i < CountUsers; i++)
            {
                if (BotList[i].Botname == Name)
                    return BotList[i];
            }
            return null;
        }*/
        public static void SendUserList()
        {
            string userList = "#userlist|";

            for(int i = 0;i < CountUsers;i++)
            {
                userList += UserList[i].Userrole +' '+ UserList[i].Username + ",";
            }

            SendAllUsers(userList);
        }
        public static void SendGlobalUpdate(string content, string clr)
        {
            for (int i = 0; i < CountUsers; i++)
            {
                UserList[i].UpdateList(content, clr);
            }
        }
        public static void SendGlobalMessage(string content,string clr)
        {
            Console.WriteLine(content);
            for (int i = 0;i < CountUsers;i++)
            {
                
                UserList[i].SendMessage(content, clr);
            }
        }
        public static void SendAllUsers(byte[] data)
        {
            for(int i = 0;i < CountUsers;i++)
            {
                UserList[i].Send(data);
            }
        }
        public static void SendAllUsers(string data)
        {
            for (int i = 0; i < CountUsers; i++)
            {
                UserList[i].Send(data);
            }
        }


    }
}
