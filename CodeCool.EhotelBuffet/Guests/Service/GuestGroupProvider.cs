using System.Text.RegularExpressions;
using CodeCool.EhotelBuffet.Guests.Model;

namespace CodeCool.EhotelBuffet.Guests.Service;

public class GuestGroupProvider:IGuestGroupProvider
{
    private int OccupiedGroup = 0;
    
    public IEnumerable<GuestGroup> SplitGuestsIntoGroups(IEnumerable<Guest> guests, int groupCount, int maxGuestPerGroup)
    {
       
        List<GuestGroup> guestGroups = new List<GuestGroup>();
        for (int i = 0; i < groupCount; i++)
        {
          
            guestGroups.Add(new GuestGroup(i, GetGroup(guests, maxGuestPerGroup)));
        }

       
        return guestGroups;
    }


    private IEnumerable<Guest> GetGroup(IEnumerable<Guest> guests,int max)
    {
        Random random = new Random();
        int groupSize = random.Next(max + 1);

        IEnumerable<Guest> guestGroup = guests.Skip(OccupiedGroup).Take(groupSize);
        OccupiedGroup += groupSize;
      
        return guestGroup;
    }

}

