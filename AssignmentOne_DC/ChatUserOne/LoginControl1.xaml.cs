using System;
using System.CodeDom;
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
using ChatServerDLL;
using ChatUserOne;

namespace ChatUserOne
{
    /// <summary>
    /// Interaction logic for LoginControl1.xaml
    /// </summary>
    public partial class LoginControl1 : UserControl
    {
        public LoginControl1()
        {
            InitializeComponent();
        }

        public event EventHandler loginAttempt;

        public void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            loginAttempt?.Invoke(this, EventArgs.Empty);
            //OnLoginButtonClick(EventArgs.Empty);
        }

        public static readonly DependencyProperty property =
            DependencyProperty.Register("UsernameText", typeof(string), typeof(LoginControl1), new UIPropertyMetadata());

        public string UsernameText
        {
            get { return (string)GetValue(property); }
            set { SetValue(property, UsernameBox.Text); }
        }
    }
}
