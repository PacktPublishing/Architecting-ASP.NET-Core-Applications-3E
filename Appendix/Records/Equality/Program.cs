var employee1 = new Employee("Johnny", "Mnemonic");
var employee2 = new Employee("Clark", "Kent");
var employee3 = new Employee("Johnny", "Mnemonic");

Console.WriteLine($"Does '{employee1}' equals '{employee2}'? {employee1 == employee2}");
Console.WriteLine($"Does '{employee1}' equals '{employee3}'? {employee1 == employee3}");
Console.WriteLine($"Does '{employee2}' equals '{employee3}'? {employee2 == employee3}");
Console.WriteLine($"Is 'employee1' the same as 'employee3'? {object.ReferenceEquals(employee1, employee3)}");

public record class Employee(string FirstName, string LastName);