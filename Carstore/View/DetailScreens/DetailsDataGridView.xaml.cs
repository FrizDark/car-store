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

namespace Carstore.View.DetailScreens
{
    /// <summary>
    /// Interaction logic for DetailsDataGridView.xaml
    /// </summary>
    public partial class DetailsDataGridView : UserControl
    {

        private string _brandSearch = "";
        private string _typeSearch = "";

        private List<string> _brands;
        private List<DetailType> _types;
        private List<DetailProposition> _propositions;

        private Grid _dgGrid;
        private DetailInfoView _infoView;

        private Action _updateNotifications;

        public DetailsDataGridView(Action updateNotifications)
        {
            InitializeComponent();

            _dgGrid = DGGrid;
            _updateNotifications = updateNotifications;

            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                _propositions = db.DetailProposition.ToList();
                dg.ItemsSource = _propositions
                    .Select(d => new DetailTableModel(d))
                    .ToList();
                _brands = db.Detail.Select(d => d.Brand).Distinct().OrderBy(d => d).ToList();
                _types = db.DetailType.OrderBy(t => t.Name).ToList();
                PriceMinBox.Minimum = PriceMinBox.Value = db.Detail.Min(d => d.Price);
                PriceMaxBox.Maximum = PriceMaxBox.Value = db.Detail.Max(d => d.Price);
            }
            BrandBox.ItemsSource = _brands.ToList();
            TypeBox.ItemsSource = _types.ToList();
        }

        public void UpdateList()
        {
            ResetButton_Click(null, null);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            _brandSearch = "";
            _typeSearch = "";
            NameBox.Text = "";
            BrandSearchBox.Text = "";
            TypeSearchBox.Text = "";
            BrandBox.SelectedIndex = -1;
            TypeBox.SelectedIndex = -1;
            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                _propositions = db.DetailProposition.ToList();
                dg.ItemsSource = _propositions
                    .Select(d => new DetailTableModel(d))
                    .ToList();
                _brands = db.Detail.Select(d => d.Brand).Distinct().OrderBy(d => d).ToList();
                _types = db.DetailType.OrderBy(t => t.Name).ToList();
                PriceMinBox.Minimum = PriceMinBox.Value = db.Detail.Min(d => d.Price);
                PriceMaxBox.Maximum = PriceMaxBox.Value = db.Detail.Max(d => d.Price);
            }
            BrandBox.ItemsSource = _brands.ToList();
            TypeBox.ItemsSource = _types.ToList();
        }

        private void BrandBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            BrandBox.ItemsSource = _brands.SearchInComboBox(ref _brandSearch, e.Text[0],
                b => string.IsNullOrWhiteSpace(_brandSearch) || b.Contains(_brandSearch));
            BrandSearchBox.Text = _brandSearch;
        }

        private void TypeBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TypeBox.ItemsSource = _types.SearchInComboBox(ref _typeSearch, e.Text[0],
                b => string.IsNullOrWhiteSpace(_typeSearch) || b.Name.Contains(_typeSearch));
            TypeSearchBox.Text = _typeSearch;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                List<DetailProposition> filteredPurposes = db.DetailProposition
                    .Include("Detail")
                    .Include("Detail.Photo")
                    .Include("Detail.DetailType")
                    .Include("User")
                    .ToList();
                if (ShowMineBox.IsChecked == true)
                {
                    filteredPurposes = filteredPurposes.Where(d => d.UserId == MainWindow.SelectedUserId).ToList();
                }
                filteredPurposes = filteredPurposes.Where(
                    d => string.IsNullOrWhiteSpace(NameBox.Text) || d.Detail.Name.Contains(NameBox.Text))
                    .ToList();
                if (BrandBox.SelectedItem is string brand && brand != null)
                {
                    filteredPurposes = filteredPurposes.Where(d => d.Detail.Brand == brand).ToList();
                }
                if (TypeBox.SelectedItem is DetailType type && type != null)
                {
                    filteredPurposes = filteredPurposes.Where(d => d.Detail.TypeId == type.Id).ToList();
                }
                int minPrice = PriceMinBox.Value;
                int maxPrice = PriceMaxBox.Value;
                filteredPurposes = filteredPurposes.Where(
                    d => d.Detail.Price >= minPrice && d.Detail.Price <= maxPrice
                    ).ToList();
                dg.ItemsSource = filteredPurposes
                        .Select(d => new DetailTableModel(d))
                        .ToList();
            }
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg.SelectedItem is DetailTableModel purpose && purpose != null)
            {
                _infoView = new DetailInfoView(_propositions.First(p => p.Id == purpose.Id), BackButton_Click);
                DetailsGrid.Children.Clear();
                DetailsGrid.Children.Add(_infoView);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            DetailsGrid.Children.Clear();
            DetailsGrid.Children.Add(_dgGrid);
            _updateNotifications();
            ResetButton_Click(sender, e);
        }

    }
}
