using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MySportsClub.Infrastructure {
    public static class DateTimeExtensions {
        public static DateTime NextDayAt(this DateTime dt, DayOfWeek dayOfWeek, int hour, int minutes) {
            int diff = (dayOfWeek >= dt.DayOfWeek) ? (dayOfWeek - dt.DayOfWeek) : (7 + (dayOfWeek - dt.DayOfWeek));
            DateTime day = dt.AddDays(diff).Date;
            DateTime nextDayAt = day.AddHours(hour).AddMinutes(minutes);
            return nextDayAt;
        }
    }
}
