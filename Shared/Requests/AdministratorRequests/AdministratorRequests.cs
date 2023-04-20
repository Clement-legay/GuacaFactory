using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using GuacaFactory.Shared.Models;

namespace GuacaFactory.Shared.Requests.AdministratorRequests;

public class AdministratorRequests : IAdministratorRequests
{
    #region Fields

    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly ISessionValues _sessionValues;
    private const string Url = "http://localhost:8080";
    private const string Version = "v1";

    #endregion

    #region Constructors

    public AdministratorRequests(HttpClient httpClient, ISessionValues sessionValues)
    {
        _httpClient = httpClient;
        _apiUrl = $"{Url}/api/{Version}/administrators";
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

    public async Task<ICollection<Administrator>?> GetAdministratorsAsync(int page, int rows)
    {
        await InitializeAsync();

        var result = await _httpClient.GetAsync($"{_apiUrl}?page={page}&rows={rows}");
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var administrators = JsonSerializer.Deserialize<ICollection<Administrator>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return administrators;
    }

    public async Task<Administrator?> GetAdministratorByIdAsync(int id)
    {
        await InitializeAsync();

        var result = await _httpClient.GetAsync($"{_apiUrl}/{id}");
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var administrator = JsonSerializer.Deserialize<Administrator>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return administrator;
    }

    public async Task<Administrator?> AddAdministratorAsync(MultipartFormDataContent dataContent)
    {
        await InitializeAsync();

        var result = await _httpClient.PostAsync($"{_apiUrl}", dataContent);
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var administrator = JsonSerializer.Deserialize<Administrator>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return administrator;
    }

    public async Task<Administrator?> UpdateAdministratorPasswordAsync(int id, MultipartFormDataContent dataContent)
    {
        await InitializeAsync();

        var result = await _httpClient.PutAsync($"{_apiUrl}/{id}/update/password", dataContent);
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var administrator = JsonSerializer.Deserialize<Administrator>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return administrator;
    }

    public async Task<Administrator?> UpdateAdministratorEmailAsync(int id, MultipartFormDataContent dataContent)
    {
        await InitializeAsync();

        var result = await _httpClient.PutAsync($"{_apiUrl}/{id}/update/email", dataContent);
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var administrator = JsonSerializer.Deserialize<Administrator>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return administrator;
    }

    public async Task<Administrator?> PersistAdministratorAsync(MultipartFormDataContent dataContent)
    {
        await InitializeAsync();

        Console.WriteLine(dataContent.ReadAsStringAsync().Result);
        
        var result = await _httpClient.PostAsync($"{_apiUrl}/token", dataContent);
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var administrator = JsonSerializer.Deserialize<Administrator>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        await _sessionValues.SetSessionUser(administrator!);
        return administrator;
    }

    public async Task<Administrator?> LoginAdministratorAsync(MultipartFormDataContent dataContent, bool rememberMe)
    {
        await InitializeAsync();
        
        var result = await _httpClient.PostAsync($"{_apiUrl}/login", dataContent);
        if (!result.IsSuccessStatusCode) return null;
        
        var content = await result.Content.ReadAsStringAsync();
        var administrator = JsonSerializer.Deserialize<Administrator>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        if (rememberMe) await _sessionValues.SetCookieAsync("token", administrator?.Token!);
        await _sessionValues.SetSessionUser(administrator!);
        
        return administrator;
    }

    public async Task LogoutAdministratorAsync()
    {
        await _sessionValues.RemoveSessionUser();
    }

    public async Task<Administrator?> DeleteAdministratorAsync(int id)
    {
        await InitializeAsync();

        var result = await _httpClient.DeleteAsync($"{_apiUrl}/{id}/delete");
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var administrator = JsonSerializer.Deserialize<Administrator>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return administrator;
    }

    #endregion
}