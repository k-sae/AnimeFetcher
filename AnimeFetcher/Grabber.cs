using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//to do :
//make the list box auto change according to the address inside the url textbox
//change how anyanime grab next page
//make a background proccess that grab all eps links and store them(canceled)
//look at notes in AddAnimeGrabber class
namespace AnimeFetcher
{
    // to do previous function 
    class Grabber
    {
        public void Grab(string AnimeUrl, TextBox status, int website, string Browser = "", int EpsNo = 1)
        {
            if (website == 1)
            {
                anyanimegrabber_f(AnimeUrl, status, Browser, EpsNo);
            }
            else if (website == 2)
            {
                AddAnimeGrabber_f(AnimeUrl, status, Browser, EpsNo);
            }
            else if (website == 4)
            {
                OkAnimeGrabber_f(AnimeUrl, status, Browser, EpsNo);
            }
            else if (website == 3)
            {
                SkyAnimeGrabber_f(AnimeUrl, status, Browser, EpsNo);
            }
        }
        private string grabTitle(string data)
        {

            string title = "<ti";
            int titleincounter = 0;
            int titleoutcounter = 0;
            foreach (char item in data)
            {
                //seach for title open tag
                if (((item == '<' && titleincounter == 0)
                     || (item == 't' && titleincounter == 1)
                     || (item == 'i' && titleincounter == 2)
                     || (item == 't' && titleincounter == 3))
                     && titleincounter != -1)
                {
                    titleincounter++;
                }
                else if (titleincounter != -1 && titleincounter != 4)
                {
                    titleincounter = 0;
                }
                //save the content of the tag
                if (titleincounter == 4)
                {
                    title += item;
                }

                //search for the end of title Tag
                if (((item == '<' && titleoutcounter == 0)
                     || (item == '/' && titleoutcounter == 1)
                     || (item == 't' && titleoutcounter == 2)
                     || (item == 'i' && titleoutcounter == 3))
                     && titleoutcounter != -1)
                {
                    titleoutcounter++;
                }
                else
                {
                    titleoutcounter = 0;
                }
                if (titleoutcounter == 4)
                {
                    break;
                }
            }
            title += "tle>\n";
            return title;
        }
        //grab Iframe tag
        private async void anyanimegrabber_f(string AnimeUrl, TextBox status, string Browser = "", int EpsNo = 1)
        {
            string path = Path.GetPathRoot(Environment.SystemDirectory);
            string userName = pickusername();
            string URI = path + @"Users\" + userName + @"\Documents\AnimeFetcher\";
            string Html = "";
            status.Text += "Downloading Page\n";
            status.ScrollToEnd();
            Task<string> data2;
            data2 = DownloadPage(AnimeUrl);
            string data = "";
            try
            {
                data = await data2;
            }
            catch (WebException)
            {
                status.Text += "cant find " + AnimeUrl
                    + "\nwebpage error make sure the webpage exists\n"
                    + "make sure that ur internet connection works\n";
                return;
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("enter URL");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            status.Text += "Download Succed\n";
            // save(data,"default", @"D:\page3.html");
            status.Text += "picking title\n";
            string title = grabTitle(data);
            Html = formHTMLpage(title);
            for (int i = 0; i < EpsNo; i++)
            {
                Html += "<h2>";
                Html += AnimeName(title) + "h2>\n";


                status.Text += "searching for Embbeded videos...\n";
                string obj = AnyAnimeGrabber.grabEmbTag(data, status);
                //string obj = GrabVideoTag(data);
                if (obj == "")
                {
                    status.Text += "searching for Framed videos...\n";
                    obj = AnyAnimeGrabber.grabFrameTag(data, status);
                }

                Html += obj;
                Html += "<hr>\n";
                string Downloadservers = AnyAnimeGrabber.grabDownloadLink(data, "down-lnk", status);
                Html += Downloadservers;
                AnimeUrl = AnimeSites.Decode_Url(AnimeUrl);
                status.Text += "current page is: " + AnimeUrl + "\n";
                File.WriteAllText(URI + "LastSession.txt", AnimeUrl + Environment.NewLine);
                if (AnimeUrl == null)
                {
                    break;
                }
                AnimeUrl = AnyAnimeGrabber.nextpage(data, title);
                if (AnimeUrl == null)
                {
                    break;
                }
                if (i + 1 != EpsNo)
                {
                    data = "";
                    status.Text += "Downloading Page\n";
                    status.ScrollToEnd();
                    if (AnimeUrl == "htt" || AnimeUrl == "" || AnimeUrl == null)
                    {
                        AnimeUrl = "";
                        status.Text += "Webpage Not Found\n";
                        break;

                    }
                    data2 = DownloadPage(AnimeUrl);
                    data = await data2;
                    status.Text += "Download Succed\n";
                    status.ScrollToEnd();
                    //save(data, @"D:\page3.html");
                    status.Text += "picking title\n";
                    status.ScrollToEnd();
                    title = grabTitle(data);


                }

            }
            Html += "</body>\n"
                      + "</html>";
            status.Text += "saving HTML file....\n";
            status.ScrollToEnd();
            save(Html, Browser, URI + "page2.html");
            status.Text += "page Saved\nopening page....\nNext Page Is:\n";
            status.ScrollToEnd();
            File.AppendAllText(URI + "LastSession.txt", AnimeUrl);
            if (AnimeUrl == null)
            {
                return;
            }
            //status.Text += (nextpage(AnimeUrl, website));
            status.Text += AnimeUrl + "\n";
            status.ScrollToEnd();
        }
        private string formHTMLpage(string title)
        {
            string Html = "<!doctype html>\n" +
                               "<html lang=\"ar\">\n" +
                               "<head>\n" +
                               "<meta http-equiv=\"Content-Type\" content=\"text/html; charset = UTF-8\"/>\n";
            Html += title;
            Html += " </head>\n"
                + "<body>\n"
                + "<h1>this page is auto created by <span style=\"color:#00f\"> Anime Fetcher </span></h1>\n";

            return Html;
        }
        private async Task<string> DownloadPage(string uri)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers[HttpRequestHeader.UserAgent] = "Chrome/44.0.874.121 Safari/535.2";
            string url = await client.DownloadStringTaskAsync(uri);
            return url;
        }
        private void save(string data, string Browser, string uri = @"D:\page2.html")
        {
            File.WriteAllText(uri, data);
            if (Browser == "default")
            {
                Process.Start(uri);
            }
            else
                Process.Start(Browser, uri);
        }
        public static string ReverseString(string mystring)
        {
            char[] arr = mystring.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        public static string pickusername()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            userName = ReverseString(userName);
            string temp = "";
            foreach (char item in userName)
            {
                if (item == '\\')
                {
                    break;
                }
                temp += item;
            }
            userName = ReverseString(temp);
            return userName;
        }
        private string AnimeName(string title)
        {
            string Html = "";
            for (int i = 6; i < title.Length - 7; i++)
            {
                Html += title[i];
            }

            return Html;
        }
  
