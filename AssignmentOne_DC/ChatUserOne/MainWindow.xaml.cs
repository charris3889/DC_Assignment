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
        private User user;

        public MainWindow()
        {
            InitializeComponent();

            ChannelFactory<ServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            string URL = "net.tcp://localhost:8100/Server";
            foobFactory = new ChannelFactory<ServerInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();

            loginControl.loginAttempt += checkLoginAttempt;
<<<<<<< HEAD
=======

            addChatControl.Visibility = Visibility.Hidden;
            addChatControl.creationAttempt += checkChatroomCreateAttempt;
>>>>>>> 584bd32e9769edbc4a8227c1734756c5c4815127
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            foob.sendMessage(user, user.CurrentChatroom, MessageArea.Text);
            MessageArea.Text = "";
            MessagesListView.ItemsSource = foob.ReceiveMessage(user.CurrentChatroom);
=======
            foob.sendMessage(this.user, MessageArea.Text);
            List<string> list = new List<string>();
            list.Add(username);
            list.Add("hi");
            ChatsListView.ItemsSource = list;
>>>>>>> 584bd32e9769edbc4a8227c1734756c5c4815127
        }

        public void checkLoginAttempt(Object sender, EventArgs e)
        {
            if (!foob.hasUser(loginControl.UsernameText))
            {
                user = foob.createUser(loginControl.UsernameText);
                if (user != null)
                {
                    UsernameBox.Text = user.Name;
                    ChatsListView.ItemsSource = foob.forDisplayChatrooms();
                    loginControl.Visibility = Visibility.Hidden;
                }
            }
<<<<<<< HEAD
=======
        }

        public void checkChatroomCreateAttempt(Object sender, EventArgs e)
        {
            //if(!foob.hasChatroom(addChatControl.ChatnameBox.Text)) {
            foob.createChatroom(addChatControl.ChatnameBox.Text);
            //}
>>>>>>> 584bd32e9769edbc4a8227c1734756c5c4815127
        }

        private void ChatsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selChatRoom = (string)ChatsListView.SelectedItem;

            if (selChatRoom != null)
            {
                foob.enterChatroom(user, selChatRoom);
                user.CurrentChatroom = selChatRoom; 
                MessagesListView.ItemsSource = foob.ReceiveMessage(selChatRoom);
            }
        }

    }
}
