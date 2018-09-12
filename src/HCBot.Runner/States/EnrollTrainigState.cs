using HCBot.Runner.Commands;
using HCBot.Runner.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.Extensions.DependencyInjection;

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

                var training = schedule.Trainigs.FirstOrDefault(t => t.Location.Name == location && t.FutureTraning.ToShortDateString() == date && t.FutureTraning.ToShortTimeString() == time);
                if (training!=null)
                {
                    bot.SendTextMessageAsync(chat.Id, "Вы записаны на тренировку "+ training.ToString());
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
