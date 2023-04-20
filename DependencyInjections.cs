using GuacaFactory.Shared;
using GuacaFactory.Shared.Requests.AddressRequests;
using GuacaFactory.Shared.Requests.AdministratorRequests;
using GuacaFactory.Shared.Requests.DocumentRequests;
using GuacaFactory.Shared.Requests.DocumentTypeRequests;
using GuacaFactory.Shared.Requests.EmployeeRequests;
using GuacaFactory.Shared.Requests.ServiceRequests;
using GuacaFactory.Shared.Requests.SiteRequests;

namespace GuacaFactory;

public static class DependencyInjections
{
    public static void AddInjections(this IServiceCollection services)
    {
        services.AddScoped<IAddressRequests, AddressRequests>();
        services.AddScoped<IDocumentRequests, DocumentRequests>();
        services.AddScoped<IDocumentTypeRequests, DocumentTypeRequests>();
        services.AddScoped<IEmployeeRequests, EmployeeRequests>();
        services.AddScoped<IServiceRequests, ServiceRequests>();
        services.AddScoped<ISiteRequests, SiteRequests>();
        services.AddScoped<IAdministratorRequests, AdministratorRequests>();
        
        services.AddDistributedMemoryCache();
        services.AddScoped<ISessionValues, SessionValues>();
    }
}