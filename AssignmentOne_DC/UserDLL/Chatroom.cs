using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UserDLL
{
    [DataContract]
    public class Chatroom
    {
        private List<string> messages;
        private string chatName;
        private List<User> users;

        public Chatroom(string chatroomName) 
        {
            this.messages = new List<string>();
            this.chatName = chatroomName;
            this.users = new List<User>();
        }

        public void addMessage(string message)
        {
            messages.Add(message);
        }

        //public void removeMessage(string message) {  messages.Remove(message); }

        public string getChatName() 
        {  
            return chatName; 
        }
        public void removeUser(string username) //might not be how we do it if they close client
        {
            foreach (User u in users) 
            {
                if (u.Name.Equals(username))
                {
                    users.Remove(u);
                }
            }
        }
        public List<User> getUsers() 
        {
            return users; 
        }

        public void addUserToChat(User user) 
        {
            users.Add(user);
        }
    }
}
