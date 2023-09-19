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
    /// Interaction logic for AddChatroomControl.xaml
    /// </summary>
    public partial class AddChatroomControl : UserControl
    {
        public AddChatroomControl()
        {
            InitializeComponent();
        }

        public event EventHandler creationAttempt;

        public void createButton_Click(object sender, RoutedEventArgs e)
        {
            creationAttempt?.Invoke(this, EventArgs.Empty);
        }

        public static readonly DependencyProperty property =
            DependencyProperty.Register("ChatText", typeof(string), typeof(LoginControl1), new UIPropertyMetadata());

        public string UsernameText
        {
            get { return (string)GetValue(property); }
            set { SetValue(property, ChatnameBox.Text); }
        }

    }
}
