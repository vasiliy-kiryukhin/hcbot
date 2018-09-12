using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HCBot.Runner.Schedule
{
    public class FlatFileTrainingScheduleProvider : ITrainingScheduleProvider
    {
        readonly string filePath;
        public FlatFileTrainingScheduleProvider(string path)
        {
            filePath = path;
        }
        public TrainingSchedule Load()
        {
            var schedule = new TrainingSchedule();
            var scheduleCsv = File.ReadAllLines(Path.Combine(filePath, "Schedule.csv"));
            schedule.Trainigs.AddRange(
            scheduleCsv.Select(l => new Training
            {
                TrainingDayOfWeek = TrainingSchedule.GetDayOfWeek(l.Split(',')[0]),
                Location = new TrainingLocation { Name = l.Split(',')[1] },
                TrainingType = TrainingSchedule.GetTrainingType(l.Split(',')[2]),
                TimeFrom = TrainingSchedule.GetFrom(l.Split(',')[3]),
                TimeTo = TrainingSchedule.GetTo(l.Split(',')[3])
            }).ToList());
            return schedule;
        }
       
    }
}
