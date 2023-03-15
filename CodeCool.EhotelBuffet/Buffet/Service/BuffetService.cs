using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Menu.Service;
using CodeCool.EhotelBuffet.Refill.Service;

namespace CodeCool.EhotelBuffet.Buffet.Service;

public class BuffetService : IBuffetService
{
    private readonly IMenuProvider _menuProvider;
    private readonly IRefillService _refillService;
    private readonly List<Portion> _currentPortions = new();

    private bool _isInitialized;

    public BuffetService(IMenuProvider menuProvider, IRefillService refillService)
    {
        _menuProvider = menuProvider;
        _refillService = refillService;
    }

    public void Refill(IRefillStrategy refillStrategy)
    {
        if (!_isInitialized)
        {
            var initialQuantities = refillStrategy.GetInitialQuantities(_menuProvider.MenuItems);

            foreach (var quantity in initialQuantities)
            {
                for (int i = 0; i < quantity.Value; i++)
                {
                    var portion = new Portion { MenuItem = quantity.Key };
                    _currentPortions.Add(portion);
                }
            }

            _isInitialized = true;
        }
        else
        {
            var refillQuantities = refillStrategy.GetRefillQuantities(_currentPortions);

            foreach (var quantity in refillQuantities)
            {
                for (int i = 0; i < quantity.Value; i++)
                {
                    var portion = new Portion { MenuItem = quantity.Key };
                    _currentPortions.Add(portion);
                }
            }
        }
    }

    public void Reset()
    {
        _currentPortions.Clear();
        _isInitialized = false;
    }

    public bool Consume(MealType mealType)
    {
        var portion = _currentPortions.FirstOrDefault(p => p.MenuItem.MealType == mealType);
        if (portion != null)
        {
            _currentPortions.Remove(portion);
            return true;
        }
        return false;
    }


    public int CollectWaste(MealDurability mealDurability, DateTime currentDate)
    {
        var expiredPortions = _currentPortions.Where(p => p.MenuItem.MealDurability == mealDurability && p.TimeStamp <= currentDate).ToList();
        var totalCost = expiredPortions.Sum(p => p.MenuItem.Cost);
        _currentPortions.RemoveAll(p => expiredPortions.Contains(p));
        return totalCost;
    }
}
