using FluentAssertions;
using Data;
using Domains;
using Microsoft.EntityFrameworkCore;

namespace Application.Test
{
    
    public class FlightApplicationSpecification
    {
        readonly Entities entities = new Entities(new DbContextOptionsBuilder<Entities>().
            UseInMemoryDatabase("Flights")
            .Options);

        readonly BookingService bookingService;

            public FlightApplicationSpecification()
            {
               bookingService = new BookingService(entities:entities);
            }

        [Theory]
        [InlineData("alfred@hotmail.com", 2)]
        [InlineData("bruce@hotmail.com", 2)]

        public void Remembers_bookings(string passengerEmail, int numberOfSeats)
        {

            var flight = new Flight(3);

            entities.Flights.Add(flight);

            var bookingService = new BookingService(entities: entities);
            bookingService.Book(new BookDto(flight.Id, passengerEmail, numberOfSeats));
            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(
                new BookingRm(passengerEmail, numberOfSeats));
        }
        [Theory]
        [InlineData(3)]
        [InlineData(10)]
        public void Frees_up_seats_after_booking(int initialCapacity)
        {
 

            var flight= new Flight(initialCapacity);
            entities.Flights.Add(flight);

            bookingService.Book(new BookDto(flight.Id, "username@hotmail.com", 2));


            bookingService.CancelBooking(  
                new CancelBookingDto(flightId:flight.Id,
                passengerEmail:"username@hotmail.com",
                numberOfSeats:2));

            bookingService.getRemainingNumberOfSeatsFor(flight.Id).Should().Be(initialCapacity);


        }

    }

}
