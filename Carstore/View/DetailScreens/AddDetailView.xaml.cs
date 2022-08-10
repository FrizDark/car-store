using Carstore.Extensions;
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

namespace Carstore.View.DetailScreens
{
    /// <summary>
    /// Interaction logic for AddDetailView.xaml
    /// </summary>
    public partial class AddDetailView : UserControl
    {

        private string _typeSearch = "";
        private List<DetailType> _types;

        private Photo _photo = null;

        private Action _backAction;

        public AddDetailView(Action backAction)
        {
            InitializeComponent();

            _backAction = backAction;

            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                _types = db.DetailType.OrderBy(t => t.Name).ToList();
            }
            TypeBox.ItemsSource = _types.ToList();

            ResetButton_Click(null, null);

        }

        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Image File|*.jpeg;*.jpg;*.png;";
            f.Multiselect = false;
            if (f.ShowDialog() == true)
            {
                _photo = new Photo { Data = ByteImage.GetBytes(f.FileName, 800, 800) };
                PhotoBlock.Source = ByteImage.GetImage(_photo.Data);
                PhotoBlock.Visibility = Visibility.Visible;
            }
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = new TextRange(NameBox.Document.ContentStart, NameBox.Document.ContentEnd).Text.Trim();
            NameBorder.BorderBrush = text.Length == 0 ? Brushes.Red : Brushes.LightGreen;
            if (text.Length > 1000)
            {
                int gap = 0;
                while (NameBox.CaretPosition.DeleteTextInRun(-1) == 0)
                {
                    NameBox.CaretPosition = NameBox.CaretPosition.GetPositionAtOffset(--gap);
                }
            }
        }

        private void TypeBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TypeBox.ItemsSource = _types.SearchInComboBox(ref _typeSearch, e.Text[0],
                b => string.IsNullOrWhiteSpace(_typeSearch) || Properties.Resources.ResourceManager.GetString(b.Name).Contains(_typeSearch));
            TypeSearchBox.Text = _typeSearch;
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

        private void CancelButton_Click(object sender, RoutedEventArgs e) => _backAction();

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            NameBox.Document.Blocks.Clear();
            _typeSearch = "";
            TypeSearchBox.Text = "";
            _photo = null;
            PhotoBlock.Source = null;
            PhotoBlock.Visibility = Visibility.Collapsed;
            TypeBox.SelectedItem = null;
            PriceBox.Value = PriceBox.Minimum;
            BrandBox.Text = "";
            DescriptionBox.Document.Blocks.Clear();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {

            using (CarstoreDBEntities db = new CarstoreDBEntities())
            {
                try
                {

                    DetailType type = db.DetailType.Find(((DetailType)TypeBox.SelectedItem).Id);
                    if (type == null) return;

                    Detail detail = new Detail
                    {
                        Name = new TextRange(NameBox.Document.ContentStart, NameBox.Document.ContentEnd).Text,
                        TypeId = type.Id,
                        Brand = BrandBox.Text,
                        Photo = _photo,
                        Price = PriceBox.Value,
                        Description = new TextRange(DescriptionBox.Document.ContentStart, DescriptionBox.Document.ContentEnd).Text
                    };

                    DetailProposition proposition = new DetailProposition
                    {
                        Detail = detail,
                        User = db.User.Find(MainWindow.SelectedUserId),
                        CreationDate = DateTime.Now
                    };
                    db.DetailProposition.Add(proposition);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            _backAction();

        }
    }
}
