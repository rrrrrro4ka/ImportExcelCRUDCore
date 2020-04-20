using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelCRUDCore.Helpers
{
    public static class Formatters
    {
        private const string DateFormat = "dd-MM-yyyy";
        private const string TimeFormat = "dd-MM-yyyy HH:mm:ss";

        public static DateTime StringToDate(string value)
        {
            return DateTime.ParseExact(value, DateFormat, CultureInfo.InvariantCulture);
        }

        public static bool TryStringToDate(string value, out DateTime result)
        {
            return DateTime.TryParseExact(value, DateFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None,
                out result);
        }

        public static bool TryStringToTime(string value, out DateTime result)
        {
            return DateTime.TryParseExact(value, TimeFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None,
                out result);
        }

        public static string DateToString(DateTime value)
        {
            return value.ToString(DateFormat);
        }

        public static string TimeToString(DateTime value)
        {
            return value.ToString(TimeFormat);
        }
    }

}
