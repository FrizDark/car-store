using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Carstore.View.CarScreens;
using Carstore.View.DetailScreens;
using Carstore.View.Profile;

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
        private NotificationsView _notificationsView;
        private RoutedEventHandler _logoutClick;

        public AppView(RoutedEventHandler logoutClick)
        {
            InitializeComponent();
            CarsTab.Content = new CarsDataGridView(UpdateNotifications);
            DetailsTab.Content = new DetailsDataGridView(UpdateNotifications);
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
            Task.Run(() =>
            {
                UpdateNotifications();
                Thread.Sleep(60000);
            });
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
            (CarsTab.Content as CarsDataGridView).UpdateList();
            (DetailsTab.Content as DetailsDataGridView).UpdateList();
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            _notificationsView = new NotificationsView(UpdateNotifications);
            PageField.Children.Clear();
            PageField.Children.Add(_notificationsView);
        }

        private void UpdateNotifications()
        {
            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                int num = db.Notification.Count(
                    n => n.UserToId == MainWindow.SelectedUserId && !n.IsRead);
                Dispatcher.Invoke(() =>
                {
                    NotificationsNumberBox.Text = num.ToString();
                    NotificationsBlock.Visibility = num > 0 ? Visibility.Visible : Visibility.Collapsed;
                });
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddPopup.IsOpen = !AddPopup.IsOpen;
        }

        private void AddPopup_MouseLeave(object sender, MouseEventArgs e)
        {
            AddPopup.IsOpen = false;
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            AddPopup.IsOpen = false;
            PageField.Children.Clear();
            PageField.Children.Add(new AddCarView(() =>
            {
                HomeButton_Click(null, null);
            }));
        }

        private void AddDetailButton_Click(object sender, RoutedEventArgs e)
        {
            AddPopup.IsOpen = false;
            PageField.Children.Clear();
            PageField.Children.Add(new AddDetailView(() =>
            {
                HomeButton_Click(null, null);
            }));
        }

    }
}
