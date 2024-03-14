string? left = null;
string right = "Right value";
left ??= right; // Assigns right to left because left is null
Console.WriteLine(left); // Output: "Right value"

left = "Left value";
left ??= right; // Does not assigns right to left because left is not null
Console.WriteLine(left); // Output: "Left value"
