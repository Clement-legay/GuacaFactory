using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace GuacaFactory.Shared.Models;

public class Document
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ContentType { get; set; }
    public string? Link { get; set; }
    public int? DocumentTypeId { get; set; }
    public int? EmployeeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public Administrator? CreatedByAdministrator { get; set; }
    public Administrator? UpdatedByAdministrator { get; set; }
    public DocumentType? DocumentType { get; set; }
    public Employee? Employee { get; set; }
}

public class DocumentRegistryDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    [Required] public IFormFile? File { get; set; }

    [Required] public int? EmployeeId { get; set; }
    
    [Required] public int? DocumentTypeId { get; set; }
}

public class DocumentUpdateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public IFormFile? File { get; set; }
    public int? DocumentTypeId { get; set; }
}