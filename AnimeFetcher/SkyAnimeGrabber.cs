using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeFetcher
{
    class SkyAnimeGrabber
    {
        static public string GrabIframeSourceVideo(string data)
        {

            int Ulstart = data.IndexOf("whcs-servers") + 12;
            int Ulend = data.IndexOf("</ul>", Ulstart);
            string IframeTags = "";
            while (Ulstart < Ulend)
            {
                Ulstart = data.IndexOf("href=\"", Ulstart) + 6;
                int end = data.IndexOf("\"", Ulstart);
                if (Ulstart < Ulend)
                {
                    IframeTags += FormIframeTag(data.Substring(Ulstart, end - Ulstart));
                }
            }
            return IframeTags;
        }
        static private string FormIframeTag(string URL)
        {
            string Iframe = "<iframe src=\""
                             + URL + "\" frameborder=\"0\" width=\"730\" height=\"440\""
                             + " scrolling=\"no\" name=\"FRAME1\" allowFullScreen></iframe>\n";
            return Iframe;
        }
        static public string NextPage(string data)
        {
            if (!data.Contains("vf-right")) return null;
            int start = data.IndexOf("vf-right");
            int DivEnd = data.IndexOf("</div>", start);
            string link = "";
            while(start < DivEnd)
            {
                start = data.IndexOf("<a href=\"", start) + 9;
                int end = data.IndexOf("\"", start);
                if (start < DivEnd)
                {
                    link = data.Substring(start, end - start);
                }
                
            }
            return link;
        }
        public static bool CheckLinkAddress(string url1, string url2)
        {
            url1 = Grabber.ReverseString(url1);
            string tempURL1 = "", tempURL2 = "";
            url2 = Grabber.ReverseString(url2);
            for (int i = 1; i < url1.Length; i++)
            {
                if (url1[i] == '/')
                {
                    break;
                }
                tempURL1 += url1[i];
            }
            for (int i = 1; i < url2.Length; i++)
            {
                if (url2[i] == '/')
                {
                    break;
                }
                tempURL2 += url2[i];
            }
            url1 = Grabber.ReverseString(tempURL1);
            url2 = Grabber.ReverseString(tempURL2);
            if (url2 == "" || url2 == null)
            {
                return false;
            }
            if (int.Parse(url1) > int.Parse(url2))
            {
                return true;
            }
            return false;
        }
        public static string GetDownloadLink(string data)
        {
            string keyword = "vdw-serverlist";
            int starttag = 0;
            int TagEnd = 0;
            int checker = -1;
            string holder = "";
            int VideoCount = 1;
            while (checker < starttag)
            {
                checker = starttag;
                starttag = data.IndexOf(keyword, starttag) + keyword.Length;
                if (starttag == keyword.Length - 1)
                {
                    break;
                }
                if (checker < starttag)
                {
                    holder += "<a href=\"";
                    TagEnd = data.IndexOf("</div>", starttag);
                    int linkstart = data.IndexOf("href=\"", starttag) + 6;
                    int linkend = data.IndexOf("\"", linkstart);
                    holder += data.Substring(linkstart, linkend - linkstart);
                    holder += "\" target=\"_blanck\">" + "Download Link " + VideoCount++ + " </a>\n";
                }
            }

            return holder;
        }
    }
}
