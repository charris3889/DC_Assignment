using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserDLL;
using ChatServerDLL;
//using UserControls.LoginControl;

namespace ChatUserOne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServerInterface foob;
        public string username;
        private User user;
        public MainWindow()
        {
            InitializeComponent();

            ChannelFactory<ServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8100/Server";
            foobFactory = new ChannelFactory<ServerInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();

            loginControl.loginAttempt += checkLoginAttempt;
            
        }
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            foob.sendMessage(this.user, MessageArea.Text);
        }

        public void checkLoginAttempt(Object sender, EventArgs e)
        {
            //loginControl.UsernameBox.Text = "success";
            if (!foob.hasUser(UsernameBox.Text))
            {
                foob.createUser(UsernameBox.Text);
                loginControl.Visibility = Visibility.Hidden;
            }
            
        }

        private void ChatsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selChatRoom = (string)ChatsListView.SelectedItem;










            if (selChatRoom != null)
            {
                MessageArea.Text = ($"Now in Chatroom: {selChatRoom}");
            }


        }
    }
        
}
