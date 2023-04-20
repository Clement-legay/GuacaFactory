using System.ComponentModel.DataAnnotations;

namespace GuacaFactory.Shared.Models;

public class Site
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? AddressId { get; set; }
    public Address? Address { get; set; }
    public ICollection<Employee>? Employees { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public Administrator? CreatedByAdministrator { get; set; }
    public Administrator? UpdatedByAdministrator { get; set; }

    public int EmployeesCount { get; set; }
}

public class SiteRegistryDto
{
    [Required] public string? Name { get; set; }
    [Required] public string? Description { get; set; }
    public int? AddressId { get; set; }
}

public class SiteUpdateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? AddressId { get; set; }
}