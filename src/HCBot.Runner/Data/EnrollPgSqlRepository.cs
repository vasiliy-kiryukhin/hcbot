using System;
using System.Collections.Generic;
using System.Text;
using HCBot.Runner.Schedule;
using Npgsql;

namespace HCBot.Runner.Data
{
    public class EnrollPgSqlRepository : IEnrollRepository
    {
        public IEnumerable<string> LoadEnrollList(DateTime date, TrainingType trainingType, string location)
        {
            var cnnString = Environment.GetEnvironmentVariable("DATABASE_URL").Remove(0, 11);
            //postgres:// fgnspgwqjrwemx:78738eabb44cfda3f74d09f8cde237b0d34c68cef615ad8afcf2ae4d416c9400@ec2-54-227-243-210.compute-1.amazonaws.com:5432/d5mu209f47ei1h
            var userName = cnnString.Split("@")[0].Split(":")[0];
            var userPassword = cnnString.Split("@")[0].Split(":")[1];
            var host = cnnString.Split("@")[1].Split(":")[0];
            var dbName = cnnString.Split("@")[1].Split(":")[1].Split("/")[1];
            using (var connection = new NpgsqlConnection($"Host={host};Username={userName};Password={userPassword};Database={dbName}"))
            {
                connection.Open();
                using(var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select * from enrollment where enroll_date=@date and location=@location and trainingtype=@trainingtype and positive=True";
                    cmd.Parameters.AddWithValue("date", date);
                    cmd.Parameters.AddWithValue("location", location);
                    cmd.Parameters.AddWithValue("trainingtype", trainingType==TrainingType.Amateur?0:1);

                    using (var reader = cmd.ExecuteReader())
                    {
                        reader.
                    }

                }
            }

        }

        public void SaveEnrollment(string uid, DateTime date, TrainingType trainingType, string location, string displayName, bool positive)
        {
            throw new NotImplementedException();
        }
    }
}
