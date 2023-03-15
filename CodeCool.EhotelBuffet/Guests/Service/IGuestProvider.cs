using CodeCool.EhotelBuffet.Guests.Model;

namespace CodeCool.EhotelBuffet.Guests.Service;

public abstract class IGuestProvider
{
    public abstract IEnumerable<Guest> Provide(int quantity);
}