using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeFetcher
{
    class OkAnimeGrabber
    {
        static public string GrabIframeSourceVideo(string data)
        {

            int Ulstart = data.IndexOf("ul-server-position") + 18;
            int Ulend = data.IndexOf("</ul>", Ulstart);
            string IframeTags = "";
            while(Ulstart < Ulend)
            {
                Ulstart = data.IndexOf("href='", Ulstart) + 6;
                int end = data.IndexOf("'", Ulstart);
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
        public static string GetDownloadLink(string data, string keyword = "<p>\n<li><a href=\"")
        {
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
                    TagEnd = data.IndexOf("\"", starttag);
                    holder += RemoveAdsHost(data.Substring(starttag, TagEnd - starttag));
                    holder += "\" target=\"_blanck\">" + "Download Link " + VideoCount++ + " </a>\n";
                }
            }

            return holder;
        }
        private static string RemoveAdsHost(string url)
        {
            int start = url.IndexOf("okanime");
            url = url.Substring(start);
            return "http://" + url;
        }
    }
}
