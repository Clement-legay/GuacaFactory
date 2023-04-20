using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using GuacaFactory.Shared.Models;

namespace GuacaFactory.Shared.Requests.AddressRequests;

public class AddressRequests : IAddressRequests
{
    #region Fields

    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly ISessionValues _sessionValues;
    private const string Url = "http://localhost:8080";
    private const string Version = "v1";

    #endregion

    #region Constructors

    public AddressRequests(HttpClient httpClient, ISessionValues sessionValues)
    {
        _httpClient = httpClient;
        _apiUrl = $"{Url}/api/{Version}/address";
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

    public async Task<ICollection<Address>?> GetAddressesAsync(int page, int rows)
    {
        await InitializeAsync();

        var result = await _httpClient.GetAsync($"{_apiUrl}?page={page}&rows={rows}");
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var addresses = JsonSerializer.Deserialize<ICollection<Address>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return addresses;
    }

    public async Task<Address?> GetAddressByIdAsync(int id)
    {
        await InitializeAsync();

        var result = await _httpClient.GetAsync($"{_apiUrl}/{id}");
        if (!result.IsSuccessStatusCode) return null;

        var content = await result.Content.ReadAsStringAsync();
        var address = JsonSerializer.Deserialize<Address>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return address;
    }

    public async Task<Address?> AddAddressAsync(MultipartFormDataContent dataContent)
    {
        await InitializeAsync();

        var result = await _httpClient.PostAsync($"{_apiUrl}", dataContent);
        if (!result.IsSuccessStatusCode) return null;

        var responseContent = await result.Content.ReadAsStringAsync();
        var address = JsonSerializer.Deserialize<Address>(responseContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return address;
    }

    public async Task<Address?> UpdateAddressAsync(int id, MultipartFormDataContent dataContent)
    {
        await InitializeAsync();

        var result = await _httpClient.PutAsync($"{_apiUrl}/{id}/update", dataContent);
        if (!result.IsSuccessStatusCode) return null;

        var responseContent = await result.Content.ReadAsStringAsync();
        var address = JsonSerializer.Deserialize<Address>(responseContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return address;
    }

    public async Task<Address?> DeleteAddressAsync(int id)
    {
        await InitializeAsync();

        var result = await _httpClient.DeleteAsync($"{_apiUrl}/{id}/delete");
        if (!result.IsSuccessStatusCode) return null;

        var responseContent = await result.Content.ReadAsStringAsync();
        var address = JsonSerializer.Deserialize<Address>(responseContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return address;
    }

    #endregion
}