using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Guests.Service;
using CodeCool.EhotelBuffet.Reservations.Model;
using CodeCool.EhotelBuffet.Reservations.Service;

namespace CodeCool.EhotelBuffetTest;

public class ReservationsTest
{
    private RandomGuestGenerator _randomGuestGenerator;
    private GuestGroupProvider _guestGroupProvider;
    private ReservationManager _reservationManager;

    [SetUp]
    public void Setup()
    {
        _randomGuestGenerator = new RandomGuestGenerator();
        _guestGroupProvider = new GuestGroupProvider();
        _reservationManager = new ReservationManager();
    }

    [Test]
    public void AddReservation()
    {
        String guestName = "Test";
        Guest guest = new Guest(guestName, GuestType.Business);
        DateTime start = new DateTime(2023, 3, 15);
        DateTime end = new DateTime(2023, 3, 20);
        Reservation reservation = new Reservation(start, end, guest);

        _reservationManager.AddReservation(reservation);
        var reservations = _reservationManager.GetAll().ToList();

        Assert.That(reservations[0].Guest.Name, Is.EqualTo(guestName));
    }
    
    [Test]
    public void GetGuestsForDate()
    {
        Guest guest1 = new Guest("Test1", GuestType.Business);
        Guest guest2 = new Guest("Test2", GuestType.Business);
        DateTime start = new DateTime(2023, 3, 15);
        DateTime end = new DateTime(2023, 3, 20);
        Reservation reservation1 = new Reservation(start, end, guest1);
        Reservation reservation2 = new Reservation(start, end, guest2);
        int numberOfReservations = 2;

        _reservationManager.AddReservation(reservation1);
        _reservationManager.AddReservation(reservation2);
        var reservations = _reservationManager.GetGuestsForDate(start);

        Assert.That(reservations.Count(), Is.EqualTo(numberOfReservations));
    }
}