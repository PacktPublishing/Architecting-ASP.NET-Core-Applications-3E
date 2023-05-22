
var client1 = new MutableClient("John", "Doe");
client1.Firstname = "Jane";
Console.WriteLine(client1);

var client2 = new ImmutableClient("John", "Doe");
Console.WriteLine(client2);
public record struct MutableClient(string Firstname, string Lastname);
public readonly record struct ImmutableClient(string Firstname, string Lastname);
