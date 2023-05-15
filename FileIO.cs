using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shitcord_Statusser
{
    static class FileIO
    {
        public static string Dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "//";

        public static string Configs => Dir + "configs//";

        public static string[] Load(string pth)
        {
            if (File.Exists(pth))
                return System.IO.File.ReadAllLines(pth);
            return null;
        }
        public static string[] GetFiles(string subDir)
        {
            if (Directory.Exists(subDir))
                return Directory.GetFiles(Dir + subDir, "*.txt", SearchOption.AllDirectories);
            return null;
        }

    }
}
