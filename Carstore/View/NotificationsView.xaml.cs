using Carstore.Model;
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

namespace Carstore.View
{
    /// <summary>
    /// Interaction logic for NotificationsView.xaml
    /// </summary>
    public partial class NotificationsView : UserControl
    {

        private enum Filter
        {
            All, FromMe, ToMe
        }

        private List<Notification> _notifications;
        private Filter _filter = Filter.All;

        private Action _updateNotifications;

        public NotificationsView(Action updateNotifications)
        {
            InitializeComponent();
            _updateNotifications = updateNotifications;
            ReloadButton_Click(null, null);
        }

        private void AllButton_Click(object sender, RoutedEventArgs e)
        {
            _filter = Filter.All;
            AllButton.IsEnabled = false;
            FromMeButton.IsEnabled = true;
            ToMeButton.IsEnabled = true;
            ReloadButton_Click(sender, e);
        }

        private void FromMeButton_Click(object sender, RoutedEventArgs e)
        {
            _filter = Filter.FromMe;
            AllButton.IsEnabled = true;
            FromMeButton.IsEnabled = false;
            ToMeButton.IsEnabled = true;
            ReloadButton_Click(sender, e);
        }

        private void ToMeButton_Click(object sender, RoutedEventArgs e)
        {
            _filter = Filter.ToMe;
            AllButton.IsEnabled = true;
            FromMeButton.IsEnabled = true;
            ToMeButton.IsEnabled = false;
            ReloadButton_Click(sender, e);
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                switch (_filter)
                {
                    case Filter.All:
                        _notifications = db.Notification.Where(
                            n => n.UserFromId == MainWindow.SelectedUserId || n.UserToId == MainWindow.SelectedUserId)
                            .ToList();
                        break;
                    case Filter.FromMe:
                        _notifications = db.Notification.Where(
                            n => n.UserFromId == MainWindow.SelectedUserId)
                            .ToList();
                        break;
                    case Filter.ToMe:
                        _notifications = db.Notification.Where(
                            n => n.UserToId == MainWindow.SelectedUserId)
                            .ToList();
                        break;
                }
                dg.ItemsSource = _notifications
                    .Select(n =>
                    {
                        NotificationTableModel model = new NotificationTableModel(n);
                        if (n.UserFromId == MainWindow.SelectedUserId) model.IsRead = true;
                        return model;
                    })
                    .OrderBy(n => n.Date)
                    .OrderBy(n => n.IsRead)
                    .ToList();
            }
            _updateNotifications();
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg.SelectedItem is NotificationTableModel notification
                && notification != null
                && !notification.IsRead)
            {
                using (CarstoreDBEntities db = new CarstoreDBEntities())
                {
                    Notification n = db.Notification.Find(notification.Id);
                    if (n.UserToId == MainWindow.SelectedUserId)
                    {
                        n.IsRead = true;
                        db.SaveChanges();
                        ReloadButton_Click(sender, e);
                    }
                }
            }
        }

    }
}
