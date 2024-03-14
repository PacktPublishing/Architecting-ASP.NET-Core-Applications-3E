var tuple = (name: "Foo", age: 23);
var (name, _) = tuple;
Console.WriteLine(name);

if (bool.TryParse("true", out _))
{
    Console.WriteLine("true was parsable!");
}
