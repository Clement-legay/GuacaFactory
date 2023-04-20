using GuacaFactory.Shared.Models;

namespace GuacaFactory.Shared.Requests.AddressRequests;

public interface IAddressRequests
{
    Task<ICollection<Address>?> GetAddressesAsync(int page, int rows);

    Task<Address?> GetAddressByIdAsync(int id);

    Task<Address?> AddAddressAsync(MultipartFormDataContent dataContent);

    Task<Address?> UpdateAddressAsync(int id, MultipartFormDataContent dataContent);

    Task<Address?> DeleteAddressAsync(int id);
}