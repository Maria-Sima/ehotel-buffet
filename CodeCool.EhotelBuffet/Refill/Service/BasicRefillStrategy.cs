using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Menu.Service;

namespace CodeCool.EhotelBuffet.Refill.Service;

public class BasicRefillStrategy : IRefillStrategy
{
    private object _menuProvider=new MenuProvider();
    private const int OptimalPortionCount = 3;

    public Dictionary<MenuItem, int> GetInitialQuantities(IEnumerable<MenuItem> menuItems)
    {
        var ret = new Dictionary<MenuItem, int>();
        foreach (var menuItem in menuItems)
        {
            ret.Add(menuItem, OptimalPortionCount);
        }

        return ret;
    }

    public Dictionary<MenuItem, int> GetRefillQuantities(IEnumerable<Portion> currentPortions)
    {
        var quantities = new Dictionary<MenuItem, int>();

        foreach (var portion in currentPortions)
        {
            var count = OptimalPortionCount - currentPortions.Count();
            if (count > 0)
            {
                quantities.Add(portion.MenuItem, count);
            }
        }

        return quantities;
    }

}
