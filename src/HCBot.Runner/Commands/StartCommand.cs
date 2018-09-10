﻿using HCBot.Runner.States;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HCBot.Runner.Commands
{
    public class StartCommand : ICommand
    {
        public BotMenuItem Position { get; set; }

        public void ExecuteCommand(ITelegramBotClient bot, Chat chat, UserStateBag user)
        {
            var menu = user.BotMenu;
            menu.currentPosition = Position; 
            new ShowMenuCommand { Position = Position, AllowBackward = false }.ExecuteCommand(bot, chat, user);
        }
    }
}