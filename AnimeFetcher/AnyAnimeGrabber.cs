using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
//To Do: 
// 1- make count the eps from the scroll bar div
// 2- add combo box items that holds the eps
// 3- think if u want to save the eps url
namespace AnimeFetcher
{
    class AnyAnimeGrabber
    {
        public static string grabFrameTag(string data, TextBox status)
        {
            string Embed = "<if";
            int frameincounter = 0;
            int found = 0;
            foreach (char item in data)
            {
                //search for the embeded video
                if (((item == '<' && frameincounter == 0)
                || (item == 'i' && frameincounter == 1)
                || (item == 'f' && frameincounter == 2)
                || (item == 'r' && frameincounter == 3))
                && frameincounter != -1)
                {
                    frameincounter++;
                }
                else if (frameincounter != 4)
                {
                    frameincounter = 0;
                }
                if (frameincounter == 4 && frameincounter != -1)
                {

                    Embed += item;
                }
                if (frameincounter == 4 && item == '>')
                {
                    found++;
                    status.Text += "Frame found\n";
                    break;
                }

            }
            Embed += Embed + " " + "</iframe>\n";
            if (found == 0)
            {
                Embed = "";
            }



            return Embed;
        }
        public static string grabobjectTag(string data)
        {
            string ObjectTag = "<ob";
            int objectincounter = 0;
            int objectoutcounter = 0;
            foreach (char item in data)
            {



                //search for the embeded video
                if (((item == '<' && objectincounter == 0)
                || (item == 'o' && objectincounter == 1)
                || (item == 'b' && objectincounter == 2)
                || (item == 'j' && objectincounter == 3))
                && objectincounter != -1)
                {
                    objectincounter++;
                }
                else if (objectincounter != 4)
                {
                    objectincounter = 0;
                }
                if (objectincounter == 4 && objectincounter != -1)
                {

                    ObjectTag += item;
                }

                if (((item == '<' && objectoutcounter == 0)
                || (item == '/' && objectoutcounter == 1)
                || (item == 'o' && objectoutcounter == 2)
                || (item == 'b' && objectoutcounter == 3)))
                {
                    objectoutcounter++;
                }
                else
                {
                    objectoutcounter = 0;
                }
                if (objectoutcounter == 4)
                {
                    objectincounter = 0;
                    ObjectTag += "ject>\n";
                }

            }

            return ObjectTag;
        }
        public static string grabEmbTag(string data, TextBox status)
        {
            string embTag = "";
            int embincounter = 0;
            int srccounter = 0;
            int servercount = 0;
            foreach (char item in data)
            {



                //search for the embeded video
                if (((item == '<' && embincounter == 0)
                || (item == 'e' && embincounter == 1)
                || (item == 'm' && embincounter == 2)
                || (item == 'b' && embincounter == 3))
                && embincounter != -1)
                {
                    embincounter++;
                }
                else if (embincounter != 4)
                {
                    embincounter = 0;
                }
                if (embincounter >= 4 && embincounter != -1)
                {
                    if (((item == 's' && srccounter == 0)
                        || (item == 'r' && srccounter == 1)
                        || (item == 'c' && srccounter == 2)
                        || (item == '=' && srccounter == 3)))
                    {
                        srccounter++;
                    }
                    else if (srccounter < 4)
                    {
                        srccounter = 0;
                    }
                    if (srccounter == 4)
                    {
                        srccounter++;
                        embTag += "<a href";
                    }
                    if (srccounter >= 4)
                    {
                        embTag += item;
                    }
                    if (item == '\"' || item == '\'')
                    {
                        srccounter++;
                    }
                    if ((item == '\"' || item == '\'') && srccounter > 6)
                    {
                        status.Text += "Embbeded video found!\n";
                        status.ScrollToEnd();
                        srccounter = 0;
                        servercount++;
                        embTag += " target=\"_blanck\">video Server " + servercount.ToString() + " </a>\n<hr>\n";
                    }
                }

                if (item == '>' && embincounter == 4)
                {
                    embincounter = 0;
                    embTag += '\n';

                }
            }
            if (servercount == 0)
            {
                embTag = "";
                status.Text += "no Embbeded video found!\n";
                status.ScrollToEnd();
            }
            return embTag;
        }
        public static string grabDownloadLink(string data, string classname, TextBox status)
        {
            int classincounter = 0;
            int hrefincounter = 0;
            int classoutcounter = 0;
            string hrefpicker = "";
            int canIout = 0;
            int noofservers = 0;
            foreach (char item in data)
            {
                if (item == classname[classincounter] && classincounter != classname.Length - 1)
                {
                    classincounter++;
                }
                else if (classincounter < classname.Length - 1)
                {
                    classincounter = 0;
                }
                if (classincounter == classname.Length - 1)
                {
                    if ((item == 'h' && hrefincounter == 0)
                        || (item == 'r' && hrefincounter == 1)
                        || (item == 'e' && hrefincounter == 2)
                        || (item == 'f' && hrefincounter == 3))
                    {
                        hrefincounter++;
                    }
                    else if (hrefincounter < 4)
                    {
                        hrefincounter = 0;
                    }
                    if (hrefincounter == 4)
                    {
                        hrefpicker += "<a hre";
                        hrefincounter++;
                    }
                    if (hrefincounter >= 4)
                    {
                        hrefpicker += item;
                    }
                    if ((item == '\"' || item == '\''))
                    {
                        hrefincounter++;
                    }

                    if (hrefincounter >= 7 && (item == '\"' || item == '\''))
                    {
                        noofservers++;
                        hrefincounter = 0;
                        status.Text += "Download Link Found!\n";
                        status.ScrollToEnd();
                        hrefpicker += " target=\"_blanck\">download server " + noofservers.ToString() + "</a><hr>\n";
                    }
                }
                if (((item == '<' && classoutcounter == 0)
                        || (item == '/' && classoutcounter == 1)
                        || (item == 'd' && classoutcounter == 2)
                        || (item == 'i' && classoutcounter == 3))
                        && classincounter == classname.Length - 1)
                {
                    classoutcounter++;
                }
                else if (classoutcounter != 4) classoutcounter = 0;
                if (classoutcounter == 4)
                {
                    canIout++;
                    classoutcounter = 0;
                }
                if (canIout == 2)
                {
                    break;
                }
            }
            return hrefpicker;
        }
        public static string nextpage(string data, string title)
        {
            // first get the Ep-number

            int counter_Ep = 0;
            string Ep_No = "";
            foreach (char item in title)
            {
                if (Char.IsNumber(item))
                {
                    counter_Ep++;
                    Ep_No += item;
                    if (Ep_No[0] == '0')
                    {
                        Ep_No = Ep_No.Replace("0", "");
                        counter_Ep--;
                    }

                }

            }
            // second get between <div class="scroll-ep" and </div>

            string dataStart = "<div class=\"scroll-ep\">";
            string dataEnd = "</div>";
            int Start, End;
            string myNewData = "";
            if (data.Contains(dataStart) && data.Contains(dataEnd))
            {
                Start = data.IndexOf(dataStart, 0) + dataStart.Length;
                End = data.IndexOf(dataEnd, Start);
                myNewData = data.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }

            // find our URL

            int counter1 = 0;
            string myUrl = "htt";
            int titleoutcounter = 0;
            int titleincounter = 0;


            // All next are Because Some Ep_Nums could be like 1 or 01 or 001 Just In Case

            if (counter_Ep == 1)
            {
                foreach (char item in myNewData)
                {
                    if (((item == Ep_No[0] && counter1 == 0)
                        || (item == '<' && counter1 == 1)
                        || (item == '/' && counter1 == 2))
                        && counter1 != -1)
                    {
                        counter1++;
                    }
                    else if (counter1 != -1 && counter1 != 3)
                    {
                        counter1 = 0;
                    }
                    if (counter1 == 3)
                    {
                        if (((item == 'h' && titleincounter == 0)
                                       || (item == 't' && titleincounter == 1)
                                       || (item == 't' && titleincounter == 2)
                                       || (item == 'p' && titleincounter == 3))
                                       && titleincounter != -1)
                        {
                            titleincounter++;
                        }
                        else if (titleincounter != -1 && titleincounter != 4)
                        {
                            titleincounter = 0;
                        }
                        if (titleincounter == 4)
                        {
                            myUrl += item;
                        }

                        //search for end
                        if (((item == '/' && titleoutcounter == 0)
                             || (item == '"' && titleoutcounter == 1))
                             && titleoutcounter != -1)
                        {
                            titleoutcounter++;
                        }
                        else
                        {
                            titleoutcounter = 0;
                        }
                        if (titleoutcounter == 2)
                        {
                            break;
                        }
                    }
                }
            }
            else if (counter_Ep == 2)
            {
                foreach (char item in myNewData)
                {
                    if (((item == Ep_No[0] && counter1 == 0)
                        || (item == Ep_No[1] && counter1 == 1)
                        || (item == '<' && counter1 == 2)
                        || (item == '/' && counter1 == 3))
                        && counter1 != -1)
                    {
                        counter1++;
                    }
                    else if (counter1 != -1 && counter1 != 4)
                    {
                        counter1 = 0;
                    }
                    if (counter1 == 4)
                    {
                        if (((item == 'h' && titleincounter == 0)
                                       || (item == 't' && titleincounter == 1)
                                       || (item == 't' && titleincounter == 2)
                                       || (item == 'p' && titleincounter == 3))
                                       && titleincounter != -1)
                        {
                            titleincounter++;
                        }
                        else if (titleincounter != -1 && titleincounter != 4)
                        {
                            titleincounter = 0;
                        }
                        if (titleincounter == 4)
                        {
                            myUrl += item;
                        }

                        //search for end
                        if (((item == '/' && titleoutcounter == 0)
                             || (item == '"' && titleoutcounter == 1))
                             && titleoutcounter != -1)
                        {
                            titleoutcounter++;
                        }
                        else
                        {
                            titleoutcounter = 0;
                        }
                        if (titleoutcounter == 2)
                        {
                            break;
                        }
                    }

                }
            }
            else if (counter_Ep == 3)
            {
                foreach (char item in myNewData)
                {
                    if (((item == Ep_No[0] && counter1 == 0)
                        || (item == Ep_No[1] && counter1 == 1)
                || (item == Ep_No[2] && counter1 == 2)
                        || (item == '<' && counter1 == 3)
                        || (item == '/' && counter1 == 4))
                        && counter1 != -1)
                    {
                        counter1++;
                    }
                    else if (counter1 != -1 && counter1 != 5)
                    {
                        counter1 = 0;
                    }
                    if (counter1 == 5)
                    {
                        if (((item == 'h' && titleincounter == 0)
                                       || (item == 't' && titleincounter == 1)
                                       || (item == 't' && titleincounter == 2)
                                       || (item == 'p' && titleincounter == 3))
                                       && titleincounter != -1)
                        {
                            titleincounter++;
                        }
                        else if (titleincounter != -1 && titleincounter != 4)
                        {
                            titleincounter = 0;
                        }
                        if (titleincounter == 4)
                        {
                            myUrl += item;
                        }

                        //search for end
                        if (((item == '/' && titleoutcounter == 0)
                             || (item == '"' && titleoutcounter == 1))
                             && titleoutcounter != -1)
                        {
                            titleoutcounter++;
                        }
                        else
                        {
                            titleoutcounter = 0;
                        }
                        if (titleoutcounter == 2)
                        {
                            break;
                        }

                    }
                }

            }
            myUrl = myUrl.Replace("\"", "");
            return myUrl;
        }

    }
}
