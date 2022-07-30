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
        private AdminUserEditToolView _adminUserEditToolView;
        private RoutedEventHandler _logoutClick;

        public AppView(RoutedEventHandler logoutClick)
        {
            InitializeComponent();
            _tabControl = StoreTabs;
            _logoutClick = logoutClick;
            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                User user = db.User.Find(MainWindow.SelectedUserId);
                if (user.Photo != null)
                {
                    ProfileAvatar.ImageSource = ByteImage.GetImage(user.Photo.Data);
                }
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings");
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            _profileView = new ProfileView(() =>
            {
                using (CarstoreDBEntities db = new CarstoreDBEntities())
                {
                    User user = db.User.Find(MainWindow.SelectedUserId);
                    if (user.Photo != null)
                    {
                        ProfileAvatar.ImageSource = ByteImage.GetImage(user.Photo.Data);
                    }
                }
            });
            _profileView.LogoutButton.Click += _logoutClick;
            _profileView.EditUsersButton.Click += EditUsersButton_Click;
            PageField.Children.Clear();
            PageField.Children.Add(_profileView);
        }

        private void EditUsersButton_Click(object sender, RoutedEventArgs e)
        {
            _adminUserEditToolView = new AdminUserEditToolView();
            PageField.Children.Clear();
            PageField.Children.Add(_adminUserEditToolView);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            PageField.Children.Clear();
            PageField.Children.Add(_tabControl);
        }

        

    }
}
