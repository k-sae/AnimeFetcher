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
            if (int.Parse(url1) > int.Parse(url2))
            {
                return true;
            }
            return false;
        }
    }
}
