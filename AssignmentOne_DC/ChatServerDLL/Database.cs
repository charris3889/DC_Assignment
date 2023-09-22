using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UserDLL;


namespace ChatServerDLL
{
    public class Database
    {
        public List<User> users; //Made them public cos i cant be bothered making getters and setters
        public Dictionary<string, List<string>> chatrooms;

        public Database()
        {
            users = new List<User>();
            chatrooms = new Dictionary<string, List<string>>();
            chatrooms["sample"] = new List<string>();
        }

        public List<string> GetUserNames(string chatroom)
        {
            List<string> names = new List<string>();

            for(int i = 0; i < users.Count; i++)
            {
                names.Add(users[i].Name);
            }

            return names;
        }
        public bool HasUser(string username)
        {
            return users.Exists(u => u.Name == username);
        }

        public User CreateUser(string username)
        {
            if (string.IsNullOrEmpty(username)) return null; //Handle invalid usernames.

            User newUser = new User(username);
            users.Add(newUser);
            return newUser;
        }

        public bool HasChatroom(string chatroom)
        {
            if(chatrooms.ContainsKey(chatroom))
            {
                return true;
            }

            return false;
        }

        public void CreateChatroom(string chatname)
        {
            if (string.IsNullOrEmpty(chatname)) return; //Handle invalid chat names.

            if (!chatrooms.ContainsKey(chatname))
            {
                chatrooms[chatname] = new List<string>();
            }
        }

        public void CreatePersonalRoom(User user1, User user2)
        {
            if (user1 == null || user2 == null) return; //Handle null users.

            string personalRoomName = $"{user1.Name}_{user2.Name}";
            if (!chatrooms.ContainsKey(personalRoomName))
            {
                chatrooms[personalRoomName] = new List<string>();
            }
        }

        public void EnterChatroom(User user, string chatname)
        {
            if (user == null || string.IsNullOrEmpty(chatname)) return; //Handle edge cases.

            if (chatrooms.ContainsKey(chatname))
            {
                Console.WriteLine($"[{DateTime.Now}]{user.Name} has entered the chatroom.");
            }
        }

        public void SendMessage(User user, string chatname, string message)
        {
            if (user == null || string.IsNullOrEmpty(chatname) || string.IsNullOrEmpty(message)) return; //Handle edge cases.

            if (chatrooms.ContainsKey(chatname))
            {
                chatrooms[chatname].Add($"[{DateTime.Now}] --> {user.Name}: {message}");
            }
        }

        public List<string> ReceiveMessage(string chatname)
        {
            if (string.IsNullOrEmpty(chatname)) return null; //Handle invalid chat names.

            if (chatrooms.ContainsKey(chatname))
            {
                return chatrooms[chatname];
            }
            return null; //Or return an empty list.
        }

        public List<string> ForDisplayChatrooms()
        {
            return new List<string>(chatrooms.Keys);
        }


      
    }

}
