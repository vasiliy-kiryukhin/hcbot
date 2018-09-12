using System;
using System.Collections.Generic;
using System.Text;

namespace HCBot.Runner.Schedule
{
    public interface ITrainingScheduleProvider
    {
        TrainingSchedule Load();
    }
}
