﻿using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Guests.Service;
using CodeCool.EhotelBuffet.Reservations.Service;
using CodeCool.EhotelBuffet.Simulator.Model;
using CodeCool.EhotelBuffet.Simulator.Service;

namespace CodeCool.EhotelBuffet.Ui;

public class EhoteBuffetUi
{
    private readonly IReservationManager _reservationManager;
    private readonly IDiningSimulator _diningSimulator;

    private readonly IReservationProvider _reservationProvider;

    public EhoteBuffetUi(
        IReservationProvider reservationProvider,
        IReservationManager reservationManager,
        IDiningSimulator diningSimulator)
    {
        _reservationProvider = reservationProvider;
        _reservationManager = reservationManager;
        _diningSimulator = diningSimulator;
    }

    public void Run()
    {
        int guestCount = 20;
        DateTime seasonStart = DateTime.Today;
        DateTime seasonEnd = DateTime.Today.AddDays(3);

        var guests = GetGuests(guestCount);
        CreateReservations(guests, seasonStart, seasonEnd);

        PrintGuestsWithReservations();

        var currentDate = seasonStart;

        while (currentDate <= seasonEnd)
        {
            var simulatorConfig = new DiningSimulatorConfig(
                currentDate.AddHours(6),
                currentDate.AddHours(10),
                30,
                3);

            var results = _diningSimulator.Run(simulatorConfig);
            PrintSimulationResults(results);
            currentDate = currentDate.AddDays(1);
        } ;
        Console.ReadLine();
    }

    private IEnumerable<Guest> GetGuests(int count)
    {
        RandomGuestGenerator generate = new RandomGuestGenerator();
        IEnumerable<Guest> generatedGuests = generate.Provide(count);
        return generatedGuests;
    }

    private void CreateReservations(IEnumerable<Guest> guests, DateTime seasonStart, DateTime seasonEnd)
    {
        foreach (var guest in guests)
        {
            var res = _reservationProvider.Provide(guest, seasonStart, seasonEnd);
            _reservationManager.AddReservation(res);
        }
    }

    private void PrintGuestsWithReservations()
    {
        var guestsWithRezervation = _reservationManager.GetAll();
        foreach (var guest in guestsWithRezervation)
        {
            Console.WriteLine(guest.Guest);
        }
    }

    private static void PrintSimulationResults(DiningSimulationResults results)
    {
        Console.WriteLine($"Simulation results for {results.Date}");
        Console.WriteLine($"Total guests: {results.TotalGuests}");
        Console.WriteLine($"Happy guests: {results.HappyGuests.Count()}");
        Console.WriteLine($"Unhappy guests: {results.UnhappyGuests}");
        Console.WriteLine($"Food waste cost: {results.FoodWasteCost}");
        Console.WriteLine();
    }
}
