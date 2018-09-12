using HCBot.Runner.Menu;
using HCBot.Runner.Schedule;
using HCBot.Runner.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.Extensions.DependencyInjection;


namespace HCBot.Runner.Commands
{
    public class EnrollAmateurCommand : CommandBase, ICommand
    {
        public void ExecuteCommand(ITelegramBotClient bot, Chat chat, UserStateBag user)
        {
            var schedule = ServiceProvider.GetRequiredService<ITrainingScheduleProvider>().Load();
            var returnKeyboardMenu = new ReplyKeyboardMarkup();
            var back = new KeyboardButton[] { new KeyboardButton("Вернуться") } ;

            IEnumerable<IEnumerable<KeyboardButton>> btns =
                schedule.Trainigs.FindAll(t => t.TrainingType == TrainingType.Amateur).OrderBy(t => t.FutureTraning)
                .Select(t => 
                    new KeyboardButton(t.Location.Name + " " + t.FutureTraning.Date.ToString("dd.MM.yyyy") + " " + t.FutureTraning.TimeOfDay.ToString(@"hh\:mm")))
                .Union(back)
                .Select(b => new List<KeyboardButton>() { b });

            returnKeyboardMenu.Keyboard = btns;

            bot.SendTextMessageAsync(chat.Id, "Выберите пункт меню", replyMarkup: returnKeyboardMenu);
            user.UserState = UserBotState.Enroll;
        }
    }
}
