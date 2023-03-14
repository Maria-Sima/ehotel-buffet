using CodeCool.EhotelBuffet.Guests.Model;

namespace CodeCool.EhotelBuffet.Guests.Service;

public interface IGuestProvider
{
    private static readonly Random Random;

    private static readonly string[] Names;

    IEnumerable<Guest> Provide(int quantity);
}