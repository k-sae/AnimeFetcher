using System;
using System.Collections.Generic;
using System.IO;
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

namespace AnimeFetcher
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
            string[] settings = StartUp.settings();
            DefaultBrowser_TextBox.Text = settings[0];
        }

        private void DefaultBrowser_Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".exe";
            dlg.Filter = "Excutable Files (*.exe)|*.exe";
            //dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            Nullable <bool> result = dlg.ShowDialog();
            if (result == true)
            {
                DefaultBrowser_TextBox.Text = dlg.FileName;
            }
        }

        private void Save_button_Click(object sender, RoutedEventArgs e)
        {

            string URI = StartUp.Location();
            if (LightTheme_radioButton.IsChecked == true)
            {
                File.WriteAllText(URI + "Settings.anf", DefaultBrowser_TextBox.Text + "\n");
                File.AppendAllText(URI + "Settings.anf", "2\n");

            }
            else if(DarkTheme_radioButton.IsChecked == true)
            {
                File.WriteAllText(URI + "Settings.anf", DefaultBrowser_TextBox.Text + "\n");
                File.AppendAllText(URI + "Settings.anf", "1\n");
            }
            MessageBox.Show("some changes will apply after application restart");
        }

        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            string URI = StartUp.Location();
            DarkTheme_radioButton.IsChecked = true;
            DefaultBrowser_TextBox.Text = "default";
           File.WriteAllText(URI + "Settings.anf", DefaultBrowser_TextBox.Text + "\n");
            File.AppendAllText(URI + "Settings.anf", "1\n");

        }
}}

