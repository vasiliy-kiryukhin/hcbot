using HCBot.Runner.Schedule;
using System;
using System.Collections.Generic;
using System.Text;

namespace HCBot.Runner.Data
{
    public interface IEnrollRepository
    {
        IEnumerable<string> LoadEnrollList(DateTime date, TrainingType trainingType, string location);
        void SaveEnrollment(string uid, DateTime date, TrainingType trainingType, string location, string displayName, bool positive);
    }
}
