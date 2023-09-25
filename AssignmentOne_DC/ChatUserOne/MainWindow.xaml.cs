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
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Drawing.Imaging;
using System.Threading;
using System.Timers;
using System.Net;
using System.Globalization;
//using UserControls.LoginControl;

namespace ChatUserOne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServerInterface foob;
        private User user = null;
        private System.Timers.Timer updateDataTimer;
        private System.Timers.Timer updateMessageTimer; //made global

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

            updateDataTimer = new System.Timers.Timer();
            updateDataTimer.Elapsed += new ElapsedEventHandler(OnDataUpdatePeriod);
            updateDataTimer.Interval = 5000; //Thread Timer activates every 5 seconds
            updateDataTimer.Enabled = false; //disable initially so it doesnt crash when a new client gets hit with the check and their user is still null

            updateMessageTimer = new System.Timers.Timer();
            updateMessageTimer.Elapsed += new ElapsedEventHandler(OnMessageUpdatePeriod);
            updateMessageTimer.Interval = 5000;
            updateMessageTimer.Enabled = false; //disable initially
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            foob.sendMessage(user, user.CurrentChatroom, MessageArea.Text, false);
            MessageArea.Text = "";

           

            MessagesListView.ItemsSource = foob.ReceiveMessage(user.CurrentChatroom);

        }

        public void OnDataUpdatePeriod(object sender, System.Timers.ElapsedEventArgs e)
        {
            //update chatroom list
            //update user list for chatroom
            if (user == null)
                return; //break in case user gets checked when they havent logged in

            Dispatcher.BeginInvoke(new Action(() => {
                ChatsListView.ItemsSource = foob.forDisplayChatrooms(user.Name);
            }));

            if (user != null && user.CurrentChatroom != null)
            {
                Dispatcher.BeginInvoke(new Action(() => {
                    CurrentChatUsersList.ItemsSource = foob.GetUserList(user.CurrentChatroom);
                }));
            }
        }

        public void OnMessageUpdatePeriod(object sender, System.Timers.ElapsedEventArgs e)
        {
            //update chatroom messages
            //update private messages
            if (user == null)
                return; //break in case user gets checked when they havent logged in

            if (user != null && user.CurrentChatroom != null)
            {
                Dispatcher.BeginInvoke(new Action(() => {
                    MessagesListView.ItemsSource = foob.ReceiveMessage(user.CurrentChatroom);
                }));
            }
        }

        public void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {
                string filepath = openFileDialog.FileName;
                string fileExtension = System.IO.Path.GetExtension(filepath).ToLower();
                string folderPath = "UploadedFiles";

                Directory.CreateDirectory(folderPath);

                string fileName = $"{DateTime.Now:yyyyMMddHHmmssfff}{fileExtension}";
                string filePath = System.IO.Path.Combine(folderPath, fileName);

                try
                {
                    File.Copy(filepath, filePath);

                    string relativePath = $"UploadedFiles/{fileName}";
                    string fileLink = $"<a href='{relativePath}' download>Download File: {fileName}</a>";

                    foob.sendMessage(user, user.CurrentChatroom, fileLink, true);
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error saving file: {ex.Message}");
                    return;
                }
            }
        }

        public void checkLoginAttempt(Object sender, EventArgs e)
        {
            if (!foob.hasUser(loginControl.UsernameText))
            {
                user = foob.createUser(loginControl.UsernameText);
                if (user != null)
                {
                    UsernameBox.Text = user.Name;
                    ChatsListView.ItemsSource = foob.forDisplayChatrooms(user.Name);
                    loginControl.Visibility = Visibility.Hidden;  // Make loginControl hidden here
                    addChatControl.Visibility = Visibility.Visible;  // Make addChatControl visible here
                    updateMessageTimer.Enabled = true;
                    updateDataTimer.Enabled = true; //enable timers once users are created
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

        private void ChatCurrUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedUser = (string)CurrentChatUsersList.SelectedItem;
            User otherUser = foob.getUser(selectedUser); //get the user from list

            if (otherUser != null)
            {
                string personalChatroom = foob.createPersonalRoom(user, otherUser);
                foob.enterChatroom(user, personalChatroom); //enter the personal chat
                user.CurrentChatroom = personalChatroom;
                MessagesListView.ItemsSource = foob.ReceiveMessage(personalChatroom);
                MyGridView.Columns[0].Header = "Personal Chat: " + otherUser.Name;
            }
        }

        public void checkChatroomCreateAttempt(Object sender, EventArgs e)
        {
            string chatname = addChatControl.ChatnameText;

            if (!foob.HasChatroom(chatname))
            {
                foob.createChatroom(addChatControl.ChatnameText);
                ChatsListView.ItemsSource = foob.forDisplayChatrooms(user.Name);
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



        private void MessagesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = (Hyperlink)sender;
            string text = hyperlink.DataContext.ToString();

            if (IsHtmlLink(text))
            {
                string url = ExtractUrlFromHtmlLink(text);
                DownloadFile(url);
            }
        }

        private bool IsHtmlLink(string text)
        {
            string pattern = @"<a\s+(?:[^>]*?\s+)?href=(['""])(.*?)\1";

            return System.Text.RegularExpressions.Regex.IsMatch(text, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        private string ExtractUrlFromHtmlLink(string htmlLink)
        {
            string pattern = @"<a\s+(?:[^>]*?\s+)?href=(['""])(.*?)\1";

            System.Text.RegularExpressions.Match match= System.Text.RegularExpressions.Regex.Match(htmlLink, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if ( match.Success)
            {
                return match.Groups[2].Value;
            }

            return string.Empty;
        }

        private void DownloadFile(string url)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.FileName = "DownloadedFile";
            bool? result = saveFileDialog.ShowDialog();

            if (result ==true)
            {
                string filePath = saveFileDialog.FileName;
                using (WebClient webClient = new WebClient())
                {
                    try
                    {
                        webClient.DownloadFile(url, filePath);
                        MessageBox.Show("File successfully downloaded");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error downloading file: " + ex.Message);
                    }
                }
            }
        }
    }

}
