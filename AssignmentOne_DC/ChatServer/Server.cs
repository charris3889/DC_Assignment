using ChatServerDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDLL;
using System.ServiceModel;
using System.Drawing;

namespace ChatServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false, IncludeExceptionDetailInFaults = true)]
    public class Server : ServerInterface
    {
        private DatabaseSingleton db = new DatabaseSingleton();
        //private Database db = new Database();
        public bool hasUser(string username)
        {
            return db.HasUser(username);
        }

        public User createUser(string username)
        {
            return db.CreateUser(username);
        }

        public bool HasChatroom(string chatroom)
        {
            return db.HasChatroom(chatroom);
        }

        public List<string> GetUserList(string chatroom) 
        {
            return db.GetUserNames(chatroom);
        }

        public void createChatroom(string chatname)
        {
            db.CreateChatroom(chatname);
        }

        public string createPersonalRoom(User user1, User user2)
        {
            return db.getOrCreatePersonalRoom(user1, user2);
        }

        public void enterChatroom(User user, string chatname)
        {
            db.EnterChatroom(user, chatname);
        }

        public void sendMessage(User user, string chatname, string message)
        {
            db.SendMessage(user, chatname, message);
        }

        public List<string> ReceiveMessage(string chatname)
        {
            return db.ReceiveMessage(chatname);
        }

        public List<string> forDisplayChatrooms(string currentUser)
        {
            return db.ForDisplayChatrooms(currentUser);
        }

        public void addTextFiles(string files)
        {
            db.addTextFiles(files);
        }
        public void addImageFiles(Bitmap files)
        {
            db.addImageFiles(files);
        }

        public List<string> getTextFiles()
        {
            return db.textFiles;
        }

        public List<Bitmap> getImageFiles()
        {
            return db.imageFiles;
        }

        public User getUser(string username)
        {
            return db.GetUserByName(username);
        }



    }
}
