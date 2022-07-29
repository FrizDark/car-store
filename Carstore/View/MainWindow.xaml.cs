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
using System.Security.Cryptography;
using System.Configuration;

namespace Carstore
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static int SelectedUserId { get; private set; } = 0;

        LoginView _loginView;
        RegisterView _registerView;
        AppView _appView;

        public MainWindow()
        {
            InitializeComponent();
            MainGrid.Children.Clear();
            
            _loginView = new LoginView();
            _loginView.ExitButton.Click += LoginViewExitClick;
            _loginView.LoginButton.Click += LoginViewLoginClick;
            _loginView.RegisterButton.Click += LoginViewRegisterClick;

            Configuration config = ConfigurationManager.OpenExeConfiguration(GetAppPath());
            try
            {
                SelectedUserId = Convert.ToInt32(config.AppSettings.Settings["UserId"].Value);
            }
            catch { }
            EncryptAppSettings();

            _appView = new AppView(AppLogoutClick);
            if (SelectedUserId > 0)
            {
                MainGrid.Children.Add(_appView);
                return;
            }

            MainGrid.Children.Add(_loginView);
        }

        private void AppLogoutClick(object sender, RoutedEventArgs e)
        {
            SelectedUserId = 0;
            Configuration config = ConfigurationManager.OpenExeConfiguration(GetAppPath());
            config.AppSettings.Settings.Remove("UserId");
            config.Save(ConfigurationSaveMode.Minimal);
            EncryptAppSettings();

            MainGrid.Children.Clear();
            MainGrid.Children.Add(_loginView);
        }

        private void LoginViewRegisterClick(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();

            _registerView = new RegisterView();
            _registerView.BackButton.Click += RegisterViewBackClick;
            _registerView.RegisterButton.Click += RegisterViewRegisterClick;

            MainGrid.Children.Add(_registerView);
        }

        private async void RegisterViewRegisterClick(object sender, RoutedEventArgs e)
        {
            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                string email = _registerView.EmailBox.Text;
                string phone = _registerView.PhoneBox.Text;
                phone = $"({phone.Substring(0, 3)}){phone.Substring(3, 3)}-{phone.Substring(6, 2)}-{phone.Substring(8, 2)}";
                bool isUsedEmail = false;
                bool isUsedPhone = false;
                await Task.Run(() => isUsedEmail = db.User.First(user => user.Email == email) != null);
                await Task.Run(() => isUsedPhone = db.User.First(user => user.Phone == phone) != null);
                if (isUsedEmail && isUsedPhone)
                {
                    _registerView.MessageBlock.Text = "Email and phone are used";
                    return;
                } 
                else if (isUsedEmail)
                {
                    _registerView.MessageBlock.Text = "Email is used";
                    return;
                } 
                else if (isUsedPhone)
                {
                    _registerView.MessageBlock.Text = "Phone is used";
                    return;
                }
                db.User.Add(new User
                {
                    Firstname = _registerView.FirstnameBox.Text,
                    Lastname = _registerView.LastnameBox.Text,
                    Email = _registerView.EmailBox.Text,
                    Phone = phone,
                    Password = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(_registerView.PasswordBox.Password)),
                    TypeId = db.UserType.First(type => type.Name == "User").Id
                });
                await db.SaveChangesAsync();
                RegisterViewBackClick(sender, e);
            }
        }

        private void RegisterViewBackClick(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            _loginView.MessageBlock.Text = "";
            MainGrid.Children.Add(_loginView);
        }

        private async void LoginViewLoginClick(object sender, RoutedEventArgs e)
        {
            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                User user = null;
                string email = _loginView.EmailBox.Text;
                await Task.Run(() => user = db.User.First(usr => usr.Email == email));
                byte[] hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(_loginView.PasswordBox.Password));

                if (user != null)
                {
                    bool buf = true;
                    for (int i = 0; i < Math.Min(hash.Length, user.Password.Length); i++)
                    {
                        if (hash[i] != user.Password[i])
                        {
                            buf = false;
                            break;
                        }
                    }
                    if (buf)
                    {
                        SelectedUserId = user.Id;

                        if (_loginView.SavePasswordBox.IsChecked == true)
                        {
                            Configuration config = ConfigurationManager.OpenExeConfiguration(GetAppPath());
                            config.AppSettings.Settings.Remove("UserId");
                            config.AppSettings.Settings.Add("UserId", user.Id.ToString());
                            config.Save(ConfigurationSaveMode.Minimal);
                            EncryptAppSettings();
                        }

                        MainGrid.Children.Clear();
                        _appView = new AppView(AppLogoutClick);
                        MainGrid.Children.Add(_appView);
                        return;
                    }
                }
                _loginView.MessageBlock.Text = "Wrong email or password";
            }
        }

        private void LoginViewExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EncryptAppSettings()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(GetAppPath());
            AppSettingsSection appSettingsSection = (AppSettingsSection)config.GetSection("appSettings");
            appSettingsSection.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
            appSettingsSection.SectionInformation.ForceSave = true;
            config.Save(ConfigurationSaveMode.Modified);
        }

        private string GetAppPath()
        {
            System.Reflection.Module[] modules = System.Reflection.Assembly.GetExecutingAssembly().GetModules();
            return modules[0].FullyQualifiedName;
        }

    }
}
