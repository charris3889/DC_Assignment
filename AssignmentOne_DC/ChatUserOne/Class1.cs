using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatUserOne
{
    internal class UsernameEventArgs : EventArgs
    {
        string username;

        public UsernameEventArgs(string username)
        {
            this.username = username;
        }
    }
}
