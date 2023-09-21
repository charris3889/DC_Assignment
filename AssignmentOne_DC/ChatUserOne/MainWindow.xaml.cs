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
            addChatControl.creationAttempt += checkChatroomCreateAttempt;

        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            foob.sendMessage(user, user.CurrentChatroom, MessageArea.Text);
            MessageArea.Text = "";
            MessagesListView.ItemsSource = foob.ReceiveMessage(user.CurrentChatroom);

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
                    loginControl.Visibility = Visibility.Hidden;  // Make loginControl hidden here
                    addChatControl.Visibility = Visibility.Visible;  // Make addChatControl visible here
                }
            }

        }



        private void ChatsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selChatRoom = (string)ChatsListView.SelectedItem;

            if (selChatRoom != null)
            {
                foob.enterChatroom(user, selChatRoom);
                user.CurrentChatroom = selChatRoom; 
                MessagesListView.ItemsSource = foob.ReceiveMessage(selChatRoom);
                MyGridView.Columns[0].Header = "Chatroom: " + selChatRoom;
            }
        }

        public void checkChatroomCreateAttempt(Object sender, EventArgs e)
        {
            string chatname = addChatControl.ChatnameText;

            if (!foob.HasChatroom(chatname))
            {
                foob.createChatroom(addChatControl.ChatnameText);
                ChatsListView.ItemsSource = foob.forDisplayChatrooms();
                addChatControl.Visibility = Visibility.Hidden;
            }
        }

        private void AddChatroom_Click(object sender, RoutedEventArgs e)
        {
            addChatControl.Visibility = Visibility.Visible;
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            loginControl.Visibility = Visibility.Visible;
        }
    }
}
