using Booking.ApplicationCore.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.Extentions
{
    public static class AppExtentions
    {
        private const int needFormatIndex = 0;
        public static string ToYYYYMMDDDateFormat(this DateTime date) =>
                                date.GetDateTimeFormats()[ApplicationConstants.MinDateTimeFormatIndex].Split(" ")[needFormatIndex];

        public static string CardTitle(this string text)
        {
            return text.Substring(0, Math.Min(text.Length, ApplicationConstants.CardTitleSimbolCount)) + "...";
        }

        public static string CardText(this string text)
        {
            return text.Substring(0, Math.Min(text.Length, ApplicationConstants.CardTextSimbolCount))+"...";
        }
    }
}
