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
        string? code = null;
        int codeLength = 6;

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        int attempts = 0;
        while (await _context.AccessCodes.FirstOrDefaultAsync(x => x.Code == code) != null)
        {
            attempts++;
            code = new string(Enumerable.Repeat(chars, codeLength).Select(x => x[random.Next(x.Length)]).ToArray());
            if (attempts > 10)
            {
                return (
                    new Error(true, "Помилка під час створення ключа доступу, спробуйте ще раз!"),
                    null
                );
            }
        }

        if (code == null)
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
        catch (Exception e)
        {
            Console.WriteLine(e);
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
        List<AccessCode> accessCodes = await _context.AccessCodes.ToListAsync();
        try
        {
            accessCodes = await _context.AccessCodes.ToListAsync();
        }
        catch (Exception e)
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
        AccessCode? accesCode = await _context.AccessCodes.FirstOrDefaultAsync(x => x.Code == accessCode);
        if (accesCode == null)
        {
            return (
                new Error(true, "Код доступу не знайдено!"),
                null
                );
        }

        return (
            new Error(false),
            accesCode
            );
    }
    
    // Update
    public async Task<(Error, AccessCode?)> Revoke(string code)
    {
        AccessCode? accessCode = await _context.AccessCodes.FirstOrDefaultAsync(x => x.Code == code);
        if (accessCode == null)
        {
            return (
                new Error(true, "Код доступу не знайдено!"),
                null
            );
        }

        try
        {
            _context.AccessCodes.Remove(accessCode);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
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
}