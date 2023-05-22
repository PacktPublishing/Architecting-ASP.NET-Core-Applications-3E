// See https://aka.ms/new-console-template for more information
Console.WriteLine(new Restaurant("The Cool Place"));
Console.WriteLine(new Restaurant("The Even Cooler Place"));

public class Restaurant
{
    public readonly string _name;
    public Restaurant(string name)
        => _name = name;

    public string Name => _name;

    public override string ToString()
        => $"Restaurant: {Name}";
}


public class RestaurantWithBody
{
    public readonly string _name;
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