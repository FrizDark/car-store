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
    /// Interaction logic for CarInfoView.xaml
    /// </summary>
    public partial class CarInfoView : UserControl
    {

        CarPurpose _purpose;
        RoutedEventHandler _backAction;

        public CarInfoView(CarPurpose purpose, RoutedEventHandler backAction)
        {
            InitializeComponent();

            _backAction = backAction;
            BackButton.Click += _backAction;

            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                _purpose = db.CarPurpose.Find(purpose.Id);
                if (purpose == null) _backAction(null, null);
                CarInformationBlock.Text = $"{_purpose.Car.CarModel.CarMark.Name} {_purpose.Car.CarModel.Name}";
                GalleryList.ItemsSource = _purpose.Car.CarPhoto.Select(p => p.Photo);
                PriceBlock.Text = _purpose.Car.Price.ToString();
                LengthBlock.Text = _purpose.Car.Length.ToString();
                WidthBlock.Text = _purpose.Car.Width.ToString();
                HeightBlock.Text = _purpose.Car.Height.ToString();
                PowerBlock.Text = _purpose.Car.Power.ToString();
                WeightBlock.Text = _purpose.Car.Weight.ToString();
                TankSizeName.Text = _purpose.Car.IsElectrical ? "Battery" : "Tank";
                TankSizeBlock.Text = _purpose.Car.TankSize.ToString();
                TankSizeUnits.Text = _purpose.Car.IsElectrical ? "kW" : "l";
                SeatsBlock.Text = _purpose.Car.Seats.ToString();
                ColorBlock.Text = _purpose.Car.Color;
                TypeBlock.Text = _purpose.Car.CarType.Name;
                if (_purpose.User.Photo != null)
                {
                    SellerPhoto.ImageSource = ByteImage.GetImage(_purpose.User.Photo.Data);
                    SellerPhotoField.Visibility = Visibility.Visible;
                }
                SellerName.Text = $"{_purpose.User.Firstname} {_purpose.User.Lastname}";
                DescriptionBlock.Text = _purpose.Car.Description;

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
                    CarId = _purpose.Car.Id,
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
                db.CarPurpose.Remove(db.CarPurpose.Find(_purpose.Id));
                db.SaveChanges();
            }
            _backAction(sender, e);
        }

    }
}
