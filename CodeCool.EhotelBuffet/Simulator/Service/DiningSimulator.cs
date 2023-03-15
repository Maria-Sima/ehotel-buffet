using CodeCool.EhotelBuffet.Buffet.Service;
using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Refill.Service;
using CodeCool.EhotelBuffet.Reservations.Service;
using CodeCool.EhotelBuffet.Simulator.Model;

namespace CodeCool.EhotelBuffet.Simulator.Service;

public class DiningSimulator:IDiningSimulator
{
    private readonly IBuffetService _buffetService;
    private readonly IReservationManager _reservationManager;

    public DiningSimulator(IBuffetService buffetService, IReservationManager reservationManager)
        {
            _buffetService = buffetService;
            _reservationManager = reservationManager;
        }

        public DiningSimulationResults Run(DiningSimulatorConfig config)
        {
            _buffetService.Reset();
            var currentTime = config.Start;
            
            List<Guest> guests = _reservationManager.GetGuestsForDate(config.Start).ToList();
            
            var maxGuestsPerGroup = guests.Count / config.MinimumGroupCount;
            
            var refillStrategy = new BasicRefillStrategy();

            var happyGuests = new List<Guest>();
            var unhappyGuests = new List<Guest>();
            
            for (int i = 0; i < config.Cycles; i++)
            {
                _buffetService.Refill(refillStrategy);
                
                for (int j = 0; j < guests.Count; j += maxGuestsPerGroup)
                {
                    var group = guests.GetRange(j, Math.Min(maxGuestsPerGroup, guests.Count - j));

                    foreach (var item in consumedItems)
                    {
                    }

                    if (happyGuests.Count == j / maxGuestsPerGroup + 1)
                    {
                       
                        continue;
                    }

                    unhappyGuests.AddRange(group);
                }

                
                _buffetService.CollectWaste();
                
                currentTime = currentTime.AddMinutes(config.CycleLengthInMinutes);
            }
            
            var foodWasteCost = _buffetService.();
            
            var results = new DiningSimulationResults(
                Date: config.Start,
                TotalGuests: guests.Count,
                FoodWasteCost: foodWasteCost,
                HappyGuests: happyGuests,
                UnhappyGuests: unhappyGuests
            );

            return results;
        }
    }

