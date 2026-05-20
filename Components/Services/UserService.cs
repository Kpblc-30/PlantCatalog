using PlantCatalog.Components.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PlantCatalog.Components.Models;
using PlantCatalog.Components.Data;

namespace PlantCatalog.Components.Services;

public class UserService
{
    private readonly PlantDbContext _context;

    public UserService(PlantDbContext context)
    {
        _context = context;
    }

    // 1. РЕГИСТРАЦИЯ (Добавление в БД)
    public async Task<bool> RegisterAsync(string name, string login, string password)
    {
        // Проверяем, не занят ли логин
        if (await _context.Users.AnyAsync(u => u.Login == login)) 
            return false;

        var newUser = new User 
        { 
            Name = name, 
            Login = login, 
            PasswordHash = HashPassword(password) // Шифруем пароль перед сохранением!
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        return true;
    }

    // 2. ВХОД (Проверка данных)
    public async Task<User?> LoginAsync(string login, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        if (user == null) return null;

        // Сверяем введенный пароль с зашифрованным в базе
        if (VerifyPassword(password, user.PasswordHash)) 
            return user;
        
        return null;
    }

    // 3. ПОИСК ПОЛЬЗОВАТЕЛЯ (нужно для корзины)
    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    // --- Служебные методы для шифрования (Безопасность) ---
    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    private bool VerifyPassword(string password, string hash)
    {
        return HashPassword(password) == hash;
    }
}