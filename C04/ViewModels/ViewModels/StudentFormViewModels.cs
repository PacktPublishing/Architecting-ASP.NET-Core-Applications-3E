
namespace ViewModels;

public record class CreateStudentViewModel(string Name) : StudentFormViewModel(Name);
public record class EditStudentViewModel(int Id, IEnumerable<string> Classes, StudentFormViewModel Form);
public record class StudentFormViewModel(string Name);
