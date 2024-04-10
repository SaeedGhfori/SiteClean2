using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public static class DateTimeUtility
    {
        private static PersianCalendar persianCalendar = new PersianCalendar();
        private static IFormatProvider culture;

        //public static DateTime ConvertToGrigorian(string persianDate)
        //{
        //    var splitedDate = persianDate.Split('/');
        //    var year = Convert.ToInt32(splitedDate[0]);

        //    persianCalendar.TwoDigitYearMax = 1490;
        //    var fourDigitYear = persianCalendar.ToFourDigitYear(year);
        //    var month = int.Parse(splitedDate[1]);
        //    var day = int.Parse(splitedDate[2]);
        //    var grigorianDate = persianCalendar.ToDateTime(fourDigitYear, month, day, 0, 0, 0, 0);

        //    return grigorianDate;
        //}
        public static DateTime ToGeorgeDateTime(this string persianDate)
        {
            if (string.IsNullOrEmpty(persianDate))
            {
                throw new Exception(@"تاریخ ورودی معتبر نمی باشد");
            }

            if (persianDate.Trim().Length != 10)
            {
                if (persianDate.Trim().Length != 16)
                {
                    throw new Exception(@"تاریخ ورودی معتبر نمی باشد");
                }
            }

            var persianCalendar = new PersianCalendar();
            var separator = '/';
            if (persianDate.Contains("-"))
            {
                separator = '-';
            }
            else if (persianDate.Contains("/"))
            {
                separator = '/';
            }
            else
            {
                throw new Exception(@"تاریخ ورودی معتبر نمی باشد");
            }

            int hour = 0, minute = 0, second = 0;
            if (persianDate.Contains(":") && persianDate.Length == 16)
            {
                //1350/01/02 12:50
                var time = persianDate.Substring(11, 5);
                hour = Convert.ToInt32(time.Split(':')[0]);
                minute = Convert.ToInt32(time.Split(':')[1]);
                persianDate = persianDate.Substring(0, 10);
            }

            var year = Convert.ToInt32(persianDate.Split(separator)[0]);
            var month = Convert.ToInt32(persianDate.Split(separator)[1]);
            var day = Convert.ToInt32(persianDate.Split(separator)[2]);

            return new DateTime(year, month, day, hour, minute, second, persianCalendar);
        }
        public static DateTime ToGeorgeDateTime(string persianDate, int hour, int min)
        {
            if (string.IsNullOrEmpty(persianDate))
            {
                throw new Exception(@"تاریخ ورودی معتبر نمی باشد");
            }

            if (persianDate.Trim().Length != 10)
            {
                throw new Exception(@"تاریخ ورودی معتبر نمی باشد");
            }

            var persianCalendar = new PersianCalendar();
            var separator = '/';
            if (persianDate.Contains("-"))
            {
                separator = '-';
            }
            else if (persianDate.Contains("/"))
            {
                separator = '/';
            }
            else
            {
                throw new Exception(@"تاریخ ورودی معتبر نمی باشد");
            }

            var year = Convert.ToInt32(persianDate.Split(separator)[0]);
            var month = Convert.ToInt32(persianDate.Split(separator)[1]);
            var day = Convert.ToInt32(persianDate.Split(separator)[2]);

            return new DateTime(year, month, day, hour, min, 0, persianCalendar);
        }

        public static DateTime ToGeorgeDateTime(string persianDate, string time)
        {
            if (string.IsNullOrEmpty(persianDate))
            {
                throw new Exception(@"تاریخ ورودی معتبر نمی باشد");
            }

            if (persianDate.Trim().Length != 10)
            {
                throw new Exception(@"تاریخ ورودی معتبر نمی باشد");
            }

            var persianCalendar = new PersianCalendar();
            var separator = '/';
            if (persianDate.Contains("-"))
            {
                separator = '-';
            }
            else if (persianDate.Contains("/"))
            {
                separator = '/';
            }
            else
            {
                throw new Exception(@"تاریخ ورودی معتبر نمی باشد");
            }

            var year = Convert.ToInt32(persianDate.Split(separator)[0]);
            var month = Convert.ToInt32(persianDate.Split(separator)[1]);
            var day = Convert.ToInt32(persianDate.Split(separator)[2]);
            var hour = Convert.ToInt32(time.Split(':')[0]);
            var min = Convert.ToInt32(time.Split(':')[1]);
            return new DateTime(year, month, day, hour, min, 0, persianCalendar);
        }

        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthDifference = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthDifference);
        }

        public static string ConvertToJalali(this DateTime date)
        {
            var year = persianCalendar.GetYear(date);
            var month = persianCalendar.GetMonth(date);
            var day = persianCalendar.GetDayOfMonth(date);
            return string.Format("{0}/{1}/{2}", year, month, day);
        }
        public static string ConvertToJalali1(this DateTime date, DateTime date1)
        {
            var year = persianCalendar.GetYear(date1);
            var month = persianCalendar.GetMonth(date1);
            var day = persianCalendar.GetDayOfMonth(date1);
            return string.Format("{0}/{1}/{2}", year, month, day);
        }

        public static string PersianEightDigitDate(string currentDate)
        {
            var currentDateEvalution = currentDate.Split('/');
            var currentYear = currentDateEvalution[0];
            if (currentYear.Length == 4)
            {
                currentYear = currentYear.Substring(2, 2);
            }
            else if (currentYear.Length == 1)
            {
                currentYear = "0" + currentYear;
            }
            var currentMonth = currentDateEvalution[1];
            if (currentMonth.Length == 1)
            {
                currentMonth = "0" + currentMonth;
            }
            var currentDay = currentDateEvalution[2];
            if (currentDay.Length == 1)
            {
                currentDay = "0" + currentDay;
            }
            currentDate = currentYear + "/" + currentMonth + "/" + currentDay;
            return currentDate;
        }


        public static string PersianTenDigitDate(string currentDate)
        {
            if (currentDate == null || currentDate.Trim().Length == 0)
            {
                currentDate = DateTime.Now.ConvertToJalali();
            }


            var currentDateEvalution = currentDate.Split('/');
            var currentYear = currentDateEvalution[0];

            var currentMonth = currentDateEvalution[1];
            if (currentMonth.Length == 1)
            {
                currentMonth = "0" + currentMonth;
            }
            var currentDay = currentDateEvalution[2];
            if (currentDay.Length == 1)
            {
                currentDay = "0" + currentDay;
            }
            currentDate = currentYear + "/" + currentMonth + "/" + currentDay;
            return currentDate;


        }
        public static int GetDiffrenceOfSaturday(this DayOfWeek dayOfWeek)
        {
            int diff = 0;
            switch (dayOfWeek)
            {
                case DayOfWeek.Saturday:
                    diff = 0;
                    break;
                case DayOfWeek.Sunday:
                    diff = -1;
                    break;
                case DayOfWeek.Monday:
                    diff = -2;
                    break;
                case DayOfWeek.Tuesday:
                    diff = -3;
                    break;
                case DayOfWeek.Wednesday:
                    diff = -4;
                    break;
                case DayOfWeek.Thursday:
                    diff = -5;
                    break;
                case DayOfWeek.Friday:
                    diff = -6;
                    break;


            }
            return diff;
        }
        //public static DateTime Convertformate(this DateTime date)
        //{
        //   Convert.ToDateTime(date.ToString(),IForm)
        //    DateTime dateVal = DateTime..ParseExact(date.Date.ToString(), "yyyy-MM-dd", culture);
        //    return dateVal;
        //}

    }
}
