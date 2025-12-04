using Diagnost.Domain.Models;

namespace Diagnost.Domain;

public interface IAccessCodeService
{
    Task<(Error, AccessCode?)> CreateAsync();
    Task<(Error, List<AccessCode>?)> GetAsync();
}