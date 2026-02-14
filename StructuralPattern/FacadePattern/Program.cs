using System;
using FacadePattern.Facade;
using FacadePattern.Models;

namespace FacadePattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BookingFacade facade = new BookingFacade();

            BookingRequest ok = new BookingRequest(
                guestName: "Nikhil",
                checkInDate: new DateTime(2026, 2, 13),
                nights: 2,
                roomCount: 1,
                currency: "USD",
                maxAmountToCharge: 500m
            );

            BookingResponse result1 = facade.BookRoom(ok);
            Console.WriteLine("Success: " + result1.Success + " | " + result1.ConfirmationNumber + " | " + result1.Message);

            BookingRequest tooExpensive = new BookingRequest(
                guestName: "Karle",
                checkInDate: new DateTime(2026, 2, 13),
                nights: 10,
                roomCount: 2,
                currency: "USD",
                maxAmountToCharge: 200m
            );

            BookingResponse result2 = facade.BookRoom(tooExpensive);
            Console.WriteLine("Success: " + result2.Success + " | " + result2.ConfirmationNumber + " | " + result2.Message);
        }
    }
}
