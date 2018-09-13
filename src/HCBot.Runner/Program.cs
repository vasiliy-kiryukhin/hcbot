using HCBot.Runner.Data;
using HCBot.Runner.Menu;
using HCBot.Runner.Schedule;
using HCBot.Runner.States;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

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
        IServiceProvider Services { get; set; }
        IServiceCollection serviceCollection;
        ILogger logger;

        static void Main(string[] args)
        {
            string path;
            if (args.Count()!=1)
            {
                path = string.Empty;
            }
            else
            {
                path = args[0];
            }
            
            Task.Run(() => 
            {
                var p = new Program();
                p.Run(path);

            }).Wait();           
        }

        void Run(string datadir)
        {
            serviceCollection = new ServiceCollection();
            
            ILoggerFactory loggerFactory =
                new LoggerFactory()
                    .AddConsole()
                    .AddDebug();
            logger = loggerFactory.CreateLogger<Program>();

            serviceCollection.AddSingleton<ILogger>(logger);
            serviceCollection.AddScoped<IMenuProvider>( sp => new FlatFileMenuProvider(datadir));
            serviceCollection.AddScoped<ITrainingScheduleProvider>(sp => new FlatFileTrainingScheduleProvider(datadir));
            //serviceCollection.AddScoped<IEnrollRepository>(sp => new EnrollFlatFileRepository(datadir));
            serviceCollection.AddScoped<IEnrollRepository>(sp => new EnrollPgSqlRepository());
           Services = serviceCollection.BuildServiceProvider();

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
            ExecuteCommand(sender as TelegramBotClient, e.Message.From, e.Message.Chat, e.Message.Text);            
        }

        private void ExecuteCommand(ITelegramBotClient bot, User from, Chat chat, string commandName)
        {
            var uid = from?.Id ?? chat?.Id;
            if (commandName=="halt")
            {
                logger.LogInformation($"Setting halt event");
                botHaltEvent.Set();
                return;
            }
            if (!user.ContainsKey(uid.Value))
            {
                var menu = Services.GetRequiredService<IMenuProvider>().Load();
                user.Add(uid.Value, new UserStateBag { Uid=uid.Value, UserState = UserBotState.SerfMenu, BotMenu = menu });
            }

            new StateFactory(Services).CreateState(user[uid.Value].UserState).ProceedState(bot, user[chat.Id], chat, commandName);
        }
    }
}
