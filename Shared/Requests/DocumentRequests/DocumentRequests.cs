using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using GuacaFactory.Shared.Models;

namespace GuacaFactory.Shared.Requests.DocumentRequests;

public class DocumentRequests : IDocumentRequests
{
    #region Fields

    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly ISessionValues _sessionValues;
    private const string Url = "http://localhost:8080";
    private const string Version = "v1";

    #endregion

    #region Constructors

    public DocumentRequests(HttpClient httpClient, ISessionValues sessionValues)
    {
        _httpClient = httpClient;
        _apiUrl = $"{Url}/api/{Version}/document";
        _sessionValues = sessionValues;
    }

    private async Task InitializeAsync()
    {
        var userConnected = await _sessionValues.GetSessionUser();
        var apiKey = await _httpClient.GetStringAsync($"{Url}/api/{Version}/basics/apikey");
        
        var token = userConnected?.Employee?.Username is null
            ? Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey))
            : Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{userConnected.Employee.Username}:{userConnected.Employee.Username}"));

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    #endregion

    #region Methods

    public async Task<ICollection<Document>?> GetDocumentsAsync(int page, int rows)
    {
        await InitializeAsync();

        var result = await _httpClient.GetAsync($"{_apiUrl}?page={page}&rows={rows}");
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var documents = JsonSerializer.Deserialize<ICollection<Document>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return documents;
    }

    public async Task<Document?> GetDocumentByIdAsync(int id)
    {
        await InitializeAsync();

        var result = await _httpClient.GetAsync($"{_apiUrl}/{id}");
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var document = JsonSerializer.Deserialize<Document>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return document;
    }

    public async Task<ICollection<Document>?> GetDocumentsByEmployeeIdAsync(int id)
    {
        await InitializeAsync();

        var result = await _httpClient.GetAsync($"{_apiUrl}/employee/{id}");
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var documents = JsonSerializer.Deserialize<ICollection<Document>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return documents;
    }

    public string GetDocumentUrl(string documentType, string employeeName, string documentName)
    {
        var url = $"{_apiUrl}/files/{employeeName}/{documentType}/{documentName}";
        return url;
    }

    public string GetDocumentViaUrl(string url)
    {
        var documentUrl = $"{_apiUrl}/{url}";
        return documentUrl;
    }

    public async Task<Document?> AddDocumentAsync(MultipartFormDataContent dataContent)
    {
        await InitializeAsync();

        var result = await _httpClient.PostAsync($"{_apiUrl}", dataContent);
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var document = JsonSerializer.Deserialize<Document>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return document;
    }

    public async Task<Document?> UpdateDocumentAsync(int id, MultipartFormDataContent dataContent)
    {
        await InitializeAsync();

        var result = await _httpClient.PutAsync($"{_apiUrl}/{id}/update", dataContent);
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var document = JsonSerializer.Deserialize<Document>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return document;
    }

    public async Task<Document?> DeleteDocumentAsync(int id)
    {
        await InitializeAsync();

        var result = await _httpClient.DeleteAsync($"{_apiUrl}/{id}/delete");
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var document = JsonSerializer.Deserialize<Document>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return document;
    }

    #endregion
}