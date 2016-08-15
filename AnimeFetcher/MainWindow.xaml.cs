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

namespace AnimeFetcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            ListBoxItem[] Website_LBI = new ListBoxItem[4];
            Website_LBI[0] = AnyAnime_ListBoxItem;
            Website_LBI[1] = AddAnime_ListBoxItem;
            Website_LBI[2] = SkyAnime_ListBoxItem;
            Website_LBI[3] = OkAnime_ListBoxItem;

            AnimeSites page1 = new AnimeSites(Website_LBI);
            MyFrame.Navigate(page1);
        }

       
        private void WebSites_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            
        }

        private void websites_ListBoxItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem[] Website_LBI = new ListBoxItem[4];
            Website_LBI[0] = AnyAnime_ListBoxItem;
            Website_LBI[1] = AddAnime_ListBoxItem;
            Website_LBI[2] = SkyAnime_ListBoxItem;
            Website_LBI[3] = OkAnime_ListBoxItem;

            AnimeSites page1 = new AnimeSites(Website_LBI);
            MyFrame.Navigate(page1);
            try
            {
                Settings_listBoxItem.IsSelected = false;
                About_listBoxItem.IsSelected = false;
            }
            catch (Exception)
            {
                return;
            }
        }

        private void settings_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Settings_listBoxItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Settings_listBoxItem.IsSelected)
            {
                Settings page = new Settings();
                MyFrame.Navigate(page);
            }
            try
            {
                AnyAnime_ListBoxItem.IsSelected = false;
                AddAnime_ListBoxItem.IsSelected = false;
                OkAnime_ListBoxItem.IsSelected = false;
                SkyAnime_ListBoxItem.IsSelected = false;
            }
            catch (Exception)
            {
                return;
            }
        }

        private void myFrame_ContentRendered(object sender, EventArgs e)
        {
            MyFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
        }
    }
}
