using HCBot.Runner.States;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace HCBot.Runner.Commands
{
    public interface ICommand
    {
        BotMenuItem Position { get; set;  }
        void ExecuteCommand(ITelegramBotClient bot, Chat chat, UserStateBag user);
    }
}
