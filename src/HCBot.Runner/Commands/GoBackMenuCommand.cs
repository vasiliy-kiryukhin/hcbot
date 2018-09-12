using HCBot.Runner.Menu;
using HCBot.Runner.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HCBot.Runner.Commands
{
    public class GoBackMenuCommand : CommandBase, ICommand
    { 
        public void ExecuteCommand(ITelegramBotClient bot, Chat chat, UserStateBag user)
        {
            var menu = user.BotMenu;
            menu.currentPosition = menu.GetPrevPosition();
            new ShowMenuCommand { Position = menu.currentPosition }.ExecuteCommand(bot, chat, user);
        }
    }
}
