﻿using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.Linq;
using HCBot.Runner.States;
using HCBot.Runner.Menu;

namespace HCBot.Runner.Commands
{
    public class ShowMenuCommand : CommandBase, ICommand
    {
        public bool AllowBackward { get; set; } = true;
        public void ExecuteCommand(ITelegramBotClient bot, Chat chat, UserStateBag user)
        {
            var returnKeyboardMenu = new ReplyKeyboardMarkup();
            var back = AllowBackward ? new KeyboardButton[] { new KeyboardButton("Вернуться") } : new KeyboardButton[] { };
            if (Position != null)
            {
                var menu = user.BotMenu;
                IEnumerable<IEnumerable<KeyboardButton>> btns =
                    Position.SubMenu
                    .Select(m => new KeyboardButton(m.Text))
                    .Union(back)
                    .Select(b => new List<KeyboardButton>() { b });

                menu.currentPosition = Position;
                returnKeyboardMenu.Keyboard = btns;

                bot.SendTextMessageAsync(chat.Id, "Выберите пункт меню", replyMarkup: returnKeyboardMenu);
            }
            user.UserState = UserBotState.SerfMenu;
        }
    }
}
