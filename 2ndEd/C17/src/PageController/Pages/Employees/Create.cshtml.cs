using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PageController.Data;
using PageController.Data.Models;
using System.Diagnostics.CodeAnalysis;

namespace PageController.Pages.Employees;

public class CreateModel : PageModel, ICreateOrEditModel
{
    private readonly EmployeeDbContext _context;
    public CreateModel(EmployeeDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Employee? Employee { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelIsValid())
        {
            return Page();
        }

        _context.Employees.Add(Employee);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }


    [MemberNotNullWhen(true, nameof(Employee))]
    private bool ModelIsValid()
    {
        return ModelState.IsValid;
    }
}
