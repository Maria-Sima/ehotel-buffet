using CodeCool.EhotelBuffet.Guests.Model;

namespace CodeCool.EhotelBuffet.Guests.Service;

public class GuestGroupProvider : IGuestGroupProvider
{
    private Random random = new Random();
    List<GuestGroup> guestGroups = new List<GuestGroup>();
    private int id = 0;
    public List<Guest> GuestsNotInAGroup = new List<Guest>();

    public IEnumerable<GuestGroup> SplitGuestsIntoGroups(IEnumerable<Guest> guests, int groupCount,
        int maxGuestPerGroup)
    {
        List<Guest> guestsList = guests.ToList();
        List<Guest> group = new List<Guest>();
        int guestsAddedToGroups = 0;

        int guestsPerGroup = random.Next(maxGuestPerGroup);

        for (int i = 0; i < groupCount; i++)
        {
            if (i == guestsPerGroup)
            {
                guestsPerGroup = random.Next(maxGuestPerGroup);
                AddToGroups(group);
                group.Clear();
            }

            group.Add(guestsList[i]);
            guestsAddedToGroups = i;
        }

        for (int i = guestsAddedToGroups; i < guestsList.Count; i++)
        {
            GuestsNotInAGroup.Add(guestsList[i]);
        }

        return guestGroups;
    }

    public void AddToGroups(List<Guest> guests)
    {
        Console.WriteLine(1);
        GuestGroup guestGroup = new GuestGroup(id++, guests);
        guestGroups.Add(guestGroup);
    }
}