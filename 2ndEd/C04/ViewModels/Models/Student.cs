namespace ViewModels.Models;

public class Student
{
    public Student(int id, string name, IEnumerable<Class> classes)
    {
        Id = id;
        Name = name;
        Classes = classes;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Class> Classes { get; set; } = Enumerable.Empty<Class>();
}
