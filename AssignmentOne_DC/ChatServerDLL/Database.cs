using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UserDLL;

namespace ChatServerDLL
{
    internal class Database
    {
        List<Chatroom> chatrooms;
        HashSet<User> users;

        public Database() {
            chatrooms = new List<Chatroom>();
            users = new HashSet<User>();
        }

        public void AddChatroom(Chatroom chatroom)
        {
            chatrooms.Add(chatroom);
        }

        public void RemoveChatroom(Chatroom toRemChatroom)
        {
            foreach(Chatroom c in chatrooms)
            {
                if(toRemChatroom == c)
                {
                    chatrooms.Remove(c);
                }
            }
        }


        public void AddUser(string inUser) 
        {
            User newUser = new User(inUser);
            users.Add(newUser);
        }

        public void RemoveUser(string outUser) 
        {
            foreach(User u in users)
            {
                if(u.getName() == outUser)
                {
                    users.Remove(u);
                }
            }
            
        }

        public void SendUserMessage(User user, string msg)
        {
            foreach(User u in users)
            {
                if(u == user)
                {
                    foreach(Chatroom c in chatrooms)
                    {
                        if(c == user.getCurrentRoom())
                        {
                            c.addMessage($"[{DateTime.Now}]: {user.getName()}" + msg);
                        }
                    }
                }
            }
        }

    }
}
