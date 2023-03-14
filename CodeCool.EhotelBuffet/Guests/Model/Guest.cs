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
                return new MealType[]
                {
                    MealType.ScrambledEggs, MealType.FriedBacon, MealType.Croissant
                };
            }
            case GuestType.Tourist:
            {
                return new MealType[]
                {
                    MealType.SunnySideUp, MealType.FriedSausage, MealType.MashedPotato, MealType.Bun, MealType.Muffin
                };
            }
            case GuestType.Kid:
            {
                return new MealType[]
                {
                    MealType.Pancake, MealType.Muffin, MealType.Cereal, MealType.Milk
                };
            }
            default:
            {
                return Array.Empty<MealType>();
            }
        }
    }
}