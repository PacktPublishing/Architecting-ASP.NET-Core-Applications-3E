using ViewModels.Models;

namespace ViewModels.Services;

public class StudentService
{
    public Task<IEnumerable<Student>> ReadAllAsync()
    {
        return Task.FromResult(FakeData.Students.AsEnumerable());
    }

    public Task<Student?> ReadOneAsync(int id)
    {
        return Task.FromResult(FakeData.Students.FirstOrDefault(x => x.Id == id));
    }

    public Task<Student> CreateAsync(string name)
    {
        var student = new Student(
            id: FakeData.NextId,
            name: name,
            classes: new List<Class>()
        );
        FakeData.Students.Add(student);
        return Task.FromResult(student);
    }

    public async Task<Student?> UpdateAsync(int id, string name)
    {
        var student = await ReadOneAsync(id);
        if(student == null)
        {
            return student;
        }
        student.Name = name;
        return student;
    }

    private class FakeData
    {
        public static List<Student> Students { get; }
        private static int _lastId = 0;

        static FakeData()
        {
            var mathClass = new Class(id: 1, name: "Math");
            var scienceClass = new Class(id: 2, name: "Science");
            var computerClass = new Class(id: 3, name: "Computer");
            Students = new List<Student>
                {
                    new Student(
                        id: NextId,
                        name: "Maddie Powers",
                        classes: new List<Class>{ mathClass, scienceClass }
                    ),
                    new Student(
                        id: NextId,
                        name: "Harper Black",
                        classes: new List<Class>{ mathClass, scienceClass, computerClass }
                    ),
                    new Student(
                        id: NextId,
                        name: "Allen York",
                        classes: new List<Class>{ mathClass, computerClass }
                    ),
                    new Student(
                        id: NextId,
                        name: "Lillie Adkins",
                        classes: new List<Class>{ scienceClass, computerClass }
                    )
                };
        }

        public static int NextId => ++_lastId;
    }
}
