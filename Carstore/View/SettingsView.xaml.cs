using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {

        private CultureInfo _culture;

        public SettingsView()
        {
            InitializeComponent();
            _culture = Thread.CurrentThread.CurrentUICulture;
            switch (_culture.Name)
            {
                case "uk":
                    LanguageBox.SelectedIndex = 1;
                    break;
                default:
                    LanguageBox.SelectedIndex = 0;
                    break;
            }
            SaveAndRestartButton.IsEnabled = false;
        }

        private void LanguageBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveAndRestartButton.IsEnabled = true;
            switch (LanguageBox.SelectedIndex)
            {
                case 0:
                    _culture = new CultureInfo("en");
                    break;

                case 1:
                    _culture = new CultureInfo("uk");
                    break;
            }
        }

        private void SaveAndRestartButton_Click(object sender, RoutedEventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(MainWindow.GetAppPath());
            try
            {
                config.AppSettings.Settings.Remove("Language");
            } catch { }
            config.AppSettings.Settings.Add("Language", _culture.Name);
            config.Save(ConfigurationSaveMode.Minimal);
            MainWindow.Restart();
        }

    }
}
