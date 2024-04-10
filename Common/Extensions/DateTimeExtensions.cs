using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime birthdate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthdate.Year;
            if (birthdate.Date > today.AddYears(-age)) age--;
            return age;
        }

        public static DateTime AddBusinessDays(this DateTime source, int days)
        {
            // افزودن روزهای کاری به تاریخ
            var sign = Math.Sign(days);
            var unsignedDays = Math.Abs(days);
            for (var i = 0; i < unsignedDays; i++)
            {
                do
                {
                    source = source.AddDays(sign);
                }
                while (source.DayOfWeek == DayOfWeek.Saturday ||
                       source.DayOfWeek == DayOfWeek.Sunday);
            }
            return source;
        }

    }
}
