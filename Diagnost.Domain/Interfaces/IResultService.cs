using Diagnost.Domain.Models;

namespace Diagnost.Domain.Interfaces
{
    public interface IResultService
    {
        Task<(Error, Result?)> CreateAsync(Result result);
        Task<(Error, Result?)> DeleteAsync(long id);
        Task<(Error, List<Result>?)> GetAsync();
        Task<(Error, Result?)> GetAsync(long id);
    }
}
