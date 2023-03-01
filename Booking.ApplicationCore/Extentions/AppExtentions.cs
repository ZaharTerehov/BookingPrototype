using Booking.ApplicationCore.Constants;
using Booking.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.Extentions
{
    public static class AppExtentions
    {
        public static string ToYYYYMMDDDateFormat(this DateTime date) => date.ToString("yyyy-MM-dd");

        public static string CardTitle(this string text)
        {
            var result = text.Substring(0, Math.Min(text.Length, ApplicationConstants.CardTitleSimbolCount));
            return result == text ? result : $"{result}...";            
        }

        public static string CardText(this string text)
        {
            var result = text.Substring(0, Math.Min(text.Length, ApplicationConstants.CardTextSimbolCount));
            return result == text ? result: $"{result}...";
        }

        public static IList<ApartmentPicture> ToListApartmentPictures(this IList<string> filesList)
        {            
            IList<ApartmentPicture> pictureList = new List<ApartmentPicture>();
            if (filesList != null)
            {
                foreach (var file in filesList)
                {
                    pictureList.Add(new ApartmentPicture { PictureUrl = file });
                }
            }

            return pictureList;
        }
        public static IList<string> ToListStringPaths(this IList<ApartmentPicture> picturesList)
        {
            IList<string> pictureList = new List<string>();
            if (picturesList != null)
            {
                foreach (var picture in picturesList)
                {
                    pictureList.Add(picture.PictureUrl);
                }
            }

            return pictureList;
        }
    }
}
