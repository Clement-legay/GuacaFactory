using System.ComponentModel.DataAnnotations;

namespace GuacaFactory.Shared.Models;

public class Employee
{
    public int Id { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Username { get; init; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? HomePhone { get; set; }
    public DateTime BirthDate { get; set; }
    public int? AddressId { get; set; }
    public int? ServiceId { get; set; }
    public int? SiteId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public Administrator? CreatedByAdministrator { get; set; }
    public Administrator? UpdatedByAdministrator { get; set; }
    public Address? Address { get; set; }
    public ICollection<Document>? Documents { get; set; }
    public Service? Service { get; set; }
    public Site? Site { get; set; }
    public Administrator? Administrator { get; set; }

    public string? PictureUrl { get; set; }
    public string? SiteName { get; set; }
    public string? ServiceName { get; set; }
}

public class EmployeeRegistryDto
{
    [Required] public string? Firstname { get; set; }
    [Required] public string? Lastname { get; set; }
    [Required] public string? Email { get; set; }
    [Required] public DateTime BirthDate { get; set; }
    public string? Phone { get; set; }
    public string? HomePhone { get; set; }
    public int? AddressId { get; set; }
    public int? ServiceId { get; set; }
    public int? SiteId { get; set; }
}

public class EmployeeUpdateDto
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Phone { get; set; }
    public string? HomePhone { get; set; }
    public int? AddressId { get; set; }
    public int? ServiceId { get; set; }
    public int? SiteId { get; set; }
}