// See https://aka.ms/new-console-template for more information
Console.WriteLine(new Restaurant("The Cool Place"));
Console.WriteLine(new Restaurant("The Even Cooler Place"));

public class Restaurant
{
    private readonly string _name;
    public Restaurant(string name)
        => _name = name;

    public string Name => _name;

    public override string ToString()
        => $"Restaurant: {Name}";
}


public class RestaurantWithBody
{
    private readonly string _name;
    public RestaurantWithBody(string name)
    {
        _name = name;
    }

    public string Name
    {
        get
        {
            return _name;
        }
    }

    public override string ToString()
    {
        return $"Restaurant: {Name}";
    }
}