        //to do
        //grab the iframe tag
        private async void AddAnimeGrabber_f(string AnimeUrl, TextBox status, string Browser = "", int EpsNo = 1)
        {
            string path = Path.GetPathRoot(Environment.SystemDirectory);
            string userName = pickusername();
            string URI = path + @"Users\" + userName + @"\Documents\AnimeFetcher\";
            string Html = "";
            status.Text += "Downloading Page\n";
            status.ScrollToEnd();
            Task<string> data2;
            data2 = DownloadPage(AnimeUrl);
            string data = "";
            try
            {
                data = await data2;
            }
            catch (WebException)
            {
                status.Text += "cant find " + AnimeUrl
                    + "\nwebpage error make sure the webpage exists\n"
                    + "make sure that ur internet connection works\n";
                return;
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("enter URL");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            status.Text += "Download Succed\n";
            //save(data, @"D:\page3.html");
            status.Text += "picking title\n";
            string title = grabTitle(data);
            Html = formHTMLpage(title);
            for (int i = 0; i < EpsNo; i++)
            {
                Html += "<h2>";
                Html += AnimeName(title) + "h2>\n";
                status.Text += "picking AddAnime Video player...\n";
                Html += AddAnimeGrabber.GenerateObjectTag(data);
                Html += "<hr>\n";
                status.Text += "searching for Embbeded videos...\n";
                string obj = AnyAnimeGrabber.grabEmbTag(data, status);
                if (obj == "")
                {
                    status.Text += "NoEbbeded Video found!\n";
                }
                Html += obj;
                Html += "<hr>\n";
                status.Text += "download Links...\n";
                Html += "<h2>Direct Download Links</h2>\n";
                // string DirectdownloadLinks = AddAnimeGrabber.GetDownloadLink(data);
                Html += TheTriplets.GetDownloadLink(data);
                Html += "<h2>Google Download Links</h2>\n";
                Html += TheTriplets.GetDownloadLink(data, "(Google Drive) : <a href=\"");

                status.Text += "current page is: " + AnimeUrl + "\n";
                File.WriteAllText(URI + "LastSession.txt", AnimeUrl + Environment.NewLine);
                if (AnimeUrl == null || AnimeUrl == "")
                {
                    break;
                }
                AnimeUrl = TheTriplets.NextPage(data);
                if (AnimeUrl == null)
                {
                    break;

                }
                status.Text += AnimeName(title) + "\n";
                if (i + 1 != EpsNo)
                {
                    data = "";
                    status.Text += "Downloading Page\n";
                    status.ScrollToEnd();
                    if (AnimeUrl == "" || AnimeUrl == null)
                    {
                        status.Text += "Webpage Not Found\n";
                        break;
                    }
                    data2 = DownloadPage(AnimeUrl);
                    data = await data2;
                    status.Text += "Download Succed\n";
                    status.ScrollToEnd();
                    //save(data, @"D:\page3.html");
                    status.Text += "picking title\n";
                    status.ScrollToEnd();
                    title = grabTitle(data);
                    if (AnimeName(title) == ">add-anime | اد انمي الانمي اون لاين - اد انمي الرئيسية</"
                        || title == "<title>add-anime | اد انمي الانمي اون لاين - no data</title>\n")
                    {
                        status.Text += "Webpage Not Found\n";
                        break;

                    }

                }
            }
            Html += "</body>\n"
                      + "</html>";
            status.Text += "saving HTML file....\n";
            status.ScrollToEnd();
            save(Html, Browser, URI + "page2.html");
            status.Text += "page Saved\nopening page....\nNext Page Is:\n";
            status.ScrollToEnd();
            if (AnimeUrl == null)
            {
                return;
            }
            File.AppendAllText(URI + "LastSession.txt", AnimeUrl);
            status.Text += AnimeUrl + "\n";
            status.ScrollToEnd();
        }
        private async void OkAnimeGrabber_f(string AnimeUrl, TextBox status, string Browser = "", int EpsNo = 1)
        {
            string path = Path.GetPathRoot(Environment.SystemDirectory);
            string userName = pickusername();
            string URI = path + @"Users\" + userName + @"\Documents\AnimeFetcher\";
            string Html = "";
            status.Text += "Downloading Page\n";
            status.ScrollToEnd();
            Task<string> data2;
            data2 = DownloadPage(AnimeUrl);
            string data = "";
            try
            {
                data = await data2;
            }
            catch (WebException)
            {
                status.Text += "cant find " + AnimeUrl
                    + "\nwebpage error make sure the webpage exists\n"
                    + "make sure that ur internet connection works\n";
                return;
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("enter URL");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            status.Text += "Download Succed\n";
            status.Text += "picking title\n";
            string title = grabTitle(data);
            Html = formHTMLpage(title);
            for (int i = 0; i < EpsNo; i++)
            {
                Html += "<h2>";
                Html += AnimeName(title) + "h2>\n";
                status.Text += "Picking Videos...\n";
                Html += OkAnimeGrabber.GrabIframeSourceVideo(data);
                Html += "<hr>\n";
                Html += "<hr>\n";
                status.Text += "download Links...\n";
                Html += OkAnimeGrabber.GetDownloadLink(data, "<li class=\"li-server\">\n<a href=\"");
                status.Text += "current page is: " + AnimeUrl + "\n";
                File.WriteAllText(URI + "LastSession.txt", AnimeUrl + Environment.NewLine);
                if (AnimeUrl == null)
                {
                    break;
                }
             
                AnimeUrl = TheTriplets.NextPage(data, "", "ApplyTooltip\">\n<a href=\"");
                if (AnimeUrl == null )
                {
                    status.Text += "Webpage Not Found\n";
                    break;

                }
                status.Text += AnimeName(title) + "\n";
                if (i + 1 != EpsNo)
                {
                    data = "";
                    status.Text += "Downloading Page\n";
                    status.ScrollToEnd();
                    if (AnimeUrl == "" || AnimeUrl == null)
                    {
                        status.Text += "Webpage Not Found\n";
                        break;
                    }
                    data2 = DownloadPage(AnimeUrl);
                    data = await data2;
                    status.Text += "Download Succed\n";
                    status.ScrollToEnd();
                    //save(data, @"D:\page3.html");
                    status.Text += "picking title\n";
                    status.ScrollToEnd();
                    title = grabTitle(data);

                }
            }
            Html += "</body>\n"
                     + "</html>";
            status.Text += "saving HTML file....\n";
            status.ScrollToEnd();
            save(Html, Browser, URI + "page2.html");
            status.Text += "page Saved\nopening page....\nNext Page Is:\n";
            status.ScrollToEnd();
            if (AnimeUrl == null)
            {
                return;
            }
            status.Text += AnimeUrl + "\n";
            File.AppendAllText(URI + "LastSession.txt", AnimeUrl);
            status.ScrollToEnd();
        }
        private string GrabVideoTag(string data)
        {
            int indexstart = 0;
            int indexend = 0;
            int IsFound = 1;
            string videotag = "";
            while (IsFound != 0)
            {
                string temp = "";
                if (data.Contains("<video") && data.Contains("</video>"))
                {
                    indexstart = data.IndexOf("<video", indexstart) + 6;
                    indexend = data.IndexOf("</video>", indexstart);
                    temp = data.Substring(indexstart, indexend - indexstart);
                    videotag += temp + "\n";
                }
                IsFound = 0;

            }
            return videotag;
        }
        private async void SkyAnimeGrabber_f(string AnimeUrl, TextBox status, string Browser = "", int EpsNo = 1)
        {
            string path = Path.GetPathRoot(Environment.SystemDirectory);
            string userName = pickusername();
            string URI = path + @"Users\" + userName + @"\Documents\AnimeFetcher\";
            string Html = "";
            status.Text += "Downloading Page\n";
            status.ScrollToEnd();
            Task<string> data2;
            data2 = DownloadPage(AnimeUrl);
            string data = "";
            try
            {
                data = await data2;
            }
            catch (WebException)
            {
                status.Text += "cant find " + AnimeUrl
                    + "\nwebpage error make sure the webpage exists\n"
                    + "make sure that ur internet connection works\n";
                return;
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("enter URL");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            status.Text += "Download Succed\n";
            status.Text += "picking title\n";
            string title = grabTitle(data);
            Html = formHTMLpage(title);
            for (int i = 0; i < EpsNo; i++)
            {
                Html += "<h2>";
                Html += AnimeName(title) + "h2>\n";
                status.Text += "Picking Videos...\n";
                Html += SkyAnimeGrabber.GrabIframeSourceVideo(data);
                Html += "<hr>\n";
                Html += "<hr>\n";
                status.Text += "download Links...\n";
                Html += TheTriplets.GetDownloadLink(data, "</span>\n					<a href=\"");
                status.Text += "current page is: " + AnimeUrl + "\n";
                File.WriteAllText(URI + "LastSession.txt", AnimeUrl + Environment.NewLine);
                if (AnimeUrl == null )
                {
                    break;
                }
                string OldUrl = AnimeUrl;
                AnimeUrl = SkyAnimeGrabber.NextPage(data);
                if (AnimeUrl == null || SkyAnimeGrabber.CheckLinkAddress(OldUrl, AnimeUrl))
                {
                    status.Text += "Webpage Not Found\n";
                    break;

                }
                status.Text += AnimeName(title) + "\n";
                if (i + 1 != EpsNo)
                {
                    data = "";
                    status.Text += "Downloading Page\n";
                    status.ScrollToEnd();
                    if (AnimeUrl == "" || AnimeUrl == null)
                    {
                        status.Text += "Webpage Not Found\n";
                        break;
                    }
                    data2 = DownloadPage(AnimeUrl);
                    data = await data2;
                    status.Text += "Download Succed\n";
                    status.ScrollToEnd();
                    //save(data, @"D:\page3.html");
                    status.Text += "picking title\n";
                    status.ScrollToEnd();
                    title = grabTitle(data);

                }
            }
            Html += "</body>\n"
                     + "</html>";
            status.Text += "saving HTML file....\n";
            status.ScrollToEnd();
            save(Html, Browser, URI + "page2.html");
            status.Text += "page Saved\nopening page....\nNext Page Is:\n";
            status.ScrollToEnd();
            if (AnimeUrl == null)
            {
                return;
            }
            status.Text += AnimeUrl + "\n";
            File.AppendAllText(URI + "LastSession.txt", AnimeUrl);
            status.ScrollToEnd();
        }
    }
   
}

