using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        void sendMessage(User user, string chatname, string message);
        [OperationContract]
        List<string> ReceiveMessage(string chatroom); //not sure what to make return type atm
        [OperationContract]
        bool hasUser(string username);
        [OperationContract]
        List<string> forDisplayChatrooms();

        [OperationContract]
        User createUser(string username);
        [OperationContract]
        void createChatroom(string chatname);
        [OperationContract]
        void createPersonalRoom(User user1, User user2);
        [OperationContract]
        void enterChatroom(User user, string chatname);
        [OperationContract]
        bool HasChatroom(string chatroom);
        //[OperationContract]
        //List<User> getChatroomMembers(Chatroom chatroom);
        [OperationContract]
        void addTextFiles(string files);
        [OperationContract]
        void addImageFiles(Bitmap files);
        [OperationContract]
        List<string> getTextFiles();
        [OperationContract]
        List<Bitmap> getImageFiles();
    }
}
