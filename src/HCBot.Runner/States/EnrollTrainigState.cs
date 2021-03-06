﻿using HCBot.Runner.Commands;
using HCBot.Runner.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.Extensions.DependencyInjection;
using HCBot.Runner.Data;

namespace HCBot.Runner.States
{
    public class EnrollTrainigState : UserState
    {
        public EnrollTrainigState(IServiceProvider sp) : base(sp)
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

            if (entries?.Count()==3)           
            {
                var location = entries[0];
                var date = entries[1];
                var time = entries[2];

                var schedule = ServiceProvider.GetRequiredService<ITrainingScheduleProvider>().Load();

                var training = schedule.Trainigs.FirstOrDefault(t => t.Location.Name == location && t.FutureTraning.Date.ToString("dd.MM.yyyy") == date && t.FutureTraning.TimeOfDay.ToString(@"hh\:mm") == time);
                if (training!=null)
                {
                   
                    var repo = ServiceProvider.GetRequiredService<IEnrollRepository>();
                    var displayName = !string.IsNullOrEmpty(chat.Username) ? chat.Username : 
                                (!string.IsNullOrEmpty(chat.FirstName) ? string.Concat(chat.FirstName," ",chat.LastName) : user.Uid.ToString());

                    var alreadyEnrolled = repo.IsEnrolled(user.Uid.ToString(), training.FutureTraning, training.TrainingType, training.Location.Name);
                    repo.SaveEnrollment(user.Uid.ToString(), training.FutureTraning, training.TrainingType, training.Location.Name, displayName, alreadyEnrolled);

                    var list = repo.LoadEnrollList(training.FutureTraning, training.TrainingType, training.Location.Name);

                    var msg = !alreadyEnrolled ? "Вы записаны на тренировку " : "Вы отменили запись на тренировку ";
                    bot.SendTextMessageAsync(chat.Id, msg + training.ToString() + Environment.NewLine+ string.Join(Environment.NewLine, list));
                }
                else
                {
                    bot.SendTextMessageAsync(chat.Id, "Указанная тренировка не найдена");
                }
            }
            else
            {
                bot.SendTextMessageAsync(chat.Id, "Не распознанная команда");
            }

            new StartCommand().ExecuteCommand(bot, chat, user);
            return;
        }
    }
}
