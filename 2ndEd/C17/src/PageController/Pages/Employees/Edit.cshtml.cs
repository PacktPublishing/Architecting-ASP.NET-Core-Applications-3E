using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PageController.Data;
using PageController.Data.Models;
using System.Diagnostics.CodeAnalysis;

namespace PageController.Pages.Employees;

public class EditModel : PageModel, ICreateOrEditModel
{
    private readonly EmployeeDbContext _context;

    public EditModel(EmployeeDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Employee? Employee { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Employee = await _context.Employees.FirstOrDefaultAsync(m => m.Id == id);

        if (Employee == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelIsValid())
        {
            return Page();
        }
        _context.Attach(Employee).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmployeeExists(Employee))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    [MemberNotNullWhen(true, nameof(Employee))]
    private bool ModelIsValid()
    {
        return ModelState.IsValid;
    }

    [MemberNotNullWhen(true, nameof(Employee))]
    private bool EmployeeExists(Employee? employee)
    {
        if (employee == null)
        {
            return false;
        }
        return _context.Employees.Any(e => e.Id == employee.Id);
    }
}
