using System.ComponentModel.DataAnnotations;

namespace GuacaFactory.Shared.Models;

public class DocumentType
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public Administrator? CreatedByAdministrator { get; set; }
    public Administrator? UpdatedByAdministrator { get; set; }
    public ICollection<Document>? Documents { get; set; }
}

public class DocumentTypeRegistryDto
{
    // unique name
    [Required] public string? Name { get; set; }
}