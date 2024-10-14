using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Model.DTOs.Common
{
    public class PersianCalendarViewModel
    {
        private PersianCalendar _persianCalendar = new PersianCalendar();

        public int CurrentYear { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentDay { get; set; }
        public string MonthName { get; set; }

        public PersianCalendarViewModel()
        {
            var now = DateTime.Now;
            CurrentYear = _persianCalendar.GetYear(now);
            CurrentMonth = _persianCalendar.GetMonth(now);
            CurrentDay = _persianCalendar.GetDayOfMonth(now);
            MonthName = GetMonthName(CurrentMonth);
        }

        private string GetMonthName(int month)
        {
            return month switch
            {
                1 => "فروردین",
                2 => "اردیبهشت",
                3 => "خرداد",
                4 => "تیر",
                5 => "مرداد",
                6 => "شهریور",
                7 => "مهر",
                8 => "آبان",
                9 => "آذر",
                10 => "دی",
                11 => "بهمن",
                12 => "اسفند",
                _ => ""
            };
        }

        public List<int[]> GetCalendar()
        {
            var firstDayOfMonth = new DateTime(CurrentYear, CurrentMonth, 1, _persianCalendar);
            var daysInMonth = _persianCalendar.GetDaysInMonth(CurrentYear, CurrentMonth);

            var calendar = new List<int[]>();
            var week = new int[7];
            int dayOfWeek = (int)firstDayOfMonth.DayOfWeek;

            dayOfWeek = (dayOfWeek == 6) ? 0 : dayOfWeek + 1;

            int dayCounter = 1;

            for (int i = 0; i < 7; i++)
            {
                if (i >= dayOfWeek)
                {
                    week[i] = dayCounter++;
                }
            }
            calendar.Add(week);

            while (dayCounter <= daysInMonth)
            {
                week = new int[7];
                for (int i = 0; i < 7 && dayCounter <= daysInMonth; i++)
                {
                    week[i] = dayCounter++;
                }
                calendar.Add(week);
            }

            return calendar;
        }

        public string GetDayClass(int day, int dayIndex)
        {
            if (day == CurrentDay)
            {
                return "current-day";
            }
            else if (day != 0 && dayIndex == 6)
            {
                return "friday";
            }

            return "";
        }
    }

}
