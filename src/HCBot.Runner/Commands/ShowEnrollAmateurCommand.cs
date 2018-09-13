using System;
using System.Collections.Generic;
using System.Text;
using HCBot.Runner.Menu;
using HCBot.Runner.States;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace HCBot.Runner.Commands
{
    public class ShowEnrollAmateurCommand : ICommand
    {
        public BotMenuItem Position { get ; set ; }
        public IServiceProvider ServiceProvider { get; set; }

        public void ExecuteCommand(ITelegramBotClient bot, Chat chat, UserStateBag user)
        {
            new EnrollAmateurCommand() { ServiceProvider = ServiceProvider }.ExecuteCommand(bot, chat, user);
            user.UserState = UserBotState.ShowEnroll;
        }
    }
}
