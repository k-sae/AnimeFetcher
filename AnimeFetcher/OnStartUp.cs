using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeFetcher
{
    //check the If Directory Exists
    //get the theme
    //get last video URl
    //get get next vieo URl
    class StartUp
    {
        public static string Location()
        {
            string path = Path.GetPathRoot(Environment.SystemDirectory);
            string userName = Grabber.pickusername();
            string URI = path + @"Users\" + userName + @"\Documents\AnimeFetcher\";
            return URI;
        }
        public static string[] settings()
        {
            if(File.Exists(Location() + "Settings.anf"))
            {
                return File.ReadAllLines(Location() + "Settings.anf");
            }
            else
            {
                string[] settings = new string[2];
                settings[0] = "default";
                settings[1] = "1";
                File.WriteAllLines(Location() + "Settings.anf", settings);
                return settings;
            }
        }
        public static string[] lastsession()
        {
            if (File.Exists(Location() + "LastSession.txt"))
            {
                return File.ReadAllLines(Location() + "LastSession.txt");

            }
            else
            {
                string[] temp = new string[2];
                return temp;
            }
        }
        public static void check()
        {
            if(!Directory.Exists(Location()))
            {
                Directory.CreateDirectory(Location());
            }
        }
    }
}
