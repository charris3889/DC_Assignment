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


        public void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {
                string filepath = openFileDialog.FileName;
            

                string fileExtension = System.IO.Path.GetExtension(filepath).ToLower();

                if (fileExtension == ".txt")
                {
                    string fileContent = File.ReadAllText(filepath);
                    foob.addTextFiles(fileContent);

                    List<string> list = foob.getTextFiles();
                    MessageBox.Show(list[0]); 
                }
                else if (IsImageFile(fileExtension))
                {
                    Bitmap bitmap = new Bitmap(filepath);
                    foob.addImageFiles(bitmap);

                    List<Bitmap> list = foob.getImageFiles();
                    Picture.Source = ConvertBitmapToBitmapSource(list[0]); 
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


        private bool IsImageFile(string fileExtension)
        {
            List<string> supportedImageExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff" };

            return supportedImageExtensions.Contains(fileExtension);
        }


        private BitmapSource ConvertBitmapToBitmapSource(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Bmp);
                memoryStream.Seek(0, SeekOrigin.Begin);

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
    }
}
