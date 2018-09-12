using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.Extensions.DependencyInjection;

namespace HCBot.Runner.States
{
    public enum UserBotState
    {
        SerfMenu, Enroll
    }

    public abstract class UserState
    {
        protected readonly IServiceProvider ServiceProvider;
        public UserState(IServiceProvider sp)
        {
            ServiceProvider = sp;
        }
        public abstract void ProceedState(ITelegramBotClient bot, UserStateBag user, Chat chat, string commandName);
    }
}
