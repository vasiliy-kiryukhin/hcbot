using HCBot.Runner.Schedule;
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
    public class EnrollAmateurCommand : ICommand
    {
        public BotMenuItem Position { get; set; }

        public void ExecuteCommand(ITelegramBotClient bot, Chat chat, UserStateBag user)
        {
            var schedule = TrainingSchedule.LoadFromFile("Schedule.csv");

            var returnKeyboardMenu = new ReplyKeyboardMarkup();
            var back = new KeyboardButton[] { new KeyboardButton("Вернуться") } ;
            
            IEnumerable<IEnumerable<KeyboardButton>> btns =
                schedule.Trainigs.FindAll(t=>t.TrainingType == TrainingType.Amateur).OrderBy(t => t.FutureTraning)
                .Select(t => new KeyboardButton(t.Location.Name+" "+t.FutureTraning.ToShortDateString()+ " "+ t.FutureTraning.ToShortTimeString()))
                .Union(back)
                .Select(b => new List<KeyboardButton>() { b });

            returnKeyboardMenu.Keyboard = btns;

            bot.SendTextMessageAsync(chat.Id, "Choose!", replyMarkup: returnKeyboardMenu);
            user.UserState = UserBotState.Enroll;
        }
    }
}
