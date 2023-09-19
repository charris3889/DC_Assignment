using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UserDLL;


namespace ChatServerDLL
{
    public class Database
    {
        List<Chatroom> chatrooms;
        HashSet<User> users;

        public Database() {
            chatrooms = new List<Chatroom>();
            users = new HashSet<User>();
        }

        public void AddChatroom(string chatroom) //chatroom creation
        {
            Chatroom newChat = new Chatroom(chatroom);
            chatrooms.Add(newChat);
        }

        public void RemoveChatroom(Chatroom toRemChatroom) //chatroom deletion
        {
            foreach(Chatroom c in chatrooms)
            {
                if(toRemChatroom == c)
                {

                    chatrooms.Remove(c);
                }
            }
        }

        public void AssignChatroom(User user, string chatroom)
        {
            foreach (Chatroom c in chatrooms)
            {
                if (c.getChatName().Equals(chatroom))
                {
                    user.setCurrentChat(c);
                    c.addUserToChat(user);
                }
            }
        }


        public void AddUser(string inUser)  //adding user to database
        {
            User newUser = new User(inUser);
            users.Add(newUser);
        }

        public void RemoveUser(string outUser) //removing user from data base
        {
            foreach(User u in users)
            {
                if(u.getName().Equals(outUser))
                {
                    users.Remove(u);
                }
            }
            
        }

        public bool UserExists(string username) //username validation
        {
            foreach(User u in users)
            {
                if (u.getName().Equals(username))
                {
                    return true;
                }
            }
            return false;
        }
        public bool AlreadyCreated(string chatroomName) //personal chat creation validation
        {
            foreach (Chatroom c in chatrooms)
            {
                if (c.getChatName().Equals(chatroomName)){
                    return true;
                }
            }

            return false;
        }

        public void SendUserMessage(User user, string msg) //sending message to user
        {
            foreach(User u in users)
            {
                if(u == user)
                {
                    foreach(Chatroom c in chatrooms)
                    {
                        if(c == user.getCurrentChat())
                        {
                            c.addMessage($"[{DateTime.Now}]: {user.getName()}" + msg);
                        }
                    }
                }
            }
        }

        public List<User> retrieveChatUsers(Chatroom chat)
        {
            foreach(Chatroom c in chatrooms)
            {
                if (c.Equals(chat))
                {
                    return c.getUsers();
                }
            }

            return null;
        }

    }
}
