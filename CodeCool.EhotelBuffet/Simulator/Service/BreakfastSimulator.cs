using System.Runtime.Serialization;
using CodeCool.EhotelBuffet.Buffet.Service;
using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Guests.Service;
using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Refill.Service;
using CodeCool.EhotelBuffet.Reservations.Service;
using CodeCool.EhotelBuffet.Simulator.Model;

namespace CodeCool.EhotelBuffet.Simulator.Service;

public class BreakfastSimulator : IDiningSimulator
{
    private static readonly Random Random = new();

    private readonly IBuffetService _buffetService;
    private readonly IReservationManager _reservationManager;
    private readonly IGuestGroupProvider _guestGroupProvider;
    private readonly ITimeService _timeService;

    private readonly List<Guest> _happyGuests = new();
    private readonly List<Guest> _unhappyGuests = new();

    private int _foodWasteCost;

    public BreakfastSimulator(
        IBuffetService buffetService,
        IReservationManager reservationManager,
        IGuestGroupProvider guestGroupProvider,
        ITimeService timeService)
    {
        _buffetService = buffetService;
        _reservationManager = reservationManager;
        _guestGroupProvider = guestGroupProvider;
        _timeService = timeService;
    }

    public DiningSimulationResults Run(DiningSimulatorConfig config)
    {
        ResetState();
        DateTime currentTime = config.Start;
        List<Guest> guestsToday = _reservationManager.GetGuestsForDate(currentTime).ToList();
        int maxGuestsPerGroup = guestsToday.Count / config.MinimumGroupCount;
        IRefillStrategy refillStrategy = new BasicRefillStrategy();
        List<GuestGroup> groups = _guestGroupProvider.SplitGuestsIntoGroups(guestsToday, config.MinimumGroupCount ,maxGuestsPerGroup).ToList();

        for (int i = 0; i < config.Cycles; i++)
        {
            _buffetService.Refill(refillStrategy);
            IEnumerator<Guest> guestsPergroup = groups[i].Guests.GetEnumerator();
            var meals = guestsPergroup.Current.MealPreferences;
            foreach (var meal in meals)
            {
                if (_buffetService.Consume(meal) == false)
                {
                    _unhappyGuests.Add(guestsPergroup.Current);
                }
                else
                {
                    _happyGuests.Add(guestsPergroup.Current);
                }
            }

            currentTime = _timeService.IncreaseCurrentTime(30);

            _foodWasteCost = _buffetService.CollectWaste(MealDurability.Short, currentTime);

        }

        var result = new DiningSimulationResults(currentTime, guestsToday.Count, _foodWasteCost, _happyGuests,
            _unhappyGuests);
        return  result;
    }

    private void ResetState()
    {
        _foodWasteCost = 0;
        _happyGuests.Clear();
        _unhappyGuests.Clear();
        _buffetService.Reset();
    }
}
