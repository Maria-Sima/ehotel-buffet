using CodeCool.EhotelBuffet.Menu.Model;

namespace CodeCool.EhotelBuffet.Guests.Model;

public record Guest(string Name, GuestType GuestType)
{
    public MealType[] MealPreferences { get; } = SetMealPreferences(GuestType);

    public static MealType[] SetMealPreferences(GuestType guest)
    {
        switch (guest)
        {
            case GuestType.Business:
            {
                return new MealType[] { MealType.ScrambledEggs, MealType.FriedBacon, MealType.Croissant};
            }
            case GuestType.Kid:
            {
                return new MealType[] { MealType.Cereal, MealType.Pancake, MealType.Muffin, MealType.Milk };
            }
            case GuestType.Tourist:
            {
                return new MealType[] { MealType.SunnySideUp, MealType.FriedSausage, MealType.MashedPotato, MealType.Bun, MealType.Muffin };
            }
            default:
            {
                return Array.Empty<MealType>();
            }
        }
    }
}
