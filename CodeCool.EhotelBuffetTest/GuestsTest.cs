using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Guests.Service;

namespace CodeCool.EhotelBuffetTest;

public class Tests
{
    private RandomGuestGenerator _randomGuestGenerator;
    private GuestGroupProvider _guestGroupProvider;
    
    [SetUp]
    public void Setup()
    {
        _randomGuestGenerator = new RandomGuestGenerator();
        _guestGroupProvider = new GuestGroupProvider();
    }

    [Test]
    public void RandomGuestProviderProvides()
    {
        int numberOfGuests = 20;
        
        IEnumerable<Guest> guests = _randomGuestGenerator.Provide(numberOfGuests);

        Assert.That(numberOfGuests, Is.EqualTo(guests.Count()));
    }
    
    [Test]
    public void RandomGuestProviderRandomName()
    {
        int numberOfGuests = 20;
        
        IEnumerable<Guest> guests = _randomGuestGenerator.Provide(numberOfGuests);

        Assert.NotNull(guests.First().Name);
    }
    
    [Test]
    public void GuestGroupProviderProvides()
    {
        int numberOfGuests = 20;
        int groupCount = 5;
        int maxGuestPerGroup = 3;
        
        IEnumerable<Guest> guests = _randomGuestGenerator.Provide(numberOfGuests);
        List<Guest> guestsList = guests.ToList();
        var guestGroups = _guestGroupProvider.SplitGuestsIntoGroups(guestsList, groupCount, maxGuestPerGroup);

        Assert.That(guestGroups.Count(), Is.GreaterThan(1));
    }
}