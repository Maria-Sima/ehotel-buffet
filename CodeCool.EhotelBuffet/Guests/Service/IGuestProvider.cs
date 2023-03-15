using CodeCool.EhotelBuffet.Guests.Model;

namespace CodeCool.EhotelBuffet.Guests.Service;

public interface IGuestProvider
{
    public  IEnumerable<Guest> Provide(int quantity);
}