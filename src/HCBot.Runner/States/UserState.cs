using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace HCBot.Runner.States
{
    public enum UserBotState
    {
        SerfMenu, Enroll
    }

    public abstract class UserState
    {
        public abstract void ProceedState(ITelegramBotClient bot, UserStateBag user, Chat chat, string commandName);
    }
}
