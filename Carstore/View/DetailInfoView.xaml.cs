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

namespace Carstore.View
{
    /// <summary>
    /// Interaction logic for DetailInfoView.xaml
    /// </summary>
    public partial class DetailInfoView : UserControl
    {

        DetailPurpose _purpose;
        RoutedEventHandler _backAction;

        public DetailInfoView(DetailPurpose purpose, RoutedEventHandler backAction)
        {
            InitializeComponent();

            _backAction = backAction;
            BackButton.Click += _backAction;

            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                _purpose = db.DetailPurpose.Find(purpose.Id);
                if (purpose == null) _backAction(null, null);
                PhotoBlock.Source = ByteImage.GetImage(_purpose.Detail.Photo.Data);
                PriceBlock.Text = _purpose.Detail.Price.ToString();
                NameBlock.Text = _purpose.Detail.Name;
                BrandBlock.Text = _purpose.Detail.Brand.ToString();
                TypeBlock.Text = _purpose.Detail.DetailType.Name;
                if (_purpose.Detail.Description != null)
                {
                    DescriptionBlock.Text = _purpose.Detail.Description;
                } else
                {
                    DescRichBox.Visibility = Visibility.Hidden;
                }
                if (_purpose.User.Photo != null)
                {
                    SellerPhoto.ImageSource = ByteImage.GetImage(_purpose.User.Photo.Data);
                    SellerPhotoField.Visibility = Visibility.Visible;
                }
                SellerName.Text = $"{_purpose.User.Firstname} {_purpose.User.Lastname}";

                User user = db.User.Find(MainWindow.SelectedUserId);
                if (user == _purpose.User)
                {
                    ContactButton.Visibility = Visibility.Collapsed;
                }
                if (user.UserType.Name == "Admin" || user.UserType.Name == "Moderator" || user == _purpose.User)
                {
                    DeleteButton.Visibility = Visibility.Visible;
                }

            }

        }

        private void ContactButton_Click(object sender, RoutedEventArgs e)
        {
            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                db.Notification.Add(new Notification
                {
                    UserFromId = db.User.Find(MainWindow.SelectedUserId).Id,
                    UserToId = _purpose.User.Id,
                    DetailId = _purpose.Detail.Id,
                    Date = DateTime.Now
                });
                db.SaveChanges();
            }
            _backAction(sender, e);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                db.DetailPurpose.Remove(db.DetailPurpose.Find(_purpose.Id));
                db.SaveChanges();
            }
            _backAction(sender, e);
        }

    }
}
