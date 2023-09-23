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

        public string GetOrCreatePersonalRoom(User user1, User user2) //create personal chatrooms
        {
            if (user1 == null || user2 == null) return null;

            string[] sortedNames = new string[] { user1.Name, user2.Name }.OrderBy(name => name).ToArray();
            string personalRoomName = $"{sortedNames[0]}_{sortedNames[1]}";

            if (!chatrooms.ContainsKey(personalRoomName))
            {
                chatrooms[personalRoomName] = new List<string>();
            }

            return personalRoomName;  //return personal chats
        }


        public void EnterChatroom(User user, string chatname)
        {
            if (user == null || string.IsNullOrEmpty(chatname)) return; //Handle edge cases.

            if (chatrooms.ContainsKey(chatname))
            {
                Console.WriteLine($"[{DateTime.Now}]{user.Name} has entered the chatroom.");
            }
        }

        public void SendMessage(User user, string chatname, string message, bool isHtmlMessage)
        {
            if (user == null || string.IsNullOrEmpty(chatname) || string.IsNullOrEmpty(message)) return; //Handle edge cases.

            if (chatrooms.ContainsKey(chatname))
            {
                if (isHtmlMessage)
                {
                    // If it's an HTML message, you can wrap it in a special HTML tag or add a flag.
                    // Here, we wrap it in <html> tags for simplicity.
                    chatrooms[chatname].Add($"[{DateTime.Now}] --> {user.Name}: <html>{message}</html>");
                }
                else
                {
                    // If it's not an HTML message, add it without any special tags.
                    chatrooms[chatname].Add($"[{DateTime.Now}] --> {user.Name}: {message}");
                }
            }
        }

        public List<string> ReceiveMessage(string chatname, User requestingUser)
        {
            if (string.IsNullOrEmpty(chatname)) return null;

            if (chatrooms.ContainsKey(chatname))
            {
                // Check if it's a personal chatroom
                if (chatname.Contains("_") && !chatname.Split('_').Contains(requestingUser.Name))
                {
                    Console.WriteLine($"[{DateTime.Now}]{requestingUser.Name} tried to access a personal chatroom they don't belong to.");
                    return null;  // Or an empty list if you prefer.
                }
                return chatrooms[chatname];
            }
            return null;
        }

        public List<string> ForDisplayChatrooms(string currentUser)//change to take in username for available chatroom checking
                                                                   //we dont want other users to see other's personal chats
        {
            return chatrooms.Keys.Where(room =>
                !room.Contains("_")
                || room.StartsWith(currentUser + "_")
                || room.EndsWith("_" + currentUser)).ToList();
        }

        public User GetUserByName(string username)
        {
            return users.FirstOrDefault(u => u.Name == username);
        }


    }

}
