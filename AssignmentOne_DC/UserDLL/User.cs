using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDLL
{
    public class User
    {
        private string name;
        private List<string> currentChat;   
        private Dictionary<string, List<string>> chatRooms; 


        public User(string userName) 
        {
            name = userName;
            currentChat = null;
            chatRooms = new Dictionary<string, List<string>>();
        }


        public void setCurrentChat(List<string> currentChat)    //assign a current chat whenever a groupchat or person is clicked
        {
            this.currentChat = currentChat;
        }

        public void addChatRoom(string chatName, List<String> roomName)     //whenever a chatroom is created
        {
            chatRooms.Add(chatName, roomName);
        }


    }
}
