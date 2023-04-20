using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using GuacaFactory.Shared.Models;

namespace GuacaFactory.Shared.Requests.EmployeeRequests;

public class EmployeeRequests : IEmployeeRequests
{
    #region Fields

    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly ISessionValues _sessionValues;
    private const string Url = "http://localhost:8080";
    private const string Version = "v1";

    #endregion

    #region Constructors

    public EmployeeRequests(HttpClient httpClient, ISessionValues sessionValues)
    {
        _httpClient = httpClient;
        _apiUrl = $"{Url}/api/{Version}/employee";
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

    public async Task<ICollection<Employee>?> GetEmployeesAsync(int page, int rows)
    {
        await InitializeAsync();

        var result = await _httpClient.GetAsync($"{_apiUrl}?page={page}&rows={rows}");
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var employees = JsonSerializer.Deserialize<ICollection<Employee>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return employees;
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        await InitializeAsync();

        var result = await _httpClient.GetAsync($"{_apiUrl}/{id}");
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var employee = JsonSerializer.Deserialize<Employee>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return employee;
    }

    public async Task<ICollection<Employee>?> GetEmployeesBySiteIdAsync(int siteId, int page, int rows)
    {
        await InitializeAsync();

        var result = await _httpClient.GetAsync($"{_apiUrl}/site/{siteId}?page={page}&rows={rows}");
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var employees = JsonSerializer.Deserialize<ICollection<Employee>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return employees;
    }

    public async Task<ICollection<Employee>?> GetEmployeesByServiceIdAsync(int serviceId, int page, int rows)
    {
        await InitializeAsync();

        var result = await _httpClient.GetAsync($"{_apiUrl}/service/{serviceId}?page={page}&rows={rows}");
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var employees = JsonSerializer.Deserialize<ICollection<Employee>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return employees;
    }

    public async Task<ICollection<Employee>?> GetEmployeesByNameAsync(string name, int page, int rows)
    {
        await InitializeAsync();

        var result = await _httpClient.GetAsync($"{_apiUrl}/search?name={name}&page={page}&rows={rows}");
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var employees = JsonSerializer.Deserialize<ICollection<Employee>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return employees;
    }

    public async Task<Employee?> AddEmployeeAsync(MultipartFormDataContent dataContent)
    {
        await InitializeAsync();

        var result = await _httpClient.PostAsync($"{_apiUrl}", dataContent);
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var employee = JsonSerializer.Deserialize<Employee>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return employee;
    }

    public async Task<Employee?> UpdateEmployeeAsync(int id, MultipartFormDataContent dataContent)
    {
        await InitializeAsync();

        var result = await _httpClient.PutAsync($"{_apiUrl}/{id}/update", dataContent);
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var employee = JsonSerializer.Deserialize<Employee>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return employee;
    }

    public async Task<Employee?> DeleteEmployeeAsync(int id)
    {
        await InitializeAsync();

        var result = await _httpClient.DeleteAsync($"{_apiUrl}/{id}/delete");
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var employee = JsonSerializer.Deserialize<Employee>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return employee;
    }

    #endregion
}