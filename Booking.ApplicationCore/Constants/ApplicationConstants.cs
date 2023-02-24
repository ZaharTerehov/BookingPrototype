using Booking.ApplicationCore.Enums;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.Constants
{
    public static class ApplicationConstants
    {
        public const PageSize ApartmentsPageSize = PageSize.FourElements;
        public const int MinPeopleNumber = 1;
        public const int MaxPeopleNumber = 10;
        public const int MinReservedDays = 1;
        public const int FirstPage = 1;
        public const int CardTextSimbolCount = 130;
        public const int CardTitleSimbolCount = 20;
        public const string ImagesDir = "Images";
        public const string Currency = "&#36;";
    }
}
