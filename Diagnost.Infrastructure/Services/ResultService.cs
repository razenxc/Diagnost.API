using Diagnost.Domain.Interfaces;
using Diagnost.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Diagnost.Infrastructure.Services
{
    public class ResultService : IResultService
    {
        private readonly ApplicationDbContext _context;
        public ResultService(ApplicationDbContext context) 
        { 
            _context = context;
        }

        // Create
        public async Task<(Error, Result?)> CreateAsync(Result result)
        {
            AccessCode? accessCode = await _context.AccessCodes.FindAsync(result.AccessCodeId);
            if (accessCode == null)
            {
                return (
                    new Error(true, "Код доступу не існує"), 
                    null
                    );
            }

            try
            {
                _context.Results.Add(result);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return (
                    new Error(true, "Помилка під час запису результатів до бази даних!"),
                    null
                    );
            }

            return (
                new Error(false, null),
                result
                );
        }

        // Read All
        public async Task<(Error, List<Result>?)> GetAsync()
        {
            List<Result> results = await _context.Results.ToListAsync();
            return (
                new Error(false, null),
                results
                );
        }

        // Read
        public async Task<(Error, Result?)> GetAsync(long id)
        {
            Result? result;

            try
            {
                result = await _context.Results.FindAsync(id);
            }
            catch (Exception)
            {
                return (
                    new Error(true, "Помилка під час отримання результату з бази даних!"),
                    null
                    );
            }

            if (result == null)
            {
                return (
                    new Error(true, "Результат не знайдено!"),
                    null
                    );
            }

            return (
                new Error(false, null),
                result
                );
        }

        // Delete
        public async Task<(Error, Result?)> DeleteAsync(long id)
        {
            Result? result;

            try
            {
                result = await _context.Results.FindAsync(id);
            }
            catch (Exception)
            {
                return (
                    new Error(true, "Помилка під час отримання результату з бази даних!"),
                    null
                    );
            }

            if (result == null)
            {
                return (
                    new Error(true, "Результат не знайдено!"),
                    null
                    );
            }

            try
            {
                _context.Results.Remove(result);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return (
                    new Error(true, "Помилка під час видалення результату з бази даних!"),
                    null
                    );
            }

            return (
                new Error(false, null),
                result
                );
        }
    }
}
