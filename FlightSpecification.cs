using FluentAssertions;
using System.Linq.Expressions;
using Domains;
namespace FlightTest
{
    public class FlightSpecification
    {
        [Theory]
        [InlineData(3,1,2)]
        [InlineData(10,6,4)]
        [InlineData(6,3,3)]
        [InlineData(12,8,4)]

        public void Booking_Reduces_The_number_of_seats(int seatsCapacity,int numberOfSeats,int remainingNumberOfSeats)
        {
            var flight = new Flight(seatsCapacity);

            flight.Book("username@hotmail.com", numberOfSeats);
            flight.RemainingNumberOfSeat.Should().Be(remainingNumberOfSeats);
        }

        [Fact]

        public void Booking_Reduces_The_number_of_seats_2()
        {
            var flight = new Flight(6);

            flight.Book("username@hotmail.com", 3);
            flight.RemainingNumberOfSeat.Should().Be(3);
        }
        [Fact]

        public void Avoids_Overbooking()
        {
            var flight = new Flight(3);

            var error = flight.Book("username@hotmail.com", 4);
            error.Should().BeOfType<OverBookingError>();

        }

        [Fact]

        public void Books_Flight_Sucessfully()
        {
            var flight= new Flight(3);

            var error = flight.Book("username@hotmail.com", 1);
            error.Should().BeNull();

        }
        [Fact]

        public void Rembembers_Booking()
        {
            var flight=new Flight(150);

            flight.Book("username@hotmail.com", 4);

            flight.BookingList.Should().ContainEquivalentOf(new Booking("username@hotmail.com", 4));
        }

        [Theory]
        [InlineData(3,1,1,3)]
        [InlineData(4,2,2,4)]
        [InlineData(7,5,4,6)]

        public void Canceling_booking_frees_up_the_seats(
            int initialCapacity,
            int numberOfSeatsToBook,
            int numberOfSeatsToCancel,
            int remainNumberOfSeats)
        {
            var flight = new Flight(initialCapacity);

            flight.Book("username@hotmail.com", numberOfSeatsToBook);

            flight.CancelBooking("username@hotmail.com", numberOfSeatsToCancel);

            flight.RemainingNumberOfSeat.Should().Be(remainNumberOfSeats);
        }

        [Fact]

        public void Doesnt_cancel_booking_for_passengers_who_have_not_booked()
        {
            var flight = new Flight(3);

            var error = flight.CancelBooking("username@hotmail.com", 2);
            error.Should().BeOfType<BookingNotFoundError>();


        }
        [Fact]

        public void Returns_null_when_successfully_cancels_a_booking()
        {
            var flight = new Flight(3);

            flight.Book("username@hotmail.com", 1);
            var error = flight.CancelBooking("username@hotmail.com", 1);
            error.Should().BeNull();

        }

    }
}