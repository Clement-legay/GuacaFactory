using GuacaFactory.Shared.Models;

namespace GuacaFactory.Shared.Requests.EmployeeRequests;

public interface IEmployeeRequests
{
    Task<ICollection<Employee>?> GetEmployeesAsync(int page, int rows);
    Task<Employee?> GetEmployeeByIdAsync(int id);
    Task<ICollection<Employee>?> GetEmployeesBySiteIdAsync(int siteId, int? serviceId, string search, int page, int rows);
    Task<ICollection<Employee>?> GetEmployeesByServiceIdAsync(int serviceId, int? siteId, string search, int page, int rows);
    Task<ICollection<Employee>?> GetEmployeesByNameAsync(string name, int? serviceId, int? siteId, int page, int rows);
    Task<Employee?> AddEmployeeAsync(MultipartFormDataContent dataContent);
    Task<Employee?> UpdateEmployeeAsync(int id, MultipartFormDataContent dataContent);
    Task<Employee?> DeleteEmployeeAsync(int id);
}