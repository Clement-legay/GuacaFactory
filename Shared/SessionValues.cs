using System.Text;
using System.Text.Json;
using Blazored.SessionStorage;
using GuacaFactory.Shared.Models;
using Microsoft.JSInterop;

namespace GuacaFactory.Shared;

public class SessionValues : ISessionValues
{

    #region Fields

    private readonly IJSRuntime _jsRuntime;
    private readonly ISessionStorageService _sessionStorageService;

    #endregion

    #region Constructor

    public SessionValues(IJSRuntime jsRuntime, ISessionStorageService sessionStorageService)
    {
        _jsRuntime = jsRuntime;
        _sessionStorageService = sessionStorageService;
    }

    #endregion

    #region Methods
    
    public async Task TestAlert()
    {
        await _jsRuntime.InvokeVoidAsync("showAlert");
    }

    public async Task<string?> GetCookieAsync(string key)
    {
        var result = await _jsRuntime.InvokeAsync<string?>("getCookie", key);
        if (key == "token") result += "==";
        return result;
    }

    public async Task SetCookieAsync(string key, string value)
    {
        await _jsRuntime.InvokeVoidAsync("setCookie", key, value);
    }

    public async Task RemoveCookieAsync(string key)
    {
        await _jsRuntime.InvokeVoidAsync("deleteCookie", key);
    }
    
    public async Task SetSessionUser(Administrator administrator)
    {
        var serializedUser = JsonSerializer.Serialize(administrator);
        var encodedUser = Convert.ToBase64String(Encoding.UTF8.GetBytes(serializedUser));
        await _sessionStorageService.SetItemAsync("user", encodedUser);
    }
    
    public async Task<Administrator?> GetSessionUser()
    {
        var encodedUser = await _sessionStorageService.GetItemAsync<string>("user");
        if (encodedUser is null) return null;
        var decodedUser = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUser));
        var user = JsonSerializer.Deserialize<Administrator>(decodedUser);
        return user;
    }
    
    public async Task RemoveSessionUser()
    {
        await _sessionStorageService.RemoveItemAsync("user");
    }

    #endregion
}

public interface ISessionValues
{
    Task TestAlert();
    Task SetSessionUser(Administrator administrator);
    Task<Administrator?> GetSessionUser();
    Task RemoveSessionUser();
    Task<string?> GetCookieAsync(string key);
    Task SetCookieAsync(string key, string value);
    Task RemoveCookieAsync(string key);
}