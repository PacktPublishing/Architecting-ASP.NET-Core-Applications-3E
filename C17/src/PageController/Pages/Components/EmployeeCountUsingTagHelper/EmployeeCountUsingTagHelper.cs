using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PageController.Data;

namespace PageController.Pages.Components.EmployeeCountUsingTagHelper;

public class EmployeeCountUsingTagHelper : ViewComponent
{
    private readonly EmployeeDbContext _context;
    public EmployeeCountUsingTagHelper(EmployeeDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var count = await _context.Employees.CountAsync();
        return View(new EmployeeCountViewModel(count));
    }
}
