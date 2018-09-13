using HCBot.Runner.Commands;
using HCBot.Runner.Data;
using HCBot.Runner.Schedule;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace HCBot.Runner.States
{
    public class ShowEnrollState : UserState
    {
        public ShowEnrollState(IServiceProvider sp) : base(sp)
        {
        }

        public override void ProceedState(ITelegramBotClient bot, UserStateBag user, Chat chat, string commandName)
        {
            if (commandName == "Вернуться")
            {
                new GoBackMenuCommand().ExecuteCommand(bot, chat, user);
                return;
            }

            var entries = commandName.Split(" ");

            if (entries?.Count() == 3)
            {
                var location = entries[0];
                var date = entries[1];
                var time = entries[2];

                var schedule = ServiceProvider.GetRequiredService<ITrainingScheduleProvider>().Load();
                var repo = ServiceProvider.GetRequiredService<IEnrollRepository>();

                var training = schedule.Trainigs.FirstOrDefault(t => t.Location.Name == location && t.FutureTraning.Date.ToString("dd.MM.yyyy") == date && t.FutureTraning.TimeOfDay.ToString(@"hh\:mm") == time);
                if (training != null)
                {
                    var list = repo.LoadEnrollList(training.FutureTraning, training.TrainingType, training.Location.Name);
                    var msg = list.Count() > 0 ? (training.ToString() + Environment.NewLine + string.Join(Environment.NewLine, list)) : "На тренировку пока никто не записан";
                    bot.SendTextMessageAsync(chat.Id, msg);
                }
            }
            user.UserState = UserBotState.ShowEnroll;
        }
    }
}
