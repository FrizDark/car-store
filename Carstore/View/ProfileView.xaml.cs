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
using System.IO;
using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Carstore.View
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {

        private string _profilePhotoPath = null;
        public ProfileView()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                User user = db.User.First(usr => usr.Id == MainWindow.SelectedUserId);
                if (user.Photo != null)
                {
                    ProfilePhoto.Source = LoadImage(user.Photo.Data);
                } else
                {
                    ProfilePhoto.Source = null;
                }
                FirstnameBlock.Text = user.Firstname;
                LastnameBlock.Text = user.Lastname;
                EmailBlock.Text = user.Email;
                PhoneBlock.Text = user.Phone;

                switch (user.UserType.Name)
                {
                    case "User": break;
                    case "Moderator":
                        RoleBlock.Text = "Moderator";
                        break;
                    case "Admin":
                        RoleBlock.Text = "Admin";
                        EditUsersButton.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private void EditUsersButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit users");
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            FirstnameText.Visibility = Visibility.Hidden;
            LastnameText.Visibility = Visibility.Hidden;
            EmailText.Visibility = Visibility.Hidden;
            PhoneText.Visibility = Visibility.Hidden;
            AdminButtonsBlock.Visibility = Visibility.Hidden;
            StandardButtonsBlock.Visibility = Visibility.Hidden;

            ProfilePhotoButtonsBlock.Visibility = Visibility.Visible;
            FirstnameBox.Visibility = Visibility.Visible;
            LastnameBox.Visibility = Visibility.Visible;
            EmailBox.Visibility = Visibility.Visible;
            PhoneBox.Visibility = Visibility.Visible;
            PasswordFieldsBlock.Visibility = Visibility.Visible;
            EditButtonsBlock.Visibility = Visibility.Visible;

            FirstnameBox.Text = FirstnameBlock.Text;
            LastnameBox.Text = LastnameBlock.Text;
            EmailBox.Text = EmailBlock.Text;
            PhoneBox.Text = PhoneBlock.Text.Replace("(", "").Replace(")", "").Replace("-", "");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            FirstnameText.Visibility = Visibility.Visible;
            LastnameText.Visibility = Visibility.Visible;
            EmailText.Visibility = Visibility.Visible;
            PhoneText.Visibility = Visibility.Visible;
            AdminButtonsBlock.Visibility = Visibility.Visible;
            StandardButtonsBlock.Visibility = Visibility.Visible;

            ProfilePhotoButtonsBlock.Visibility = Visibility.Hidden;
            FirstnameBox.Visibility = Visibility.Hidden;
            LastnameBox.Visibility = Visibility.Hidden;
            EmailBox.Visibility = Visibility.Hidden;
            PhoneBox.Visibility = Visibility.Hidden;
            PasswordFieldsBlock.Visibility = Visibility.Hidden;
            EditButtonsBlock.Visibility = Visibility.Hidden;
            LoadUserData();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            CancelButton_Click(sender, e);

            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                User user = null;
                await Task.Run(new Action(() => user = db.User.First(usr => usr.Id == MainWindow.SelectedUserId)));

                if (_profilePhotoPath != null)
                {
                    if (user.Photo != null)
                    {
                        db.Photo.Remove(user.Photo);
                    }
                    user.Photo = new Photo
                    {
                        Data = CreateCopy(_profilePhotoPath)
                    };
                }
                user.Firstname = FirstnameBox.Text;
                user.Lastname = LastnameBox.Text;
                user.Email = EmailBox.Text;
                string phone = PhoneBox.Text;
                user.Phone = $"({phone.Substring(0, 3)}){phone.Substring(3, 3)}-{phone.Substring(6, 2)}-{phone.Substring(8, 2)}";
                db.SaveChanges();
            }

            LoadUserData();
        }

        private void SelectProfilePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Image File|*.jpeg;*.jpg;*.png;";
            f.Multiselect = false;
            if (f.ShowDialog() == true)
            {
                _profilePhotoPath = f.FileName;
                ProfilePhoto.Source = LoadImage(CreateCopy(_profilePhotoPath));
            }
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
        private byte[] CreateCopy(string fileName)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(fileName);
            int maxWidth = 400, maxHeight = 400;
            double ratioX = (double)maxWidth / img.Width;
            double ratioY = (double)maxHeight / img.Height;
            double ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(img.Width * ratio);
            int newHeight = (int)(img.Height * ratio);

            System.Drawing.Image mi = new Bitmap(newWidth, newHeight);// рисунок в памяти
            Graphics g = Graphics.FromImage(mi);
            g.DrawImage(img, 0, 0, newWidth, newHeight);
            MemoryStream ms = new MemoryStream();// поток для ввода|вывода байт из памяти
            mi.Save(ms, ImageFormat.Jpeg);
            ms.Flush();// выносим в поток все данные из буфера
            ms.Seek(0, SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(ms);
            byte[] buf = br.ReadBytes((int)ms.Length);
            return buf;
        }

        private void ClearProfilePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            _profilePhotoPath = null;
            ProfilePhoto.Source = null;
        }

        private async void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                User user = null;
                await Task.Run(new Action(() => user = db.User.First(usr => usr.Id == MainWindow.SelectedUserId)));
                byte[] hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(OldPasswordBox.Password));

                for (int i = 0; i < Math.Min(hash.Length, user.Password.Length); i++)
                {
                    if (hash[i] != user.Password[i])
                    {
                        MessageBox.Show("Incorrect password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        OldPasswordBox.Password = "";
                        NewPasswordBox.Password = "";
                        RepeatPasswordBox.Password = "";
                        return;
                    }
                }

                user.Password = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(NewPasswordBox.Password));

                OldPasswordBox.Password = "";
                NewPasswordBox.Password = "";
                RepeatPasswordBox.Password = "";
                db.SaveChanges();
            }
        }

        private void OldPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(OldPasswordBox.Password))
            {
                OldPasswordBox.BorderBrush = new SolidColorBrush(Colors.Gray);
            } 
            else
            {
                OldPasswordBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            ActivateChangePasswordButton();
        }

        private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NewPasswordBox.Password))
            {
                NewPasswordBox.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            else
            {
                NewPasswordBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            RepeatPasswordBox_PasswordChanged(sender, e);
        }

        private void RepeatPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(RepeatPasswordBox.Password) && RepeatPasswordBox.Password == NewPasswordBox.Password)
            {
                RepeatPasswordBox.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            else
            {
                RepeatPasswordBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            ActivateChangePasswordButton();
        }

        private void ActivateChangePasswordButton()
        {
            ChangePasswordButton.IsEnabled = !string.IsNullOrEmpty(OldPasswordBox.Password)
                && !string.IsNullOrEmpty(NewPasswordBox.Password)
                && !string.IsNullOrEmpty(RepeatPasswordBox.Password) && RepeatPasswordBox.Password == NewPasswordBox.Password;
        }

        private void PhoneBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text) || (PhoneBox.Text + e.Text).Length > 10;
        }

    }
}
