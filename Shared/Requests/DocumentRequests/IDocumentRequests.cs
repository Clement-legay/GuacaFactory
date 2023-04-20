using GuacaFactory.Shared.Models;

namespace GuacaFactory.Shared.Requests.DocumentRequests;

public interface IDocumentRequests
{
    Task<ICollection<Document>?> GetDocumentsAsync(int page, int rows);
    Task<Document?> GetDocumentByIdAsync(int id);
    Task<ICollection<Document>?> GetDocumentsByEmployeeIdAsync(int id);
    string GetDocumentUrl(string documentType, string employeeName, string documentName);
    string GetDocumentViaUrl(string url);
    Task<Document?> AddDocumentAsync(MultipartFormDataContent dataContent);
    Task<Document?> UpdateDocumentAsync(int id, MultipartFormDataContent dataContent);
    Task<Document?> DeleteDocumentAsync(int id);
}