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
        List<string> messages;
        string chatName;

        public Chatroom(string chatroomName) 
        {
            this.messages = new List<string>();
            this.chatName = chatroomName;
        }
    }
}
