using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_App.Models
{
    public class TrainDetails
    {
        //train core details
        public int Id { get; set; }
        public int TrainNumber { get; set; }
        public string FromLoc { get; set; }
        public string ToLoc { get; set; }
        public int AvailableWindowSeat { get; set; }
        public int AvailableNormalSeat { get; set; }
        public decimal WindowSeatPrice { get; set; }
        public decimal NormalSeatPrice { get; set; }
        public string AvailabiltyStatus { get; set; }

        //train book details
        public decimal TotalPrice { get; set; }
        public string BookingDate { get; set; }
        public string JourneyDate { get; set; }
        public string SeatType { get; set; }
        public int SeatCount { get; set; }
        public string CancelDate { get; set; }
        public decimal RefundAmt { get; set; }
    }
}
