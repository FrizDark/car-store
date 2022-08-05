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

        DetailProposition _proposition;
        RoutedEventHandler _backAction;

        public DetailInfoView(DetailProposition proposition, RoutedEventHandler backAction)
        {
            InitializeComponent();

            _backAction = backAction;
            BackButton.Click += _backAction;

            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                _proposition = db.DetailProposition.Find(proposition.Id);
                if (proposition == null) _backAction(null, null);
                PhotoBlock.Source = ByteImage.GetImage(_proposition.Detail.Photo.Data);
                PriceBlock.Text = _proposition.Detail.Price.ToString();
                NameBlock.Text = _proposition.Detail.Name;
                BrandBlock.Text = _proposition.Detail.Brand.ToString();
                TypeBlock.Text = _proposition.Detail.DetailType.Name;
                if (_proposition.Detail.Description != null)
                {
                    DescriptionBlock.Text = _proposition.Detail.Description;
                } else
                {
                    DescRichBox.Visibility = Visibility.Hidden;
                }
                if (_proposition.User.Photo != null)
                {
                    SellerPhoto.ImageSource = ByteImage.GetImage(_proposition.User.Photo.Data);
                    SellerPhotoField.Visibility = Visibility.Visible;
                }
                SellerName.Text = $"{_proposition.User.Firstname} {_proposition.User.Lastname}";

                User user = db.User.Find(MainWindow.SelectedUserId);
                if (user == _proposition.User)
                {
                    ContactButton.Visibility = Visibility.Collapsed;
                }
                if (user.UserType.Name == "Admin" || user.UserType.Name == "Moderator" || user == _proposition.User)
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
                    UserToId = _proposition.User.Id,
                    DetailId = _proposition.Detail.Id,
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
                db.DetailProposition.Remove(db.DetailProposition.Find(_proposition.Id));
                db.SaveChanges();
            }
            _backAction(sender, e);
        }

    }
}
