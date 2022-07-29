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
using Carstore.Model;
using Carstore.View;

namespace Carstore.View
{
    /// <summary>
    /// Interaction logic for AppView.xaml
    /// </summary>
    public partial class AppView : UserControl
    {

        private ProfileView _profileView;
        private TabControl _tabControl;
        private RoutedEventHandler _logoutClick;


        public AppView(RoutedEventHandler logoutClick)
        {
            InitializeComponent();
            _tabControl = StoreTabs;
            _logoutClick = logoutClick;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings");
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            _profileView = new ProfileView();
            _profileView.LogoutButton.Click += _logoutClick;
            PageField.Children.Clear();
            PageField.Children.Add(_profileView);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            PageField.Children.Clear();
            PageField.Children.Add(_tabControl);
        }

    }
}
