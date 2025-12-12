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

            Result? resultModel = await _context.Results.FirstOrDefaultAsync(x => x.Id == resultId);
            if (accessCode == null)
            {
                return (
                    new Error(true, "Результат не існує"),
                    null
                    );
            }

            if (testType == ResultType.PZMR)
            {
                resultModel.PZMRChtoToTam1 = result.PZMRChtoToTam1;
                resultModel.PZMRSmth2 = result.PZMRSmth2;
                resultModel.PZMR_ErrorsTotal = result.PZMR_ErrorsTotal;
                resultModel.PZMR_SuccessfulClicks = result.PZMR_SuccessfulClicks;
            }
            else if (testType == ResultType.PV2)
            {
                resultModel.PV2_3Smth1 = result.PV2_3Smth1;
                resultModel.PV2_StdDev_ms = result.PV2_StdDev_ms;
                resultModel.PV2_ErrorsMissed = result.PV2_ErrorsMissed;
                resultModel.PV2_ErrorsWrongButton = result.PV2_ErrorsWrongButton;
                resultModel.PV2_ErrorsFalseAlarm = result.PV2_ErrorsFalseAlarm;
            }
            else if (testType == ResultType.UFP)
            {
                resultModel.UFP_MinExposure_ms = result.UFP_MinExposure_ms;
                resultModel.UFP_TotalTime_s = result.UFP_TotalTime_s;
                resultModel.UFP_TimeTillMinExp_s = result.UFP_TimeTillMinExp_s;
                resultModel.UFP_ErrorsMissed = result.UFP_ErrorsMissed;
                resultModel.UFP_ErrorsWrongButton = result.UFP_ErrorsWrongButton;
                resultModel.UFP_ErrorsFalseAlarm = result.UFP_ErrorsFalseAlarm;
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
