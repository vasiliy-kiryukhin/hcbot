using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using HCBot.Runner.Schedule;
using HCBot.Runner.Utils;
using Npgsql;

namespace HCBot.Runner.Data
{
    public class EnrollPgSqlRepository : IEnrollRepository
    {
        public bool IsEnrolled(string uid, DateTime date, TrainingType trainingType, string location)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select count(*) from enrollment where userid=@uid and enroll_date=@date and location=@location and trainingtype=@trainingtype and positive=True";                    
                    cmd.Parameters.AddWithValue("uid", uid);
                    cmd.Parameters.AddWithValue("date", date);
                    cmd.Parameters.AddWithValue("location", location);
                    cmd.Parameters.AddWithValue("trainingtype", trainingType == TrainingType.Amateur ? 0 : 1);

                    var count = (Int64)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public IEnumerable<string> LoadEnrollList(DateTime date, TrainingType trainingType, string location)
        {
            var list = new List<string>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select * from enrollment where enroll_date=@date and location=@location and trainingtype=@trainingtype and positive=True";
                    cmd.Parameters.AddWithValue("date", date);
                    cmd.Parameters.AddWithValue("location", location);
                    cmd.Parameters.AddWithValue("trainingtype", trainingType==TrainingType.Amateur?0:1);

                    using (var reader = cmd.ExecuteReader())
                    while(reader.Read())
                    {
                            var uid = reader.GetString(0);
                            var displayName = reader.GetString(4);
                            list.Add(!string.IsNullOrEmpty(displayName) ? displayName : uid);                            
                    }
                }
            }

            if (list.Count() > 0)
            {
                var numbers = Enumerable.Range(1, list.Count()).Select(n => n.ToString());
                return list.Zip(numbers, (name, n) => string.Concat(n, ".", " ", name));
            }

            return new List<string>();
        }

        public void SaveEnrollment(string uid, DateTime date, TrainingType trainingType, string location, string displayName, bool alreadyEnrolled)
        {
           
            using (var connection = GetConnection())
            {               
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("uid", uid);
                    cmd.Parameters.AddWithValue("enroll_date", date);
                    cmd.Parameters.AddWithValue("trainingtype", trainingType == TrainingType.Amateur ? 0 : 1);
                    cmd.Parameters.AddWithValue("location", location);

                    if (alreadyEnrolled)
                    {
                        cmd.CommandText = "update enrollment set positive=@positive where userid=@uid and enroll_date=@enroll_date and location=@location and trainingtype=@trainingtype";
                        cmd.Parameters.AddWithValue("positive", !alreadyEnrolled);
                    }
                    else
                    {
                        cmd.CommandText = "insert into enrollment values(@uid, @enroll_date, @trainingtype, @location, @displayName, @positive, @rectimestamp)";
                        cmd.Parameters.AddWithValue("displayName", displayName);
                        cmd.Parameters.AddWithValue("positive", !alreadyEnrolled);
                        cmd.Parameters.AddWithValue("rectimestamp", DateTimeHelper.MoscowNow);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private NpgsqlConnection GetConnection()
        {
            var cnnStringEnv = Environment.GetEnvironmentVariable("DATABASE_URL");
            var cnnString = cnnStringEnv.Remove(0, 11);           
            var userName = cnnString.Split("@")[0].Split(":")[0];
            var userPassword = cnnString.Split("@")[0].Split(":")[1];
            var host = cnnString.Split("@")[1].Split(":")[0];
            var dbName = cnnString.Split("@")[1].Split(":")[1].Split("/")[1];
            return new NpgsqlConnection($"Host={host};Username={userName};Password={userPassword};Database={dbName};SSL Mode=Require; Trust Server Certificate=true");
        }
    }
}
