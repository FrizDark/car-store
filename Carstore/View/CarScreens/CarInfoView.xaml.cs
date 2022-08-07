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

namespace Carstore.View.CarScreens
{
    /// <summary>
    /// Interaction logic for CarInfoView.xaml
    /// </summary>
    public partial class CarInfoView : UserControl
    {

        CarProposition _proposition;
        RoutedEventHandler _backAction;

        public CarInfoView(CarProposition proposition, RoutedEventHandler backAction)
        {
            InitializeComponent();

            _backAction = backAction;
            BackButton.Click += _backAction;

            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                _proposition = db.CarProposition.Find(proposition.Id);
                if (proposition == null) _backAction(null, null);
                CarInformationBlock.Text = $"{_proposition.Car.CarModel.CarMark.Name} {_proposition.Car.CarModel.Name}";
                GalleryList.ItemsSource = _proposition.Car.CarPhoto.Select(p => p.Photo);
                PriceBlock.Text = _proposition.Car.Price.ToString();
                LengthBlock.Text = _proposition.Car.Length.ToString();
                WidthBlock.Text = _proposition.Car.Width.ToString();
                HeightBlock.Text = _proposition.Car.Height.ToString();
                PowerBlock.Text = _proposition.Car.Power.ToString();
                WeightBlock.Text = _proposition.Car.Weight.ToString();
                TankSizeName.Text = _proposition.Car.IsElectrical 
                    ? Properties.Resources.carInfoView_BatteryCapacity
                    : Properties.Resources.carInfoView_TankSize;
                TankSizeBlock.Text = _proposition.Car.TankSize.ToString();
                TankSizeUnits.Text = _proposition.Car.IsElectrical 
                    ? Properties.Resources.carInfoView_BatteryUnits
                    : Properties.Resources.carInfoView_TankUnits;
                SeatsBlock.Text = _proposition.Car.Seats.ToString();
                ColorBlock.Text = _proposition.Car.Color;
                TypeBlock.Text = Properties.Resources.ResourceManager.GetString(_proposition.Car.CarType.Name);
                if (_proposition.User.Photo != null)
                {
                    SellerPhoto.ImageSource = ByteImage.GetImage(_proposition.User.Photo.Data);
                    SellerPhotoField.Visibility = Visibility.Visible;
                }
                SellerName.Text = $"{_proposition.User.Firstname} {_proposition.User.Lastname}";
                DescriptionBlock.Text = _proposition.Car.Description;

                User user = db.User.Find(MainWindow.SelectedUserId);
                if (user == _proposition.User)
                {
                    ContactButton.Visibility = Visibility.Collapsed;
                }
                if (user.UserType.Name == "userType_Admin" || user.UserType.Name == "userType_Moderator" || user == _proposition.User)
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
                    CarId = _proposition.Car.Id,
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
                db.CarProposition.Remove(db.CarProposition.Find(_proposition.Id));
                db.SaveChanges();
            }
            _backAction(sender, e);
        }

    }
}
