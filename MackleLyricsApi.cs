using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shitcord_Statusser
{
    public class Mackle
    {
        public int status;
        public MackleData info;
    }
    public class MackleData
    {
        public string title;
        public string lyrics;
        public string image;
    }

    internal class MackleLyricsApi
    {

        static DynamicLinkedList<string> m_Artists 
            = new DynamicLinkedList<string>(
                FileIO.Load(
                    FileIO.Configs + "artists.txt")
                );
        public static Mackle Get()
        {
            try
            {
                using (var api = new WebClient())
                {
                    var data = JsonConvert.DeserializeObject<Mackle>(
                        api.DownloadString("https://lyric.mackle.im/api?artist=" + m_Artists.m_Next)
                        );
                    return data;
                }
            }
            catch
            {

                return new Mackle();
            }

        }

    }
}
