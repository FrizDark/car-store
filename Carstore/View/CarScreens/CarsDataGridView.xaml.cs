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
using Carstore.Extensions;

namespace Carstore.View.CarScreens
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
        private List<CarProposition> _propositions;

        private Grid _dgGrid;
        private CarInfoView _infoView;

        private Action _updateNotifications;

        public CarsDataGridView(Action updateNotifications)
        {
            InitializeComponent();

            _dgGrid = DGGrid;
            _updateNotifications = updateNotifications;

            ResetButton_Click(null, null);

        }

        public void UpdateList()
        {
            ResetButton_Click(null, null);
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
            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                _propositions = db.CarProposition.ToList();
                dg.ItemsSource = _propositions
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
            MarkBox.ItemsSource = _marks.SearchInComboBox(ref _markSearch, e.Text[0], 
                b => string.IsNullOrWhiteSpace(_markSearch) || b.Name.Contains(_markSearch));
            MarkSearchBox.Text = _markSearch;
        }

        private void ModelBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ModelBox.ItemsSource = _models.SearchInComboBox(ref _modelSearch, e.Text[0],
                b => string.IsNullOrWhiteSpace(_modelSearch) || b.Name.Contains(_modelSearch));
            ModelSearchBox.Text = _modelSearch;
        }

        private void TypeBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TypeBox.ItemsSource = _types.SearchInComboBox(ref _typeSearch, e.Text[0],
                b => string.IsNullOrWhiteSpace(_typeSearch) || Properties.Resources.ResourceManager.GetString(b.Name).Contains(_typeSearch));
            TypeSearchBox.Text = _typeSearch;
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                try
                {
                    bool isMine = ShowMineBox.IsChecked == true;
                    CarMark mark = MarkBox.SelectedItem as CarMark;
                    CarModel model = ModelBox.SelectedItem as CarModel;
                    CarType type = TypeBox.SelectedItem as CarType;
                    int minPrice = PriceMinBox.Value;
                    int maxPrice = PriceMaxBox.Value;
                    int minPower = PowerMinBox.Value;
                    int maxPower = PowerMaxBox.Value;
                    List<CarProposition> filteredPurposes = db.CarProposition
                        .Include("Car")
                        .Include("Car.CarModel")
                        .Include("Car.CarModel.CarMark")
                        .Include("Car.CarPhoto")
                        .Include("Car.CarPhoto.Photo")
                        .Include("User")
                        .ToList();
                    await Task.Run(() =>
                    {
                        if (isMine)
                        {
                            filteredPurposes = filteredPurposes.Where(c => c.UserId == MainWindow.SelectedUserId).ToList();
                        }
                        if (mark != null)
                        {
                            filteredPurposes = filteredPurposes.Where(c => c.Car.CarModel.MarkId == mark.Id).ToList();
                        }
                        if (model != null)
                        {
                            filteredPurposes = filteredPurposes.Where(c => c.Car.ModelId == model.Id).ToList();
                        }
                        if (type != null)
                        {
                            filteredPurposes = filteredPurposes.Where(c => c.Car.TypeId == type.Id).ToList();
                        }
                        filteredPurposes = filteredPurposes.Where(
                            c => c.Car.Price >= minPrice && c.Car.Price <= maxPrice && c.Car.Power >= minPower && c.Car.Power <= maxPower
                            ).ToList();
                    });
                    dg.ItemsSource = filteredPurposes
                            .Select(c => new CarTableModel(c))
                            .ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg.SelectedItem is CarTableModel purpose && purpose != null)
            {
                _infoView = new CarInfoView(_propositions.First(p => p.Id == purpose.Id), BackButton_Click);
                CarsGrid.Children.Clear();
                CarsGrid.Children.Add(_infoView);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            CarsGrid.Children.Clear();
            CarsGrid.Children.Add(_dgGrid);
            _updateNotifications();
            ResetButton_Click(sender, e);
        }

    }
}
