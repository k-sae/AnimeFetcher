using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Interaction logic for AnyAnime.xaml
    /// </summary>
    /// make application save an read eps name on startup
    public partial class AnimeSites : Page
    {
        ListBoxItem[] Website_LBI = new ListBoxItem[4];
        public AnimeSites(ListBoxItem[] Website_temp)
        {
            InitializeComponent();
            StartUp.check();
            string[] LastSession = StartUp.lastsession();

            if (LastSession.Length == 1)
            {
                Status_tb.Text += "\nLast Site Was: ";
                Status_tb.Text += LastSession[0];
                Status_tb.Text += "\n";
            }
            else if (LastSession.Length > 1)
            {
                Status_tb.Text += "\nLast Site Was: ";
                Status_tb.Text += LastSession[0];
                Status_tb.Text += "\n";
                Status_tb.Text += "\nNext Website is: ";
                Status_tb.Text += LastSession[1];
                Status_tb.Text += "\n";
            }
            Website_LBI = Website_temp;
          
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            Status_tb.Text += "\nStarting....\n";
            Grabber mygrabber = new Grabber();
           
            string[] settings = StartUp.settings();
            if (UrlIdentifier(AnimeUrl.Text) == 1)
            {
                mygrabber.Grab(AnimeUrl.Text, Status_tb, 1, settings[0], Int32.Parse(NoOfEp_TBlock.Text));
                Website_LBI[0].IsSelected = true;
            }
            else if (UrlIdentifier(AnimeUrl.Text) == 2)
            {
                mygrabber.Grab(AnimeUrl.Text, Status_tb, 2, settings[0], Int32.Parse(NoOfEp_TBlock.Text));
                Website_LBI[1].IsSelected = true;
            }
            else if(UrlIdentifier(AnimeUrl.Text) == 3)
            {
                mygrabber.Grab(AnimeUrl.Text, Status_tb, 3, settings[0], Int32.Parse(NoOfEp_TBlock.Text));
                Website_LBI[2].IsSelected = true;
            }
            else if (UrlIdentifier(AnimeUrl.Text) == 4)
            {
                MessageBox.Show("noob");
                mygrabber.Grab(AnimeUrl.Text, Status_tb, 4, settings[0], Int32.Parse(NoOfEp_TBlock.Text));
                Website_LBI[3].IsSelected = true;
            }
            else
            {
                MessageBox.Show("it seems that the website u have entered isn't supported\nif u think there is an error pls report it\nThnx :) ");
            }
            
            if ((bool)BothRadioB.IsChecked)
            {
                
                //do it later
            }
            else if ((bool)VideoRadioB.IsChecked)
            {
                //later
            }
            
            

        }
        public static string Decode_Url(string URl)
        {
            return Uri.UnescapeDataString(URl);
        }
        public int UrlIdentifier(string url)
        {
            char[] chararray = url.ToCharArray();
            for (int i = 0; i < url.Length; i++)
            {
                if (url.Length > 6)
                {


                    if (chararray[i] == 'a'
                        && chararray[i + 1] == 'n'
                        && chararray[i + 2] == 'y'
                        && chararray[i + 3] == 'a'
                        && chararray[i + 4] == 'n'
                        && chararray[i + 5] == 'i')

                        return 1;
                    else if (chararray[i] == 'a'
                        && chararray[i + 1] == 'd'
                        && chararray[i + 2] == 'd'
                        && chararray[i + 3] == '-'
                        && chararray[i + 4] == 'a'
                        && chararray[i + 5] == 'n')
                        return 2;
                    else if (chararray[i] == 's'
                        && chararray[i + 1] == 'k'
                        && chararray[i + 2] == 'y'
                        && chararray[i + 3] == '-'
                        && chararray[i + 4] == 'a'
                        && chararray[i + 5] == 'n')
                        return 3;
                    else if (chararray[i] == 'o'
                        && chararray[i + 1] == 'k'
                        && chararray[i + 2] == 'a'
                        && chararray[i + 3] == 'n'
                        && chararray[i + 4] == 'i')
                        return 4;
                }
            }
            return 0;
        }
        private void AnimeUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            AnimeUrl.Text = Decode_Url(AnimeUrl.Text);
            if (UrlIdentifier(AnimeUrl.Text) == 1)
            {
                Website_LBI[0].IsSelected = true;
            }
            else if (UrlIdentifier(AnimeUrl.Text) == 2)
            {
                Website_LBI[1].IsSelected = true;
            }
            else if (UrlIdentifier(AnimeUrl.Text) == 3)
            {
                Website_LBI[2].IsSelected = true;
            }
            else if (UrlIdentifier(AnimeUrl.Text) == 4)
            {
                Website_LBI[3].IsSelected = true;
            }
        }
        //in v 2.0
        private void CountEPs_btn_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}