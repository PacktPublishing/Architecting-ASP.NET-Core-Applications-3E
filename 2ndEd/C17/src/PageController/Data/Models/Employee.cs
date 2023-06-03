using System.ComponentModel.DataAnnotations;

namespace PageController.Data.Models;

public class Employee
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string? LastName { get; set; }
}
