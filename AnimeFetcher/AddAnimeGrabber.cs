using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//Add Anime Grabber Licensed To me (kareem) 
// To Do When i have got Any Fucken interest :
// 1- Get the value of each anime from add Anime main page 
// 2- make the add Anime User Interface Diffrent u get the anime address from the search bar in website main page
// 3- may no need for prev function 
// 4- there is Upgrade I forgot i will write it later
// 5- iam fcken bored :\
// 6- still bored 
// 7- search for some one to handle the rest of the project
namespace AnimeFetcher
{
    class AddAnimeGrabber
    {
        private static string Pickvariable(string data, string variablename)
        {
            variablename += " = \'";
            int Start = 0, End = 0;
            if (data.Contains(variablename))
            {
                Start = data.IndexOf(variablename, 0) + variablename.Length;
                End = data.IndexOf("\'", Start);
                return data.Substring(Start, End - Start);
            }
            return "";
        }
        public static string GenerateObjectTag(string data)
        {
            //if there is future errors grab the key
            //string key = Pickvariable(data, "key");
            string pakplayer_path = Pickvariable(data, "pakplayer_path");
            string player_logo = Pickvariable(data, "player_logo");
            string hq_video_file = Pickvariable(data, "hq_video_file");
            string normal_video_file = Pickvariable(data, "normal_video_file");
            string splash_img = Pickvariable(data, "splash_img");
            string video_vast_red = Pickvariable(data, "video_vast_red");
            string videoup = Pickvariable(data, "videoup");
            string video01 = Pickvariable(data, "video01");
            string video02 = Pickvariable(data, "video02");
            string video03 = Pickvariable(data, "video03");
            string video04 = Pickvariable(data, "video04");
            string dir = Pickvariable(data, "dir");
            string ObjectTag = FormObjectTag();
            ObjectTag += pakplayer_path +"/flowplayer.ima-3.2.7.swf" + "&quot;},";
            ObjectTag += "&quot;cluster&quot;:{&quot;url&quot;:&quot;"
                      + pakplayer_path + "/flowplayer.cluster-3.2.9.swf?dhtdth=dhdth&quot;,"
                      + "&quot;hosts&quot;:[&quot;" + video01 + dir
                      + "&quot;,&quot;" + video02 + dir + "&quot;,&quot;"
                      + video03 + dir + "&quot;,&quot;"
                      + videoup + dir + "&quot;],"
                      + "&quot;connectTimeout&quot;:&quot;700000&quot;,&quot;connectCount&quot;:&quot;4&quot;" 
                      + ",&quot;failureExpiry&quot;:&quot;70000&quot;,&quot;loadBalance&quot;:&quot;true&quot;},"
                      + "&quot;controls&quot;:{&quot;background&quot;:&quot;url(" + pakplayer_path + "/bg.png) repeat&quot;"
                      + ",&quot;url&quot;:&quot;pakplayer.controls.swf&quot;}"
                      + ",&quot;lighttpd&quot;:{&quot;url&quot;:&quot;" + pakplayer_path + "/pakplayer.pseudo.swf&quot;"
                      + ",&quot;enableRangeRequests&quot;:true}}"
                      + ",&quot;canvas&quot;:{&quot;backgroundColor&quot;:&quot;#000000&quot;"
                      + ",&quot;backgroundGradient&quot;:&quot;none&quot;"
                      + ",&quot;background&quot;:&quot;#000000 url(" + splash_img + ") no-repeat 50pct 50pct&quot;"
                      + ",&quot;border&quot;:&quot;2px solid #778899&quot;}"
                      + ",&quot;clip&quot;:{&quot;urlResolvers&quot;:&quot;cluster&quot;"
                      + ",&quot;linkUrl&quot;:&quot;#&quot;,&quot;provider&quot;:&quot;lighttpd&quot;"
                      + ",&quot;scaling&quot;:&quot;fit&quot;"
                      + ",&quot;autoPlay&quot;:false"
                      + ",&quot;url&quot;:&quot;" + normal_video_file + "&quot;}"
                      + ",&quot;playlists&quot;:[{&quot;url&quot;:&quot;" + normal_video_file + "&quot;}]"
                      + ",&quot;playerId&quot;:&quot;the_Video_Player&quot;"
                      + ",&quot;playlist&quot;:[{&quot;urlResolvers&quot;:&quot;cluster&quot;"
                      + ",&quot;linkUrl&quot;:&quot;#&quot;"
                      + ",&quot;provider&quot;:&quot;lighttpd&quot;"
                      + ",&quot;scaling&quot;:&quot;fit&quot;"
                      + ",&quot;autoPlay&quot;:false"
                      + ",&quot;url&quot;:&quot;" + normal_video_file + "&quot;}]}\"></object>";
            return ObjectTag;
        }
        private static string FormObjectTag()
        {
            string ObjectTag = "<object width=\"720px\" height=\"480px\" id=\"the_Video_Player_api\""
                + " name=\"the_Video_Player_api\""
                + " data=\"http://add-anime.net/player/pak_player/pakplayer.unlimited.swf?0.550757871940732\""
                + " type=\"application/x-shockwave-flash\">\n"
                + "<param name=\"allowfullscreen\" value=\"true\">\n"
                + "<param name=\"allowscriptaccess\" value=\"always\">\n"
                + "<param name=\"quality\" value=\"high\">\n"
                + "<param name=\"cachebusting\" value=\"true\">\n"
                + "<param name=\"bgcolor\" value=\"#000000\">\n"
                + "<param name=\"flashvars\" value=\"config={&quot;key&quot;:"
                + "&quot;179e02ce63a928e2893&quot;,"
                + "&quot;plugins&quot;:{&quot;ima&quot;:{&quot;url&quot;:&quot;";
            return ObjectTag;
        }
        //try to do it later
        private static void GrabIframeTag()
        {

        }
    }
}
