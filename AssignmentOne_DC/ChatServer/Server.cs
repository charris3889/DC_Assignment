using ChatServerDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDLL;
using System.ServiceModel;

namespace ChatServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false, IncludeExceptionDetailInFaults = true)]
    public class Server : ServerInterface
    {
        private Database db = new Database();
        public bool hasUser(string username)
        {
            return db.HasUser(username);
        }

        public User createUser(string username)
        {
            return db.CreateUser(username);
        }

        public void createChatroom(string chatname)
        {
            db.CreateChatroom(chatname);
        }

        public void createPersonalRoom(User user1, User user2)
        {
            db.CreatePersonalRoom(user1, user2);
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

        public List<string> forDisplayChatrooms()
        {
            return db.ForDisplayChatrooms();
        }
    }
}
