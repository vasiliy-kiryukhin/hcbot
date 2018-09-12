using HCBot.Runner.Schedule;
using HCBot.Runner.States;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        public ManualResetEvent botHaltEvent = new ManualResetEvent(false);

        ILogger logger;

        static void Main(string[] args)
        {

            Task.Run(() =>
            {
                var p = new Program();
                p.Run();

            }).Wait();
            

        }

        void Run()
        {
            ILoggerFactory loggerFactory =
                new LoggerFactory()
                    .AddConsole()
                    .AddDebug();
            logger = loggerFactory.CreateLogger<Program>();

            logger.LogInformation("HCbot started");

            var bot = new TelegramBotClient(botKey);
                               
            logger.LogInformation("HCbot awaiting halt event");
            try
            {
                bot.StartReceiving();
                bot.OnMessage += Bot_OnMessage;
                botHaltEvent.WaitOne();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error while WaitOne");
            }
            finally
            {
                bot.StopReceiving();
                logger.LogInformation("HCbot halt");
            }
        }


        private void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            logger.LogInformation($"Message received from {e.Message.Chat.Id} : {e.Message.Text}");
            ExecuteCommand(sender as TelegramBotClient, e.Message.Chat, e.Message.Text);            
        }

        private void ExecuteCommand(ITelegramBotClient bot, Chat chat, string commandName)
        {
            if (commandName=="halt")
            {
                logger.LogInformation($"Setting halt event");
                botHaltEvent.Set();
                return;
            }
            if (!user.ContainsKey(chat.Id))
            {
                user.Add(chat.Id, new UserStateBag { UserState = UserBotState.SerfMenu, BotMenu = BotMenu.LoadFromFile("Structure.json") });
            }

            new StateFactory().CreateState(user[chat.Id].UserState).ProceedState(bot, user[chat.Id], chat, commandName);
        }
    }
}
