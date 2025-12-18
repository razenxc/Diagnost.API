using Diagnost.Domain.Models;

namespace Diagnost.Domain;

public interface IAccessCodeService
{
    Task<(Error, AccessCode?)> CreateAsync();
    Task<(Error, AccessCode?)> DeleteAsync(string code);
    Task<(Error, List<AccessCode>?)> GetAsync();
    Task<(Error, AccessCode?)> GetAsync(string accessCode);
    Task<(Error, AccessCode?)> Revoke(string code);
    Task<(Error, bool)> VerifyAsync(string code);
}