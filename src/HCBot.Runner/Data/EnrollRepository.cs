using HCBot.Runner.Schedule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HCBot.Runner.Data
{
    public class EnrollRepository : IEnrollRepository
    {
        readonly string dataPath;

        public EnrollRepository(string dataPath)
        {
            this.dataPath = dataPath;
        }
        public void SaveEnrollment(string uid, DateTime date, TrainingType trainingType, string location, string displayName, bool positive)
        {
            var filePath = Path.Combine(dataPath, date.ToString("yyyyMMdd"));
            var data = string.Join(",", uid, date.ToShortTimeString(), location, trainingType.ToString(), displayName, positive ? "1" : "0", DateTime.UtcNow.ToString());
            File.AppendAllLines(filePath, new string[] { data });
        }

        public IEnumerable<string> LoadEnrollList(DateTime date, TrainingType trainingType, string location)
        {
            var filePath = Path.Combine(dataPath, date.ToString("yyyyMMdd"));
            var fullDayEnrolment = File.ReadAllLines(filePath);

            var list = fullDayEnrolment
            .Select(e =>
            {
                var enrollData = e.Split(",");
                return new { Uid = enrollData[0], Time = enrollData[1], Location = enrollData[2], TrainingType = enrollData[3], DisplayName = enrollData[4], Positive = enrollData[5] == "1" ? true : false, Timestamp = enrollData[6] };
            })
            .GroupBy(en => en.Uid)
            .Select(g => g.Single(en => en.Timestamp == g.Max(t => t.Timestamp) && en.Positive && en.Time == date.ToShortTimeString()))
            .Select(en => en.DisplayName);

            if (list.Count() > 0)
            {
                var numbers = Enumerable.Range(1, list.Count()).Select(n => n.ToString());
                return list.Zip(numbers, (name, n) => string.Concat(n, ".", " ", name));
            }

            return new List<string>();

        }
    }
}
