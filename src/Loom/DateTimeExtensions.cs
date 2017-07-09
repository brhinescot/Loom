#region Using Directives

using System;
using System.Globalization;

#endregion

namespace Loom
{
    public static class DateTimeExtensions
    {
        private const string Space = " ";

        private static readonly string[] FuzzyHours =
        {
            "midnight", "one", "two", "three", "four", "five", "six", "seven", "eight",
            "nine", "ten", "eleven", "noon", "one", "two", "three", "four", "five", "six",
            "seven", "eight", "nine", "ten", "eleven"
        };

        private static readonly string[] FuzzyMinutes = {"five", "ten", "a quarter", "twenty", "twenty five", "half"};
        private static readonly string[] Suffixes = {"th", "st", "nd", "rd"};

        public static string ToDateTimeWords(this DateTime date)
        {
            return ToDateTimeWordsPrivate(DateTime.Now, date);
        }

        public static string ToDateTimeWords(this DateTime dateTime, DateTime currentDate)
        {
            return ToDateTimeWordsPrivate(dateTime, currentDate);
        }

        public static string ToDateWords(this DateTime date)
        {
            return ToDateWordsPrivate(date, DateTime.Now);
        }

        public static string ToDateWords(this DateTime date, DateTime currentDate)
        {
            return ToDateWordsPrivate(date, currentDate);
        }

        public static string ToTimeWords(this DateTime time)
        {
            return ToTimeWordsPrivate(time);
        }

        /// <summary>
        ///     Breakdowns the specified start time.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns></returns>
        public static string ToElapsedTime(this DateTime startTime, DateTime endTime)
        {
            return ToElapsedTimePrivate(startTime, endTime);
        }

        private static string FormatBreakdown(string value, string previousValue, int number)
        {
            if (previousValue == null)
                previousValue = string.Empty;

            if (number <= 0 && previousValue.Length == 0) return string.Empty;

            value = number == 1 ? string.Concat(number, Space, value) : string.Concat(number, Space, value, "s");

            return string.Format(CultureInfo.InvariantCulture, "{0} ", value);
        }

        private static string GetOrdinal(int value)
        {
            int tenth = value % 10;

            if (tenth >= Suffixes.Length)
                return Suffixes[0];

            // special case for 11, 12, 13
            int hundredth = value % 100;
            if (hundredth >= 11 && hundredth <= 13)
                return Suffixes[0];

            return Suffixes[tenth];
        }

        private static string GetPeriod(int hour)
        {
            if (hour > 18)
                return "evening";

            if (hour > 12)
                return "afternoon";

            if (hour > 3)
                return "morning";

            return "night";
        }

        private static string ToDateTimeWordsPrivate(DateTime dateTime, DateTime currentDate)
        {
            string result;
            TimeSpan t1 = new TimeSpan(currentDate.Ticks);
            TimeSpan t2 = new TimeSpan(dateTime.Ticks);

            int daysElapsed = t1.Days - t2.Days;
            if (daysElapsed < -7 || daysElapsed >= 14)
                result = ToDateWords(dateTime, currentDate);
            else if (daysElapsed == 0)
                result = "this " + GetPeriod(dateTime.Hour);
            else
                result = ToDateWords(dateTime, currentDate) + " " + GetPeriod(dateTime.Hour);

            return result + " at " + ToTimeWords(dateTime);
        }

        private static string ToDateWordsPrivate(DateTime date, DateTime currentDate)
        {
            TimeSpan t1 = new TimeSpan(currentDate.Ticks);
            TimeSpan t2 = new TimeSpan(date.Ticks);

            int daysElapsed = t1.Days - t2.Days;
            if (daysElapsed < -1 && daysElapsed >= -7)
                return "next " + date.ToString("dddd", CultureInfo.InvariantCulture);

            if (daysElapsed == -1)
                return "tomorrow";

            if (daysElapsed == 0)
                return "today";

            if (daysElapsed == 1)
                return "yesterday";

            if (daysElapsed > 1 && daysElapsed < 7)
                return date.ToString("dddd", CultureInfo.InvariantCulture);

            if (daysElapsed >= 7 && daysElapsed < 14)
                return "last " + date.ToString("dddd", CultureInfo.InvariantCulture);

            return
                date.ToString("MMMM") + " " + date.Day + GetOrdinal(date.Day) +
                (date.Year != currentDate.Year ? " " + date.ToString("yyyy") : string.Empty);
        }

        private static string ToElapsedTimePrivate(DateTime startTime, DateTime endTime)
        {
            int seconds = endTime.Second - startTime.Second;
            int minutes = endTime.Minute - startTime.Minute;
            int hours = endTime.Hour - startTime.Hour;
            int days = endTime.Day - startTime.Day;
            int months = endTime.Month - startTime.Month;
            int years = endTime.Year - startTime.Year;

            if (seconds < 0)
            {
                minutes--;
                seconds += 60;
            }
            if (minutes < 0)
            {
                hours--;
                minutes += 60;
            }
            if (hours < 0)
            {
                days--;
                hours += 24;
            }
            if (days < 0)
            {
                months--;
                int previousMonth = endTime.Month == 1 ? 12 : endTime.Month - 1;
                int year = previousMonth == 12 ? endTime.Year - 1 : endTime.Year;
                days += DateTime.DaysInMonth(year, previousMonth);
            }
            if (months < 0)
            {
                years--;
                months += 12;
            }

            string sYears = FormatBreakdown("year", string.Empty, years);
            string sMonths = FormatBreakdown("month", sYears, months);
            string sDays = FormatBreakdown("day", sMonths, days);
            string sHours = FormatBreakdown("hour", sDays, hours);
            string sMinutes = FormatBreakdown("minute", sHours, minutes);
            string sSeconds = FormatBreakdown("second", sMinutes, seconds);

            return string.Concat(sYears, sMonths, sDays, sHours, sMinutes, sSeconds);
        }

        private static string ToTimeWordsPrivate(DateTime time)
        {
            string result;
            int minutes = time.Minute;
            int hours = time.Hour;
            bool toHour = false;
            int remainder = time.Minute % 5;

            if (remainder < 3)
                minutes -= remainder;
            else
                minutes += 5 - remainder;

            if (minutes > 30)
            {
                hours = (hours + 1) % 24;
                minutes = 60 - minutes;
                toHour = true;
            }

            if (minutes != 0)
                result = FuzzyMinutes[minutes / 6] + " " + (toHour ? "to" : "past") + " " + FuzzyHours[hours];
            else
                result = FuzzyHours[hours] + (hours != 0 && hours != 12 ? " o'clock" : string.Empty);

            if (hours > 0 && hours < 12)
                return result + " am";

            if (hours > 12)
                result = result + " pm";

            return result;
        }

        //remove leading strings with zeros and adjust for singular/plural
    }
}