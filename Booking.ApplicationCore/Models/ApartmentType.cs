using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.Models
{
    public sealed class ApartmentType : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public void UpdateDetails(ApartmentTypeDetails details)
        {
            Name = details.Name;
        }

        public readonly record struct ApartmentTypeDetails
        {
            public string? Name { get; }

            public ApartmentTypeDetails(string? name)
            {
                Name = name;
            }
        }
    }
}
