namespace PlantCatalog.Components.Models;

public class User
{
    public int Id { get; set; }
    
    // Ваше имя (отображаемое имя)
    public string? Name { get; set; }
    
    // Логин для входа
    public string Login { get; set; } = string.Empty;
    
    // Хэш пароля (мы не будем хранить пароли в открытом виде, это стандарт безопасности)
    public string PasswordHash { get; set; } = string.Empty;
}