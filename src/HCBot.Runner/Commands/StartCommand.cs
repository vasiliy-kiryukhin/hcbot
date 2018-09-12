using HCBot.Runner.Menu;
using HCBot.Runner.States;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HCBot.Runner.Commands
{
    public class StartCommand : CommandBase, ICommand
    {
        public void ExecuteCommand(ITelegramBotClient bot, Chat chat, UserStateBag user)
        {
            new ShowMenuCommand { Position = user.BotMenu.root.SubMenu[0], AllowBackward = false }.ExecuteCommand(bot, chat, user);
        }
    }
}
