using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PageController.Data;
using PageController.Data.Models;

namespace PageController.Pages.Employees;

public class DetailsModel : PageModel
{
    private readonly EmployeeDbContext _context;

    public DetailsModel(EmployeeDbContext context)
    {
        _context = context;
    }

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
}
