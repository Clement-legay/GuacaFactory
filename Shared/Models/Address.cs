using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GuacaFactory.Shared.Models;

public class Address
{
    public int Id { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public Administrator? CreatedByAdministrator { get; set; }
    public Administrator? UpdatedByAdministrator { get; set; }
    public ICollection<Employee>? Employees { get; set; }
    public ICollection<Site>? Sites { get; set; }
}

public class AddressRegistryDto
{
    [Required] public string? Street { get; set; }
    [Required] public string? City { get; set; }
    [Required] public string? PostalCode { get; set; }
}

public class AddressUpdateDto
{
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
}