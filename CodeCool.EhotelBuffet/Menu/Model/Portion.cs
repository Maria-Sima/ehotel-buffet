namespace CodeCool.EhotelBuffet.Menu.Model;

public record Portion(MenuItem MenuItem, DateTime TimeStamp)
{
    public Portion() 
    {
        throw new NotImplementedException();
    }
}
