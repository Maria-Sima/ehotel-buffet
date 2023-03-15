using CodeCool.EhotelBuffet.Menu.Model;

namespace CodeCool.EhotelBuffet.Guests.Model;

public record Guest(string Name, GuestType GuestType)
{
    public MealType[] MealPreferences { get; } = GetMeal(GuestType);


    public static MealType[] GetMeal(GuestType guest)
    {

        switch (guest)
        {
            case GuestType.Kid:
            {
                return new MealType[] { MealType.Bun, MealType.Cereal };
            }

        }

        return Array.Empty<MealType>();
    }
}
