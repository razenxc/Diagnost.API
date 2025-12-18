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
        public async Task<(Error, Result?)> CreateAsync(Result result, string accesCode)
        {
            AccessCode? accessCode = await _context.AccessCodes.FirstOrDefaultAsync(x => x.Code == accesCode);
            if (accessCode == null)
            {
                return (
                    new Error(true, "Код доступу не існує"),
                    null
                    );
            }

            result.SubmittedAt = DateTime.UtcNow;
            result.AccessCodeId = accessCode.Id;
            result.AccessCode = accessCode;

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

        public async Task<(Error, Result?)> SaveTestAsync(Result result, string accesCode, long resultId, ResultType testType)
        {
            AccessCode? accessCode = await _context.AccessCodes.FirstOrDefaultAsync(x => x.Code == accesCode);
            if (accessCode == null)
            {
                return (
                    new Error(true, "Код доступу не існує"),
                    null
                    );
            }

            Result? resultModel = await _context.Results
                .Include(r => r.AccessCode)
                .FirstOrDefaultAsync(x => x.Id == resultId);
            if (accessCode == null)
            {
                return (
                    new Error(true, "Результат не існує"),
                    null
                    );
            }

            if (testType == ResultType.PZMR)
            {
                resultModel.PZMRLatet = result.PZMRLatet;
                resultModel.PZMRvidhil = result.PZMRvidhil;
                resultModel.PZMR_ErrorsTotal = result.PZMR_ErrorsTotal;
            }
            else if (testType == ResultType.PV2_3)
            {
                resultModel.PV2_3Latet = result.PV2_3Latet;
                resultModel.PV2_StdDev_ms = result.PV2_StdDev_ms;
                resultModel.PV2_ErrorsTotal = result.PV2_ErrorsTotal;
                resultModel.PV2_ErrorsMissed = result.PV2_ErrorsMissed;
                resultModel.PV2_ErrorsWrongButton = result.PV2_ErrorsWrongButton;
                resultModel.PV2_ErrorsFalseAlarm = result.PV2_ErrorsFalseAlarm;
            }
            else if (testType == ResultType.UFP)
            {
                resultModel.UFPLatet = result.UFPLatet;
                resultModel.UFP_StdDev_ms = result.UFP_StdDev_ms;
                resultModel.UFP_ErrorsTotal = result.UFP_ErrorsTotal;
            }
            else
            {
                return (
                    new Error(true, "Тип тесту не вказано!"),
                    null
                    );
            }

            try
            {
                _context.Results.Update(resultModel);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return (
                    new Error(true, "Помилка під час оновлення результатів до бази даних!"),
                    null
                    );
            }

            return (
                new Error(false, null),
                resultModel
                );
        }

        // Read All
        public async Task<(Error, List<Result>?)> GetAsync()
        {
            List<Result> results = await _context.Results
                .Include(r => r.AccessCode)
                .ToListAsync();

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
                result = await _context.Results
                    .Include(r => r.AccessCode)
                    .FirstOrDefaultAsync(r => r.Id == id);
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
                result = await _context.Results
                    .Include(r => r.AccessCode)
                    .FirstOrDefaultAsync(r => r.Id == id);
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
