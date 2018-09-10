using HCBot.Runner.Schedule;
using HCBot.Runner.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace HCBot.Runner
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
        private Dictionary<long, UserStateBag> user = new Dictionary<long, UserStateBag>();


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Task.Run(() =>
            {
                var p = new Program();
                p.Run();
            }).Wait();


        }

        void Run()
        {
            var bot = new TelegramBotClient(botKey);
          

            bot.StartReceiving();
            bot.OnMessage += Bot_OnMessage;

            Console.ReadKey();

            bot.StopReceiving();
        }


        private void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {

            

            ExecuteCommand(sender as TelegramBotClient, e.Message.Chat, e.Message.Text);
           

            Console.WriteLine(e.Message);
        }

        private void ExecuteCommand(ITelegramBotClient bot, Chat chat, string commandName)
        {
            if (!user.ContainsKey(chat.Id))
            {
                user.Add(chat.Id, new UserStateBag { UserState = UserBotState.SerfMenu, BotMenu = BotMenu.LoadFromFile("Structure.json") });
            }

            new StateFactory().CreateState(user[chat.Id].UserState).ProceedState(bot, user[chat.Id], chat, commandName);
        }
    }
}
