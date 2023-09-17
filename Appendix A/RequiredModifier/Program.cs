// Can't do:
//var entity = new Entity();

// Must do:
var entity = new Entity()
{
    Name = "Test",
};

public class Entity
{
    public required string Name { get; set; }
}