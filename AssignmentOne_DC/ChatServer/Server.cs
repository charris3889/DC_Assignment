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
        public void createChatroom(string chatname)
        {
            db.AddChatroom(chatname);
        }

        public void createPersonalRoom(User user1, User user2)
        {
            string personalChat = user1.getName() + " " + user2.getName();
            if (!db.AlreadyCreated(personalChat))
            {
                db.AddChatroom(personalChat);
            }
            
            
        }

        public void createUser(string username)
        {
            if (!hasUser(username))
            {
                db.AddUser(username);
                Console.WriteLine($"[{DateTime.Now}] {username} has connected.");
            }
            else
            {
                Console.WriteLine($"[{DateTime.Now}] Username: {username} already in use.");
            }

            
           
        }

        public void enterChatroom(User user, string chatname)
        {
            db.AssignChatroom(user, chatname);

            
        }

        public List<User> getChatroomMembers(Chatroom chatroom)
        {
            return db.retrieveChatUsers(chatroom);
            
        }

        public bool hasUser(string username)
        {
            db.UserExists(username);
            throw new NotImplementedException();
        }

        public List<string> ReceiveMessage(Chatroom chatroom) //we might not need it depending on how well I implemented database
        {
            throw new NotImplementedException();
        }

        public void sendMessage(User user, string message)
        {
            db.SendUserMessage(user, message);
        }
    }
}
