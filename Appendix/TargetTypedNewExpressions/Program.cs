
List<string> list1 = new();
List<string> list2 = new(10);
List<string> list3 = new(capacity: 10);
var obj = new MyClass(new());
AnotherClass anotherObj = new() { Name = "My Name" };

public class MyClass {
    public MyClass(AnotherClass property)
        => Property = property;
    public AnotherClass Property { get; }
}
public class AnotherClass {
    public string? Name { get; init; }
}