﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class BookingRm
    {
        public string PassengerEmail { get; set; }

        public int NumbersOfSeats { get; set; }

        public BookingRm(string passengerEmail, int numberOfSeats)
        {
            PassengerEmail = passengerEmail;
            NumbersOfSeats = numberOfSeats;
        }
    }
}
