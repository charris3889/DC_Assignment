using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ChatServerDLL;

namespace ChatUserOne
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        private ServerInterface foob;
        public LoginPage(ServerInterface nfoob)
        {
            InitializeComponent();
            foob = nfoob;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if(foob.hasUser(UsernameBox.Text) == false)
            {
                foob.createUser(UsernameBox.Text);
            }
            
            this.Close();
        }
    }
}
