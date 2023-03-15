using CodeCool.EhotelBuffet.Menu.Model;

namespace CodeCool.EhotelBuffet.Refill.Service;

public class RefillService : IRefillService
{
    public IEnumerable<Portion> AskForRefill(Dictionary<MenuItem, int> menuItemToPortions)
    {
        var result = new List<Portion>();
        foreach (var kvp in menuItemToPortions)
        {
            var menuItem = kvp.Key;
            var numPortions = kvp.Value;
            for (int i = 0; i < numPortions; i++)
            {
                Portion newPortion = new Portion(menuItem, DateTime.Now);
                result.Add(newPortion);
            }
        }

        return result;
    }
}