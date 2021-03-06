﻿using HCBot.Runner.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HCBot.Runner.Schedule
{
    public enum TrainingType
    {
        Amateur, Youth
    }

    public class TrainingLocation
    {
        public string Name { get; set; }
        public string Adress { get; set; }
    }

    public class Training
    {
        public TrainingLocation Location { get; set; }
        public TrainingType TrainingType { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }
        public DayOfWeek TrainingDayOfWeek { get; set; }

        public DateTime FutureTraning { get
            {                
                var today = DateTimeHelper.MoscowNow.Date.DayOfWeek;
                if (today <= TrainingDayOfWeek) {                    
                    //Start on week
                    var thisWeek = DateTimeHelper.MoscowNow.Date.AddDays(-1*(int)DateTimeHelper.MoscowNow.DayOfWeek);              
                    return thisWeek.AddDays((int)TrainingDayOfWeek).Add(TimeFrom);
                }else
                {
                    //Start on next week 
                    var nextWeek = DateTimeHelper.MoscowNow.Date.AddDays(-1 * (int)DateTimeHelper.MoscowNow.DayOfWeek).AddDays(7);
                    return nextWeek.AddDays((int)TrainingDayOfWeek).Add(TimeFrom);
                }

            }
        }

        public override string ToString()
        {
            return Location.Name + " " + FutureTraning.ToShortDateString() + " " + FutureTraning.ToShortTimeString();
        }
    }

    public class TrainingSchedule
    {
        public List<Training> Trainigs = new List<Training>();
       
        public static TimeSpan GetFrom(string timeString)
        {
            return new TimeSpan(int.Parse(timeString.Split('-')[0].Split(':')[0]), int.Parse(timeString.Split('-')[0].Split(':')[1]), 0);
        }
        public static TimeSpan GetTo(string timeString)
        {
            return new TimeSpan(int.Parse(timeString.Split('-')[1].Split(':')[0]), int.Parse(timeString.Split('-')[1].Split(':')[1]), 0);
        }
        public static TrainingType GetTrainingType(string name)
        {
            switch (name)
            {
                case "Взрослая": return TrainingType.Amateur;
                case "Детская": return TrainingType.Youth;
                default:
                    return TrainingType.Amateur;
            }
        }
        public static DayOfWeek GetDayOfWeek(string name)
        {
            switch (name)
            {
                case "Понедельник": return DayOfWeek.Monday;
                case "Вторник": return DayOfWeek.Tuesday;
                case "Среда": return DayOfWeek.Wednesday;
                case "Четверг": return DayOfWeek.Thursday;
                case "Пятница": return DayOfWeek.Friday;
                case "Суббота": return DayOfWeek.Saturday;
                case "Воскресенье": return DayOfWeek.Sunday;
                default:
                     return DayOfWeek.Sunday;
            }
        }
    }

}
