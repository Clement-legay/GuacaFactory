using GuacaFactory.Shared.Models;

namespace GuacaFactory.Shared.Requests.DocumentTypeRequests;

public interface IDocumentTypeRequests
{
    Task<ICollection<DocumentType>?> GetDocumentTypesAsync(int page, int rows);
    Task<DocumentType?> GetDocumentTypeByIdAsync(int id);
    Task<DocumentType?> AddDocumentTypeAsync(MultipartFormDataContent dataContent);
    Task<DocumentType?> UpdateDocumentTypeAsync(int id, MultipartFormDataContent dataContent);
    Task<DocumentType?> DeleteDocumentTypeAsync(int id);
}