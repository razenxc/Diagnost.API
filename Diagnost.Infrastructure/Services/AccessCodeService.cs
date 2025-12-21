using Diagnost.Domain;
using Diagnost.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Diagnost.Infrastructure.Services;

public class AccessCodeService : IAccessCodeService
{
    private readonly ApplicationDbContext _context;

    public AccessCodeService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // Create
    public async Task<(Error, AccessCode?)> CreateAsync()
    {
        Random random = new Random();
        string code = string.Empty;
        int codeLength = 6;

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        int attempts = 0;

        while (code == string.Empty)
        {
            attempts++;
            code = new string(Enumerable.Repeat(chars, codeLength).Select(x => x[random.Next(x.Length)]).ToArray());
            if (attempts > 10 || await _context.AccessCodes.FirstOrDefaultAsync(x => x.Code == code) != null)
            {
                return (
                    new Error(true, "Помилка під час створення ключа доступу, спробуйте ще раз!"),
                    null
                );
            }
        }

        if (code == string.Empty)
        {
            return (
                new Error(true, "Помилка під час створення ключа доступу, спробуйте ще раз"),
                null
            );
        }
        
        AccessCode newAccessCode = new AccessCode(Guid.NewGuid(), code, true, DateTime.UtcNow);

        try
        {
            await _context.AccessCodes.AddAsync(newAccessCode);
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return (
                new Error(true, "Помилка під час запису нового ключа доступу до бази даних!"),
            null
            );
        }
        
        return (
            new Error(false), 
            newAccessCode
            );
    }
    
    // Read All
    public async Task<(Error, List<AccessCode>?)> GetAsync()
    {
        List<AccessCode> accessCodes;
        try
        {
            accessCodes = await _context.AccessCodes.ToListAsync();
        }
        catch (Exception)
        {
            return (
                new Error(true, "Помилка під час отримання даних бази даних!"),
                null
            );
        }

        return (
            new Error(false),
            accessCodes
            );
    }
    
    // Read
    public async Task<(Error, AccessCode?)> GetAsync(string accessCode)
    {
        accessCode = accessCode.ToUpper();

        AccessCode? accessCodeModel;

        try
        {
            accessCodeModel = await _context.AccessCodes.FirstOrDefaultAsync(x => x.Code == accessCode);
        }
        catch (Exception)
        {
            return (
                new Error(true, "Помилка під час отримання коду доступу з бази даних!"),
                null
                );
        }

        if (accessCodeModel == null)
        {
            return (
                new Error(true, "Код доступу не знайдено!"),
                null
                );
        }

        return (
            new Error(false),
            accessCodeModel
            );
    }
    
    // Update
    public async Task<(Error, AccessCode?)> Revoke(string code)
    {
        code = code.ToUpper();

        AccessCode? accessCode;

        try
        {
            accessCode = await _context.AccessCodes.FirstOrDefaultAsync(x => x.Code == code);
        }
        catch (Exception)
        {
            return (
                new Error(true, "Помилка під час отримання коду доступу з бази даних!"),
                null
                );
        }

        if (accessCode == null)
        {
            return (
                new Error(true, "Код доступу не знайдено!"),
                null
            );
        }

        accessCode.IsActive = false;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return (
                new Error(true, "Виникла помилка під час деактивації коду!"),
                null
                );
        }

        return (
            new Error(false),
            accessCode
            );
    }

    // Delete
    public async Task<(Error, AccessCode?)> DeleteAsync(string code)
    {
        code = code.ToUpper();

        AccessCode? accessCode;

        try
        {
            accessCode = await _context.AccessCodes.FirstOrDefaultAsync(x => x.Code == code);
        }
        catch (Exception)
        {
            return (
                new Error(true, "Помилка під час отримання коду доступу з бази даних!"),
                null
                );
        }

        if (accessCode == null)
        {
            return (
                new Error(true, "Код доступу не знайдено!"),
                null
            );
        }

        try
        {
            _context.Remove(accessCode);
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return (
                new Error(true, "Виникла помилка під час видалення коду з бази даних!"),
                null
                );
        }

        return (
            new Error(false),
            accessCode
            );
    }

    // Verify
    public async Task<(Error, bool)> VerifyAsync(string code)
    {
        code = code.ToUpper();

        AccessCode? accessCode;
        try
        {
            accessCode = await _context.AccessCodes.FirstOrDefaultAsync(x => x.Code == code);
        }
        catch (Exception)
        {
            return (
                new Error(true, "Помилка під час отримання коду доступу з бази даних!"),
                false
                );
        }
        if (accessCode == null)
        {
            return (
                new Error(true, "Код доступу не знайдено!"),
                false
            );
        }
        if (!accessCode.IsActive)
        {
            return (
                new Error(true, "Код доступу не активний!"),
                false
            );
        }
        return (
            new Error(false),
            true
            );
    }
}