using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

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
                var bot = new TelegramBotClient(botKey);

                var me = await bot.GetMeAsync();

                bot.StartReceiving();
                bot.OnMessage += Bot_OnMessage;

               

                Console.WriteLine(me.ToString());
                Console.ReadKey();

                bot.StopReceiving();
            }).Wait();


        }

        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Entities!=null && e.Message.Entities.Count()==1)
            {
                var entity  = e.Message.Entities[0];
                if (entity.Type is MessageEntityType.BotCommand)
                {
                    ExecuteCommand(sender as TelegramBotClient, e.Message.Chat, e.Message.Text);
                }
            }
            Console.WriteLine(e.Message);
        }

        private static void ExecuteCommand(TelegramBotClient bot, Chat chat, string commandName)
        {
            switch (commandName)
            {
                case "/start":
                    var k = new ReplyKeyboardMarkup(new KeyboardButton[] { new KeyboardButton("/one"), new KeyboardButton("/two"), new KeyboardButton("/three"), new KeyboardButton("/four")}));
                    bot.SendTextMessageAsync(chat.Id, "Choose!", replyMarkup: k);
                    break;
                default:
                    break;
            }
        }
    }
}
