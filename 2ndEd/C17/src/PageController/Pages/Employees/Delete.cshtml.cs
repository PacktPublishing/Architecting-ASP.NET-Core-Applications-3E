using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PageController.Data;
using PageController.Data.Models;

namespace PageController.Pages.Employees;

public class DeleteModel : PageModel
{
    private readonly EmployeeDbContext _context;

    public DeleteModel(EmployeeDbContext context)
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Employee = await _context.Employees.FindAsync(id);

        if (Employee != null)
        {
            _context.Employees.Remove(Employee);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
