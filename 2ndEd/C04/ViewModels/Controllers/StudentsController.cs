using Microsoft.AspNetCore.Mvc;
using ViewModels.Services;

namespace ViewModels.Controllers;

public class StudentsController : Controller
{
    private readonly StudentService _studentService = new();

    public async Task<IActionResult> IndexAsync()
    {
        // Get data from the data store
        var students = await _studentService.ReadAllAsync();

        // Create the ViewModel, based on the data
        var viewModel = new StudentListViewModel(
            Students: students.Select(student => new StudentListItemViewModel(
                Id: student.Id,
                Name: student.Name,
                ClassCount: student.Classes.Count()
            ))
        );

        // Return the View
        return View(viewModel);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateStudentViewModel model)
    {
        // Create the student
        await _studentService.CreateAsync(model.Name);

        // Redirect back to the list
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> EditAsync(int id)
    {
        var student = await _studentService.ReadOneAsync(id);
        if(student == null)
        {
            return NotFound();
        }
        var viewModel = new EditStudentViewModel(
            Id: student.Id,
            Form: new StudentFormViewModel(student.Name),
            Classes: student.Classes.Select(x => x.Name)
        );
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditAsync(EditStudentViewModel model)
    {
        // Update the student
        await _studentService.UpdateAsync(model.Id, model.Form.Name);

        // Redirect back to the list
        return RedirectToAction(nameof(Index));
    }
}
