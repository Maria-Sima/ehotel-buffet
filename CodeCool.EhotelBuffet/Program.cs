using CodeCool.EhotelBuffet.Buffet.Service;
using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Guests.Service;
using CodeCool.EhotelBuffet.Menu.Service;
using CodeCool.EhotelBuffet.Refill.Service;
using CodeCool.EhotelBuffet.Reservations.Model;
using CodeCool.EhotelBuffet.Reservations.Service;
using CodeCool.EhotelBuffet.Simulator.Service;
using CodeCool.EhotelBuffet.Ui;

// ITimeService timeService = new TimeService();
// IMenuProvider menuProvider = new MenuProvider();
// IRefillService refillService = null;
// IGuestGroupProvider guestGroupProvider = null;
// IReservationProvider reservationProvider = null;
// IReservationManager reservationManager = null;
//
// IBuffetService buffetService = new BuffetService(menuProvider, refillService);
// IDiningSimulator diningSimulator =
//     new BreakfastSimulator(buffetService, reservationManager, guestGroupProvider, timeService);
//
// EhoteBuffetUi ui = new EhoteBuffetUi(reservationProvider, reservationManager, diningSimulator);
//
// ui.Run();

RandomGuestGenerator randomGuestGenerator = new RandomGuestGenerator();
GuestGroupProvider guestGroupProvider = new GuestGroupProvider();


IEnumerable<GuestGroup> guestGroups=guestGroupProvider.SplitGuestsIntoGroups(randomGuestGenerator.Provide(20), 5, 4);
IReservationProvider reservationProvider = new ReservationProvider();
DateTime date1 = new DateTime(2015, 12, 25); 
DateTime date2 = new DateTime(2015, 12, 11); 

// foreach (var guestgroup in guestGroups)
// {
//     foreach (var guest in guestgroup.Guests)
//     {
//         Console.WriteLine(reservationProvider.Provide(guest, date2, date1));
//         
//     }
// }
