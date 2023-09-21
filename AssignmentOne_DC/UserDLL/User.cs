using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UserDLL
{
    [DataContract]
    public class User
    {
        [DataMember]
        private string name;
        [DataMember]
        private string currentChatroom;

        public User(string userName)
        {
            name = userName;
            currentChatroom = null;
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public string CurrentChatroom { 
            get { return currentChatroom; }
            set { currentChatroom = value; }
        }
    }
}
