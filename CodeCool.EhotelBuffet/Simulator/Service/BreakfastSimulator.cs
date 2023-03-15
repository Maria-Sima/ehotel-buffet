using CodeCool.EhotelBuffet.Buffet.Service;
using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Guests.Service;
using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Menu.Service;
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
          var currentTime = config.Start;

            List<Guest> guests = _reservationManager.GetGuestsForDate(config.Start).ToList();

            var maxGuestsPerGroup = guests.Count / config.MinimumGroupCount;

            var refillStrategy = new BasicRefillStrategy();

            for (int i = 0; i < config.Cycles; i++)
            {
                _buffetService.Refill(refillStrategy);

                for (int j = 0; j < guests.Count; j += maxGuestsPerGroup)
                {
                    IMenuProvider menuProvider = new MenuProvider();
                    var group = guests.GetRange(j, Math.Min(maxGuestsPerGroup, guests.Count - j));
                    var availableFood = menuProvider.MenuItems.ToList();

                    bool allGuestsSatisfied = true;

                    foreach (var guest in group)
                    {
                        var selectedFood = availableFood.Where(item => guest.MealPreferences.Contains(item.MealType)).ToList();

                        if (selectedFood.Count == 0)
                        {
                            _unhappyGuests.Add(guest);
                            allGuestsSatisfied = false;
                        }
                        else
                        {
                            var chosenFood = selectedFood.ElementAt(Random.Next(0, selectedFood.Count));
                            if (!_buffetService.Consume(chosenFood.MealType))
                            {
                                throw new InvalidOperationException("Chosen food could not be consumed.");
                            }
                           
                            _happyGuests.Add(guest);
                        }
                    }

                    if (!allGuestsSatisfied)
                    {
                        _foodWasteCost += _buffetService.CollectWaste(MealDurability.Short, config.Start);
                    }
                }
            }

            var results = new DiningSimulationResults(
                Date: config.Start,
                TotalGuests: guests.Count,
                FoodWasteCost: _foodWasteCost,
                HappyGuests: _happyGuests,
                UnhappyGuests: _unhappyGuests
            );

            ResetState();

            return results;
        
    }

    private void ResetState()
    {
        _foodWasteCost = 0;
        _happyGuests.Clear();
        _unhappyGuests.Clear();
        _buffetService.Reset();
    }
}
