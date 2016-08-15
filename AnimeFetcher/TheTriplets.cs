using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeFetcher
{
    class TheTriplets
    {
        public static string NextPage(string data, string Website = "http://add-anime.net", string keyword = "btn-danger\" href=\"")
        {
            //search for btn-danger" href="
            int start, end;
            if (data.Contains(keyword))
            {
                start = data.IndexOf(keyword) + keyword.Length;
                end = data.IndexOf("\"", start);
                Website += data.Substring(start, end - start);
                return Website;
            }
            return "";
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
                    holder += data.Substring(starttag, TagEnd - starttag);
                    holder += "\" target=\"_blanck\">" + "Download Link " + VideoCount++ + " </a>\n";
                }
            }

            return holder;
        }
     
    }
}
