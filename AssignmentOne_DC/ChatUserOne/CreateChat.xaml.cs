using ChatServerDLL;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ChatUserOne
{
    /// <summary>
    /// Interaction logic for CreateChat.xaml
    /// </summary>
    public partial class CreateChat : Page
    {
        private ServerInterface foob;
        private Database db;
        public CreateChat(ServerInterface nfoob, Database ndb)
        {
            InitializeComponent();
            foob = nfoob;
            db = ndb;
        }


        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            db.AddChatroom(ChatNameBox.Text);
        }
    }
}
