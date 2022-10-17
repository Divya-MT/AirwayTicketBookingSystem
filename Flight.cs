using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AirWayTicketBooking.Models
{
    public partial class Flight
    {
        public Flight()
        {
            Payments = new HashSet<Payment>();
        }

        [DisplayName("Flight Id")]
        public int FlightId { get; set; }

        [DisplayName("Date Of Arriving")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateOfArriving { get; set; }

        [DisplayName("Date Of Depature")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateOfDepature { get; set; }

        [DisplayName("From")]
        public string? LeaveFrom { get; set; }

        [DisplayName("To")]
        public string? GoingTo { get; set; }


        public decimal? Timing { get; set; }

        [DisplayName("No. Of Seats Available")]
        public int? NoSeatsAvailable { get; set; }
        public decimal? Rate { get; set; }

        [DisplayName("Tax in %")]
        public int? Tax { get; set; }

        [DisplayName("Booking Charges")]
        public decimal? Charges { get; set; }


        public virtual ICollection<Payment> Payments { get; set; }
    }
}
