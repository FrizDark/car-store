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
using Carstore.View;
using Carstore.Model;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace Carstore.View.Profile
{
    /// <summary>
    /// Interaction logic for AdminUserEditToolView.xaml
    /// </summary>
    public partial class AdminUserEditToolView : UserControl
    {

        private CarstoreDBEntities _db;
        private DbContextTransaction _tran;

        public AdminUserEditToolView()
        {
            InitializeComponent();
            _db = new CarstoreDBEntities();
            if (_db.User.Find(MainWindow.SelectedUserId).UserType.Name != "userType_Admin")
            {
                MessageBox.Show("This is admin tool!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _db.Dispose();
                Application.Current.Shutdown();
            }
            _tran = _db.Database.BeginTransaction();
            ShowUsers();
        }

        private void ShowUsers()
        {
            var users = _db.User
                .Where(u => u.UserType.Name != "userType_Admin")
                .Select(u => new
                {
                    u.Id,
                    u.Firstname,
                    u.Lastname,
                    Role = u.UserType.Name,
                    u.Email
                }).ToList();
            dg.ItemsSource = new Collection<UserRoleModel>(users
                .Select(u => new UserRoleModel(u.Id, $"{u.Firstname} {u.Lastname}", u.Role, u.Email))
                .ToList());
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg.SelectedItem is UserRoleModel user)
            {
                switch (user.Role)
                {
                    case "userType_User":
                        UserButton.IsEnabled = false;
                        ModeratorButton.IsEnabled = true;
                        break;
                    case "userType_Moderator":
                        UserButton.IsEnabled = true;
                        ModeratorButton.IsEnabled = false;
                        break;
                }
            }
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            SaveButton.IsEnabled = true;
            UserButton.IsEnabled = false;
            try
            {
                User user = _db.User.Find((dg.SelectedItem as UserRoleModel).Id);
                if (user != null)
                {
                    user.TypeId = _db.UserType.FirstOrDefault(t => t.Name == "userType_User").Id;
                }
                _db.SaveChanges();
            }
            catch { }
            ShowUsers();
        }

        private void ModeratorButton_Click(object sender, RoutedEventArgs e)
        {
            SaveButton.IsEnabled = true;
            ModeratorButton.IsEnabled = false;
            try
            {
                User user = _db.User.Find((dg.SelectedItem as UserRoleModel).Id);
                if (user != null)
                {
                    user.TypeId = _db.UserType.FirstOrDefault(t => t.Name == "userType_Moderator").Id;
                }
                _db.SaveChanges();
            }
            catch { }
            ShowUsers();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            _tran?.Rollback();
            _tran?.Dispose();
            _db?.Dispose();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _tran.Rollback();
            _tran.Dispose();
            _tran = _db.Database.BeginTransaction();
            SaveButton.IsEnabled = false;
            ShowUsers();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _tran.Commit();
            _tran.Dispose();
            _tran = _db.Database.BeginTransaction();
            SaveButton.IsEnabled = false;
        }

    }
}
