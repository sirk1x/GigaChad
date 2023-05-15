using DiscordRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shitcord_Statusser
{
    internal class Choice
    {
        public string Title;
        public string Url;

        public static Button Create(string t, string u)
        {
            //Console.WriteLine(t.Count());
            return new Button() { Label = t, Url = u };
        }
    }
}
