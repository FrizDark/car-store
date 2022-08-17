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
using Carstore.View.Login;
using System.Diagnostics;
using System.Threading;
using System.Globalization;

namespace Carstore
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static int SelectedUserId { get; private set; } = 0;

        private LoginView _loginView;
        private RegisterView _registerView;
        private AppView _appView;

        public MainWindow()
        {
            InitializeComponent();
            MainGrid.Children.Clear();

            Configuration config = ConfigurationManager.OpenExeConfiguration(GetAppPath());
            try
            {
                string value = config.AppSettings.Settings["Language"]?.Value;
                if (value != null) Thread.CurrentThread.CurrentUICulture = new CultureInfo(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            _loginView = new LoginView();
            _loginView.ExitButton.Click += LoginViewExitClick;
            _loginView.LoginButton.Click += LoginViewLoginClick;
            _loginView.RegisterButton.Click += LoginViewRegisterClick;
            
            try
            {
                string value = config.AppSettings.Settings["UserId"]?.Value;
                if (value != null) SelectedUserId = int.Parse(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            EncryptConfigSection("appSettings");
            EncryptConfigSection("connectionStrings");

            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                if (SelectedUserId > 0 && db.User.Find(SelectedUserId) != null)
                {
                    _appView = new AppView(AppLogoutClick);
                    MainGrid.Children.Add(_appView);
                    return;
                }
            }

            MainGrid.Children.Add(_loginView);
        }

        private void AppLogoutClick(object sender, RoutedEventArgs e)
        {
            SelectedUserId = 0;
            Configuration config = ConfigurationManager.OpenExeConfiguration(GetAppPath());
            try
            {
                config.AppSettings.Settings.Remove("UserId");
                config.Save(ConfigurationSaveMode.Minimal);
                EncryptConfigSection("appSettings");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

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
            try
            {
                using (CarstoreDBEntities db = new CarstoreDBEntities())
                {
                    string email = _registerView.EmailBox.Text;
                    string phone = _registerView.PhoneBox.Text;
                    phone = $"({phone.Substring(0, 3)}){phone.Substring(3, 3)}-{phone.Substring(6, 2)}-{phone.Substring(8, 2)}";
                    bool isUsedEmail = false;
                    bool isUsedPhone = false;
                    await Task.Run(() => isUsedEmail = db.User.FirstOrDefault(user => user.Email == email) != null);
                    await Task.Run(() => isUsedPhone = db.User.FirstOrDefault(user => user.Phone == phone) != null);
                    if (isUsedEmail && isUsedPhone)
                    {
                        _registerView.MessageBlock.Text = Properties.Resources.registerView_EmailAndPhoneAreUsed;
                        return;
                    }
                    else if (isUsedEmail)
                    {
                        _registerView.MessageBlock.Text = Properties.Resources.registerView_EmailIsUsed;
                        return;
                    }
                    else if (isUsedPhone)
                    {
                        _registerView.MessageBlock.Text = Properties.Resources.registerView_PhoneIsUsed;
                        return;
                    }
                    db.User.Add(new User
                    {
                        Firstname = _registerView.FirstnameBox.Text,
                        Lastname = _registerView.LastnameBox.Text,
                        Email = _registerView.EmailBox.Text,
                        Phone = phone,
                        Password = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(_registerView.PasswordBox.Password)),
                        TypeId = db.UserType.First(type => type.Name == "userType_User").Id
                    });
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            RegisterViewBackClick(sender, e);
        }

        private void RegisterViewBackClick(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            _loginView.MessageBlock.Text = "";
            MainGrid.Children.Add(_loginView);
        }

        private async void LoginViewLoginClick(object sender, RoutedEventArgs e)
        {
            try
            {
                using (CarstoreDBEntities db = new CarstoreDBEntities())
                {
                    User user = null;
                    string email = _loginView.EmailBox.Text;
                    await Task.Run(() => user = db.User.FirstOrDefault(usr => usr.Email == email));
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
                                EncryptConfigSection("appSettings");
                            }

                            MainGrid.Children.Clear();
                            _appView = new AppView(AppLogoutClick);
                            MainGrid.Children.Add(_appView);
                            return;
                        }
                    }
                    _loginView.MessageBlock.Text = Properties.Resources.loginView_WrongEmailOrPassword;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void LoginViewExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EncryptConfigSection(string sectionName)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(GetAppPath());
            ConfigurationSection section = config.GetSection(sectionName);
            section.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
            section.SectionInformation.ForceSave = true;
            config.Save(ConfigurationSaveMode.Modified);
        }

        public static string GetAppPath()
        {
            System.Reflection.Module[] modules = System.Reflection.Assembly.GetExecutingAssembly().GetModules();
            return modules[0].FullyQualifiedName;
        }

        public static void Restart()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

    }
}
