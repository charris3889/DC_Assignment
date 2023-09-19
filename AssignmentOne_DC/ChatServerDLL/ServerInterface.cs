using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UserDLL;

namespace ChatServerDLL
{
    [ServiceContract]
    public interface ServerInterface
    {
        [OperationContract]
        void sendMessage(User user, string message);
        [OperationContract]
        List<string> ReceiveMessage(Chatroom chatroom); //not sure what to make return type atm
        [OperationContract]
        Boolean hasUser(string username);
        [OperationContract]
        void createUser(string username);
        [OperationContract]
        void createChatroom(string chatname);
        [OperationContract]
        void createPersonalRoom(User user1, User user2);
        [OperationContract]
        void enterChatroom(User user, string chatname);
        [OperationContract]
        List<User> getChatroomMembers(Chatroom chatroom);
    }
}
