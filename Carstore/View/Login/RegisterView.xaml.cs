using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Carstore.View.Login
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password != "")
            {
                PasswordBox.BorderBrush = Brushes.Gray;
            } else
            {
                PasswordBox.BorderBrush = Brushes.Red;
            }
            RepeatBox_PasswordChanged(sender, e);
        }

        private void RepeatBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (RepeatBox.Password != "" && RepeatBox.Password == PasswordBox.Password)
            {
                RepeatBox.BorderBrush = Brushes.Gray;
            }
            else
            {
                RepeatBox.BorderBrush = Brushes.Red;
            }
        }

        private void PhoneBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text) || (PhoneBox.Text + e.Text).Length > 10;
        }
    }
}
