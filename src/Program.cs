using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HCBot
{
    class Program
    {
        /// <summary>
        /// HCBot
        /// HC_dev_bot
        /// 665509241:AAFxRlJKv4E-NoYMZvGWuu7o7OleN9a-4hI
        /// </summary>

        public const string tgUrlBase = "https://api.telegram.org/";
        public const string botKey = "665509241:AAFxRlJKv4E-NoYMZvGWuu7o7OleN9a-4hI";
        public const string apiBotKey = "bot" + botKey;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Task.Run(async () =>
            {
                var p = new Program();
                var me = await p.GetMe();
                Console.WriteLine(me.ToString());
                Console.ReadKey();
            }).Wait();


        }

        public async Task<User> GetMe()
        {
            var http = new HttpClient();
            var url = string.Concat(tgUrlBase, apiBotKey, "/getMe");
            var r = await http.GetStringAsync(url);
            var rr = JObject.Parse(r)["result"].ToString();
            var me = JsonConvert.DeserializeObject<User>(rr);
            return me;
        }

        public async Task<string> GetUpdates()
        {
            var http = new HttpClient();
            var url = string.Concat(tgUrlBase, apiBotKey, "/getUpdates");
            var r = await http.GetStringAsync(url);
            return r;
        }
    }
}
