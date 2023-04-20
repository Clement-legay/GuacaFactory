using GuacaFactory.Shared.Models;

namespace GuacaFactory.Shared.Requests.AdministratorRequests;

public interface IAdministratorRequests
{
    Task<ICollection<Administrator>?> GetAdministratorsAsync(int page, int rows);
    Task<Administrator?> GetAdministratorByIdAsync(int id);
    Task<Administrator?> AddAdministratorAsync(MultipartFormDataContent dataContent);
    Task<Administrator?> UpdateAdministratorPasswordAsync(int id, MultipartFormDataContent dataContent);
    Task<Administrator?> UpdateAdministratorEmailAsync(int id, MultipartFormDataContent dataContent);
    Task<Administrator?> PersistAdministratorAsync(MultipartFormDataContent dataContent);
    Task<Administrator?> LoginAdministratorAsync(MultipartFormDataContent dataContent, bool rememberMe);
    Task LogoutAdministratorAsync();
    Task<Administrator?> DeleteAdministratorAsync(int id);
}