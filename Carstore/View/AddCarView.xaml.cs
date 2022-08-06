using Carstore.Model;
using Microsoft.Win32;
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
    /// Interaction logic for AddCarView.xaml
    /// </summary>
    public partial class AddCarView : UserControl
    {

        private string _markSearch = "";
        private string _modelSearch = "";
        private string _typeSearch = "";

        private List<CarMark> _marks;
        private List<CarModel> _models;
        private List<CarType> _types;

        private List<Photo> _photos = new List<Photo>();

        private Action _backAction;

        public AddCarView(Action backAction)
        {
            InitializeComponent();

            _backAction = backAction;

            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                _marks = db.CarMark.OrderBy(m => m.Name).ToList();
                _types = db.CarType.OrderBy(t => t.Name).ToList();
            }
            MarkBox.ItemsSource = _marks.ToList();
            TypeBox.ItemsSource = _types.ToList();

            ResetButton_Click(null, null);

        }

        private void AddPhotosButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Image File|*.jpeg;*.jpg;*.png;";
            f.Multiselect = true;
            if (f.ShowDialog() == true)
            {
                foreach (string fileName in f.FileNames)
                {
                    _photos.Add(new Photo { Data = ByteImage.GetBytes(fileName, 1920, 1080)});
                }
                GalleryList.ItemsSource = _photos;
            }
        }

        private void GalleryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GalleryList.SelectedItem is Photo photo && photo != null)
            {
                _photos.Remove(photo);
                GalleryList.ItemsSource = _photos;
            }
            GalleryList.SelectedItem = null;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            _markSearch = "";
            _modelSearch = "";
            _typeSearch = "";
            MarkSearchBox.Text = "";
            ModelSearchBox.Text = "";
            TypeSearchBox.Text = "";
            _photos.Clear();
            GalleryList.ItemsSource = null;
            MarkBox.SelectedItem = null;
            ModelBox.SelectedItem = null;
            ModelBox.IsEnabled = false;
            TypeBox.SelectedItem = null;
            PriceBox.Value = PriceBox.Minimum;
            ColorBox.Text = "";
            PowerBox.Value = PowerBox.Minimum;
            WeightBox.Value = WeightBox.Minimum;
            SeatsBox.Value = SeatsBox.Minimum;
            LengthBox.Value = LengthBox.Minimum;
            WidthBox.Value = WidthBox.Minimum;
            HeightBox.Value = HeightBox.Minimum;
            IsElectricalBox.IsChecked = false;
            TankSizeBlock.Text = "Tank size:";
            TankSizeBox.Value = TankSizeBox.Minimum;
            DescriptionBox.Document.Blocks.Clear();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {

            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                try
                {

                    CarType type = db.CarType.Find(((CarType)TypeBox.SelectedItem).Id);
                    CarModel model = db.CarModel.Find(((CarModel)ModelBox.SelectedItem).Id);
                    if (type == null || model == null) return;

                    Car car = new Car
                    {
                        CarType = type,
                        CarModel = model,
                        Price = PriceBox.Value,
                        Color = ColorBox.Text,
                        IsElectrical = IsElectricalBox.IsChecked == true,
                        Description = new TextRange(DescriptionBox.Document.ContentStart, DescriptionBox.Document.ContentEnd).Text,
                        Length = LengthBox.Value,
                        Width = WidthBox.Value,
                        Height = HeightBox.Value,
                        Weight = WeightBox.Value,
                        Power = PowerBox.Value,
                        TankSize = TankSizeBox.Value,
                        Seats = SeatsBox.Value,
                        CarPhoto = _photos.Select(p => new CarPhoto { Photo = p }).ToList()
                    };

                    CarProposition proposition = new CarProposition
                    {
                        Car = car,
                        User = db.User.Find(MainWindow.SelectedUserId),
                        CreationDate = DateTime.Now
                    };
                    db.CarProposition.Add(proposition);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            _backAction();

        }

        private void MarkBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                if (MarkBox.SelectedItem is CarMark carMark)
                {
                    CarMark mark = db.CarMark.Find(carMark.Id);
                    if (mark != null)
                    {
                        _modelSearch = "";
                        _models = mark.CarModel.OrderBy(m => m.Name).ToList();
                        ModelBox.ItemsSource = _models;
                        ModelBox.IsEnabled = true;
                    }
                }
            }
        }

        private void MarkBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            MarkBox.ItemsSource = Search(_marks, ref _markSearch, e.Text[0],
                b => string.IsNullOrWhiteSpace(_markSearch) || b.Name.Contains(_markSearch));
            MarkSearchBox.Text = _markSearch;
        }

        private void ModelBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ModelBox.ItemsSource = Search(_models, ref _modelSearch, e.Text[0],
                b => string.IsNullOrWhiteSpace(_modelSearch) || b.Name.Contains(_modelSearch));
            ModelSearchBox.Text = _modelSearch;
        }

        private void TypeBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TypeBox.ItemsSource = Search(_types, ref _typeSearch, e.Text[0],
                b => string.IsNullOrWhiteSpace(_typeSearch) || b.Name.Contains(_typeSearch));
            TypeSearchBox.Text = _typeSearch;
        }

        private List<T> Search<T>(List<T> items, ref string search, char c, Func<T, bool> searchAction)
        {
            if (c == 8)
            {
                if (search.Length > 0)
                    search = search.Remove(search.Length - 1);
            }
            else
            {
                search += c;
            }
            return items.Where(searchAction).ToList();
        }

        private void DescriptionBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = new TextRange(DescriptionBox.Document.ContentStart, DescriptionBox.Document.ContentEnd).Text.Trim();
            DescriptionBorder.BorderBrush = text.Length == 0 ? Brushes.Red : Brushes.LightGreen;
            if (text.Length > 1000)
            {
                int gap = 0;
                while (DescriptionBox.CaretPosition.DeleteTextInRun(-1) == 0)
                {
                    DescriptionBox.CaretPosition = DescriptionBox.CaretPosition.GetPositionAtOffset(--gap);
                }
            }
        }

        private void IsElectricalBox_Checked(object sender, RoutedEventArgs e)
        {
            TankSizeBlock.Text = "Battery capability:";
        }

        private void IsElectricalBox_Unchecked(object sender, RoutedEventArgs e)
        {
            TankSizeBlock.Text = "Tank size:";
        }

    }
}
