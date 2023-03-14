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
                return new MealType[] { MealType.Bun};
            }
            case GuestType.Kid:
            {
                return new MealType[] { MealType.Cereal };
            }
            case GuestType.Tourist:
            {
                return new MealType[] { MealType.Croissant };
            }
            default:
            {
                return Array.Empty<MealType>();
            }
        }
    }
}
