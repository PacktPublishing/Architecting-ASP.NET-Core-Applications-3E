using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TransformTemplateView.Data;
using TransformTemplateView.Data.Models;

namespace TransformTemplateView.Pages.Employees;

public class EditModel : PageModel
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
        if (id is null)
        {
            return NotFound();
        }

        Employee = await _context.Employees.FirstOrDefaultAsync(m => m.Id == id);

        if (Employee is null)
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
            throw;
        }

        return RedirectToPage("./Index");
    }

    [MemberNotNullWhen(true, nameof(Employee))]
    private bool ModelIsValid()
    {
        return ModelState.IsValid;
    }

    private bool EmployeeExists(Employee employee)
    {
        return _context.Employees.Any(e => e.Id == employee.Id);
    }
}
