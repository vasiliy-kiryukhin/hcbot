using System;
using System.Collections.Generic;
using System.Text;

namespace HCBot.Runner.Utils
{
    public static class DateTimeHelper
    {
        public static DateTime MoscowNow { get {
                var mskTz = TimeZoneInfo.CreateCustomTimeZone("Msk", new TimeSpan(3, 0, 0), "Msk", "Msk");
                var mskDt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, mskTz);
                return mskDt;
            }
        }
        public static DateTime UtcToMsk(DateTime utcDateTime)
        {
            var mskTz = TimeZoneInfo.CreateCustomTimeZone("Msk", new TimeSpan(3, 0, 0), "Msk", "Msk");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, mskTz);
        }
    }
}
