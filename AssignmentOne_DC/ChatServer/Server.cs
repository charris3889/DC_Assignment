using ChatServerDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDLL;

namespace ChatServer
{
    internal class Server : ServerInterface
    {
        public void createChatroom(string chatname)
        {
            throw new NotImplementedException();
        }

        public void createPersonalRoom(User user1, User user2)
        {
            throw new NotImplementedException();
        }

        public void createUser(string username)
        {
            throw new NotImplementedException();
        }

        public void enterChatroom(User user, string chatname)
        {
            throw new NotImplementedException();
        }

        public List<User> getChatroomMembers(Chatroom chatroom)
        {
            throw new NotImplementedException();
        }

        public bool hasUser(string username)
        {
            throw new NotImplementedException();
        }

        public List<string> ReceiveMessage(Chatroom chatroom)
        {
            throw new NotImplementedException();
        }

        public void sendMessage(User user, string message)
        {
            throw new NotImplementedException();
        }
    }
}
