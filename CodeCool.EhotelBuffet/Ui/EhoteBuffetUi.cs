using CodeCool.EhotelBuffet.Guests.Model;
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
        // CreateReservations(guests, seasonStart, seasonEnd);
        //
        // PrintGuestsWithReservations();

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
        }

        Console.ReadLine();
    }

    private IEnumerable<Guest> GetGuests( int num)
    {
        var guests = new RandomGuestGenerator();
        return guests.Provide(num);
    }

    private void CreateReservations(IEnumerable<Guest> guests, DateTime seasonStart, DateTime seasonEnd)
    {
        foreach (var guest in guests)
        {
            var reservation = _reservationProvider.Provide(guest, seasonStart, seasonEnd);
            _reservationManager.AddReservation(reservation);
        }
    }

    private void PrintGuestsWithReservations()
    {
        Console.WriteLine("Guests with reservations:");

        var reservations = _reservationManager.GetAll();

        foreach (var reservation in reservations)
        {
            Console.WriteLine($"- {reservation.Guest.Name}, {reservation.Guest}, {reservation.Start} - {reservation.End}");
        }

        Console.WriteLine();
    }

    private static void PrintSimulationResults(DiningSimulationResults results)
    {
        Console.WriteLine($"Simulation results for {results.Date.ToShortDateString()}");
        Console.WriteLine($"Total guests: {results.TotalGuests}");
        Console.WriteLine($"Happy guests: {results.HappyGuests.Count()}");
        Console.WriteLine($"Unhappy guests: {results.UnhappyGuests.Count()}");
        Console.WriteLine($"Food waste cost: {results.FoodWasteCost}");
        Console.WriteLine();
        
    }
}
