using System.Globalization;

namespace CommonLayer
{
    public static class Utilities
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("00");
        }
    }

    public class NameGenerator
    {
        public static string GeneratorUniqCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }

    public static class FixedText
    {
        public static string FixEmail(this string email)
        {
            return email.Trim().ToLower();
        }

        public static string ConvertToHashtags(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;  // در صورتی که ورودی خالی باشد
            }

            // جدا کردن کلمات با استفاده از کاما
            var words = input.Split(',')
                             .Select(word => "#" + word.Trim()) // اضافه کردن # قبل از هر کلمه
                             .ToList();

            // ترکیب کلمات به یک رشته
            string result = string.Join(" ", words);

            return result;
        }
    }

    public static class DateTimeHelper
    {
        public static string GetTimeAgo(this DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan.Days > 365)
            {
                int years = (int)(timeSpan.Days / 365);
                return $"{years} سال پیش";
            }
            else if (timeSpan.Days > 30)
            {
                int months = (int)(timeSpan.Days / 30);
                return $"{months} ماه پیش";
            }
            else if (timeSpan.Days > 0)
            {
                return $"{timeSpan.Days} روز پیش";
            }
            else if (timeSpan.Hours > 0)
            {
                return $"{timeSpan.Hours} ساعت پیش";
            }
            else if (timeSpan.Minutes > 0)
            {
                return $"{timeSpan.Minutes} دقیقه پیش";
            }
            else
            {
                return "چند لحظه پیش";
            }
        }
    }



}
