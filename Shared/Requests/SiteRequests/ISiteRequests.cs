using GuacaFactory.Shared.Models;

namespace GuacaFactory.Shared.Requests.SiteRequests;

public interface ISiteRequests
{
    Task<ICollection<Site>?> GetSitesAsync(int page, int rows);
    Task<Site?> GetSiteByIdAsync(int id);
    Task<Site?> AddSiteAsync(MultipartFormDataContent dataContent);
    Task<Site?> UpdateSiteAsync(int id, MultipartFormDataContent dataContent);
    Task<Site?> DeleteSiteAsync(int id);
}