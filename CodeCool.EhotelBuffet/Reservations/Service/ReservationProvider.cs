using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Reservations.Model;

namespace CodeCool.EhotelBuffet.Reservations.Service;

public class ReservationProvider:IReservationProvider
{
    private readonly Random _random = new Random();

    public Reservation Provide(Guest guest, DateTime seasonStart, DateTime seasonEnd)
    {
        int numberOfDaysInSeason = seasonEnd.Day - seasonStart.Day;
        int lengthOfStay = _random.Next(1, numberOfDaysInSeason);
        
        int maxStartDateOffset = numberOfDaysInSeason - lengthOfStay;
        DateTime   startDate = seasonStart.AddDays(_random.Next(0, maxStartDateOffset));
        DateTime endDate = startDate.AddDays(lengthOfStay);

        return new Reservation(startDate, endDate, guest);
    }
}

