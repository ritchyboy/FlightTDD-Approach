using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domains
{
    public class Flight
    {
        private List<Booking> bookingList = new();
        public IEnumerable<Booking> BookingList => bookingList;

        public int RemainingNumberOfSeat { get; set; }

        public Guid Id { get; }

        [Obsolete("Needed for EF")]

        Flight() { }

            public Flight(int seatCapacity)
            {
              RemainingNumberOfSeat = seatCapacity;
            }

    public object? Book(string passengerEmail, int numberOfSeats)
    {
        if (numberOfSeats > this.RemainingNumberOfSeat)
            return new OverBookingError();

        RemainingNumberOfSeat -= numberOfSeats;

        bookingList.Add(new Booking(passengerEmail, numberOfSeats));
        return null;
    }

    public object? CancelBooking(string passengerEmail, int numberOfSeats)
    {
        if (!bookingList.Any(booking => booking.Email == passengerEmail))
            return new BookingNotFoundError();

        RemainingNumberOfSeat += numberOfSeats;
        return null;
    }
  }   
}
