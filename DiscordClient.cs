using DiscordRPC;
using DiscordRPC.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shitcord_Statusser
{
    internal class DiscordClient : IDisposable
    {
        private DiscordRpcClient Client;

        private Thread m_Thread;

        private bool m_ShowAd = bool.Parse(FileIO.Load(FileIO.Configs + "show-ad.txt")[0]);

        private Button m_AdButton = new Button { Label = "GigaChad available on GitHub", Url = "https://github.com/sirk1x/GigaChad" };

        private DynamicLinkedList<string> m_ImageList = new DynamicLinkedList<string>(FileIO.Load(
                    FileIO.Configs + "images.txt")
            );
        private DynamicLinkedList<Button> m_Buttons;

        public DiscordClient()
        {
            LoadChoices();
            while (Client == null)
            {
                Client = new DiscordRpcClient(FileIO.Load(
        FileIO.Configs + "appid.txt")[0]
        );
            }
            

            Client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

            Client.OnReady += (sender, e) =>
            {
                Console.WriteLine("Received Ready from user {0}", e.User.Username);
                
            };

            Client.OnPresenceUpdate += (sender, e) =>
            {
                Console.Title = e.Presence.Details;
            };

            Client.Initialize();
            m_Thread = new Thread(() => Update());
            m_Thread.Start();
        }

        private void Update()
        {
            while (Client != null)
            {
                UpdateRichPresence();
                WaitAMinute();     
            }
            Console.WriteLine("Discord Client Thread exited");
        }

        private void UpdateRichPresence() 
        {
            var _task = MackleLyricsApi.Get();

            if (_task.status == 200)
            {
                Client.SetPresence(new RichPresence()
                {

                    Details = _task.info.lyrics,
                    Buttons = new Button[]
                    {
                            m_Buttons.m_Next,
                            m_ShowAd ? m_AdButton : m_Buttons.m_Next,

                    },
                    State = _task.info.title,
                    Assets = new Assets
                    {
                        LargeImageKey = m_ImageList.m_Next,
                        LargeImageText = "",
                    },
                });
            }
        }

        private void WaitAMinute()
        {
            for (int i = 0; i < 60; i++) Thread.Sleep(1000);
        }

        private void LoadChoices()
        {
            var _lines = FileIO.Load(FileIO.Configs + "buttons.txt");
            var _list = new List<Button>();
            foreach (var item in _lines)
                if(!string.IsNullOrEmpty(item) && item.Contains("|"))
                {
                    var _line = item.Split('|');
                    _list.Add(Choice.Create(_line[0], _line[1]));
                }
            m_Buttons = new DynamicLinkedList<Button>(_list.ToArray());
        }

        public void Dispose()
        {
            if (Client != null)
            {
                Client.ClearPresence();
                Client.Deinitialize();
                Client.Dispose();
                Client = null;
            }
            //throw new NotImplementedException();
        }
    }
}
