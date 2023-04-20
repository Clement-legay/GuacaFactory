using GuacaFactory.Shared.Models;

namespace GuacaFactory.Shared.Requests.ServiceRequests;

public interface IServiceRequests
{
    Task<ICollection<Service>?> GetServicesAsync(int page, int rows);
    Task<Service?> GetServiceByIdAsync(int id);
    Task<Service?> AddServiceAsync(MultipartFormDataContent dataContent);
    Task<Service?> UpdateServiceAsync(int id, MultipartFormDataContent dataContent);
    Task<Service?> DeleteServiceAsync(int id);
}