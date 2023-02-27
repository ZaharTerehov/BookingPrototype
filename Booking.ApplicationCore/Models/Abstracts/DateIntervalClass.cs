using Booking.ApplicationCore.Attributes.Validation;
using Booking.ApplicationCore.Constants;
using Booking.ApplicationCore.Extentions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.Models.Abstracts
{
    public abstract class DateIntervalClass : BaseModel
    {
        private DateTime _arrivalDate;
        private DateTime _departureDate;
        private string _arrivalDateS;
        private string _departureDateS;

        [Required]
        [CurrentDate]
        public DateTime ArrivalDate { get => _arrivalDate; set => _arrivalDate = value; }
        [Required]
        [DepartureDate("ArrivalDate")]
        public DateTime DepartureDate { get => _departureDate; set => _departureDate = value; }
        [NotMapped]
        public string ArrivalDateS
        {
            get => _arrivalDateS;
            set
            {
                _arrivalDateS = value;
                DateTime.TryParse(value, out _arrivalDate);
                if (_arrivalDate >= _departureDate)
                {
                    DepartureDateS = MinDepartureDate;
                }
            }
        }
        [NotMapped]
        public string DepartureDateS
        {
            get => _departureDateS;
            set
            {
                if (DateTime.TryParse(value, out var curValue))
                {
                    if (curValue > _arrivalDate)
                    {
                        _departureDateS = value;
                        DateTime.TryParse(value, out _departureDate);
                    }
                }
            }
        }
        [NotMapped]
        public string MinArrivalDate
        {
            get { return DateTime.Now.ToYYYYMMDDDateFormat(); }
        }
        [NotMapped]
        public string MinDepartureDate
        {
            get { return _arrivalDate.AddDays(ApplicationConstants.MinReservedDays).ToYYYYMMDDDateFormat(); }
        }

        public DateIntervalClass()
        {
            _arrivalDate = DateTime.Now;
            _departureDate = DateTime.Now.AddDays(ApplicationConstants.MinReservedDays);
            ArrivalDateS = _arrivalDate.ToYYYYMMDDDateFormat();
            DepartureDateS = _departureDate.ToYYYYMMDDDateFormat();
        }
    }
}
