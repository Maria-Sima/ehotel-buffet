using System.Collections.Generic;
using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Reservations.Model;

namespace CodeCool.EhotelBuffet.Reservations.Service
{
    public class ReservationManager : IReservationManager
    {
        private List<Reservation> _reservations = new List<Reservation>();

        public void AddReservation(Reservation reservation)
        {
            _reservations.Add(reservation);
        }

        public IEnumerable<Guest> GetGuestsForDate(DateTime date)
        {
            List<Guest> occupiedGuests = new List<Guest>();
            foreach (var reservation in _reservations)
            {
                if (reservation.Start <= date && reservation.End >= date)
                {
                    occupiedGuests.Add(reservation.Guest);
                }
            }

            return occupiedGuests;
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _reservations;
        }
    }
}