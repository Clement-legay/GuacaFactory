using System.ComponentModel.DataAnnotations;

namespace GuacaFactory.Shared.Models;

public class Service
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Employee>? Employees { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public Administrator? CreatedByAdministrator { get; set; }
    public Administrator? UpdatedByAdministrator { get; set; }

    public int EmployeesCount { get; set; }
}

public class ServiceRegistryDto
{
    [Required] public string? Name { get; set; }
    [Required] public string? Description { get; set; }
}

public class ServiceUpdateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}