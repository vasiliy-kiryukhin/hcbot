using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace HCBot.Runner.States
{
    public class SerfMenuState : UserState
    {
        public override void ProceedState(ITelegramBotClient bot, UserStateBag user, Chat chat, string commandName)
        {
            var cmd = user.BotMenu.GetCommand(commandName);
            cmd.ExecuteCommand(bot, chat, user);
        }
    }
}
