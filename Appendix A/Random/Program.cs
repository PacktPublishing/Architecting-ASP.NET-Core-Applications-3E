var items = "Lorem ipsum dolor sit amet".Split(' ');
var results = Random.Shared.GetItems(items, 1);
Console.WriteLine(results.Single());

Random.Shared.Shuffle(items);
Console.WriteLine(string.Join(' ', items));