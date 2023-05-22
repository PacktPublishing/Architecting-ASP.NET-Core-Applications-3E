using PageController.Data.Models;

namespace PageController.Pages.Employees;

public interface ICreateOrEditModel
{
    Employee? Employee { get; set; }
}
