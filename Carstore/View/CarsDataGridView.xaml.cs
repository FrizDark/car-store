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
    /// Interaction logic for CarsDataGridView.xaml
    /// </summary>
    public partial class CarsDataGridView : UserControl
    {

        private string _markSearch = "";
        private string _modelSearch = "";
        private string _typeSearch = "";

        private List<CarMark> _marks;
        private List<CarModel> _models;
        private List<CarType> _types;
        private List<CarPurpose> _purposes;

        private Grid _dgGrid;
        private CarInfoView _infoView;

        public CarsDataGridView()
        {
            InitializeComponent();

            _dgGrid = DGGrid;

            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                _purposes = db.CarPurpose.ToList();
                dg.ItemsSource = _purposes
                    .Select(c => new CarTableModel(c))
                    .ToList();
                _marks = db.CarMark.OrderBy(m => m.Name).ToList();
                _types = db.CarType.OrderBy(t => t.Name).ToList();
                PriceMinBox.Minimum = PriceMinBox.Value = db.Car.Min(c => c.Price);
                PriceMaxBox.Maximum = PriceMaxBox.Value = db.Car.Max(c => c.Price);
                PowerMinBox.Minimum = PowerMinBox.Value = db.Car.Min(c => c.Power);
                PowerMaxBox.Maximum = PowerMaxBox.Value = db.Car.Max(c => c.Power);
            }
            MarkBox.ItemsSource = _marks.ToList();
            TypeBox.ItemsSource = _types.ToList();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            _markSearch = "";
            _modelSearch = "";
            _typeSearch = "";
            MarkSearchBox.Text = "";
            ModelSearchBox.Text = "";
            TypeSearchBox.Text = "";
            MarkBox.SelectedIndex = -1;
            ModelBox.SelectedIndex = -1;
            ModelBox.ItemsSource = null;
            ModelBox.IsEnabled = false;
            _models = null;
            TypeBox.SelectedIndex = -1;
            MarkBox.ItemsSource = _marks.ToList();
            TypeBox.ItemsSource = _types.ToList();
            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                _purposes = db.CarPurpose.ToList();
                dg.ItemsSource = _purposes
                    .Select(c => new CarTableModel(c))
                    .ToList();
                _marks = db.CarMark.OrderBy(m => m.Name).ToList();
                _types = db.CarType.OrderBy(t => t.Name).ToList();
                PriceMinBox.Minimum = PriceMinBox.Value = db.Car.Min(c => c.Price);
                PriceMaxBox.Maximum = PriceMaxBox.Value = db.Car.Max(c => c.Price);
                PowerMinBox.Minimum = PowerMinBox.Value = db.Car.Min(c => c.Power);
                PowerMaxBox.Maximum = PowerMaxBox.Value = db.Car.Max(c => c.Power);
            }
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
            } else
            {
                search += c;
            }
            return items.Where(searchAction).ToList();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                List<CarPurpose> filteredPurposes = db.CarPurpose
                    .Include("Car")
                    .Include("Car.CarModel")
                    .Include("Car.CarModel.CarMark")
                    .Include("Car.CarPhoto")
                    .Include("Car.CarPhoto.Photo")
                    .Include("User")
                    .ToList();
                if (MarkBox.SelectedItem is CarMark mark && mark != null)
                {
                    filteredPurposes = filteredPurposes.Where(c => c.Car.CarModel.MarkId == mark.Id).ToList();
                }
                if (ModelBox.SelectedItem is CarModel model && model != null)
                {
                    filteredPurposes = filteredPurposes.Where(c => c.Car.ModelId == model.Id).ToList();
                }
                if (TypeBox.SelectedItem is CarType type && type != null)
                {
                    filteredPurposes = filteredPurposes.Where(c => c.Car.TypeId == type.Id).ToList();
                }
                int minPrice = PriceMinBox.Value;
                int maxPrice = PriceMaxBox.Value;
                int minPower = PowerMinBox.Value;
                int maxPower = PowerMaxBox.Value;
                filteredPurposes = filteredPurposes.Where(
                    c => c.Car.Price >= minPrice && c.Car.Price <= maxPrice && c.Car.Power >= minPower && c.Car.Power <= maxPower
                    ).ToList();
                dg.ItemsSource = filteredPurposes
                        .Select(c => new CarTableModel(c))
                        .ToList();
            }
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg.SelectedItem is CarTableModel purpose && purpose != null)
            {
                _infoView = new CarInfoView(_purposes.First(p => p.Id == purpose.Id), BackButton_Click);
                CarsGrid.Children.Clear();
                CarsGrid.Children.Add(_infoView);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            CarsGrid.Children.Clear();
            CarsGrid.Children.Add(_dgGrid);
            ResetButton_Click(sender, e);
        }

    }
}
