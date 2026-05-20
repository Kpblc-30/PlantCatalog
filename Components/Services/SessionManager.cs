using PlantCatalog.Components.Models;

namespace PlantCatalog.Components.Services;

public class SessionManager
{
    public User? CurrentUser { get; set; }
    public event Action? OnChange; // Событие для уведомления
    public void NotifyStateChanged() => OnChange?.Invoke();
}