using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UserDLL
{
    [DataContract]
    public class User
    {
        private string name;
        private Chatroom currentRoom;


        public User(string userName) 
        {
            name = userName;
            currentRoom = null;
        }


        public void setCurrentChat(Chatroom chatroom)    //assign a current chat whenever a groupchat or person is clicked
        {
            currentRoom = chatroom;
        }

        public string getName() { return name; }
        public Chatroom getCurrentChat() {  return currentRoom; }
        
        public void setName(string inName) { this.name = inName; }
    }
}
