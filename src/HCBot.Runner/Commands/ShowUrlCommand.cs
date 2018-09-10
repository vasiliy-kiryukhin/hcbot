using System;
using System.Collections.Generic;
using System.Text;
using HCBot.Runner.States;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace HCBot.Runner.Commands
{
    public class ShowUrlCommand : ICommand
    {
        public BotMenuItem Position { get ; set ; }

        public void ExecuteCommand(ITelegramBotClient bot, Chat chat, UserStateBag user)
        {            
            bot.SendTextMessageAsync(chat.Id, "Ознакомится с дополнительной информацией можно по ссылкам:");
            foreach(var url in Position.Urls)
            {
                bot.SendTextMessageAsync(chat.Id, url);
            }
        }
    }
}
