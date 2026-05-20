using PlantCatalog.Components;
using Microsoft.EntityFrameworkCore;
using PlantCatalog.Components.Data;
using PlantCatalog.Components.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Регистрация DbContext для работы с базой данных
builder.Services.AddDbContext<PlantDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация сервисов
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<CartService>();

// 👇 НОВЫЕ СЕРВИСЫ ДЛЯ АВТОРИЗАЦИИ
builder.Services.AddScoped<UserService>();      // Логика регистрации/входа
builder.Services.AddScoped<SessionManager>();   // Хранение текущего пользователя

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found");
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// --- АВТОЗАПОЛНЕНИЕ БАЗЫ (35 ПОЗИЦИЙ) ---
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PlantCatalog.Components.Data.PlantDbContext>();
    
    // Добавляем только если база пустая (чтобы не дублировать при каждом запуске)
    if (!context.Products.Any())
    {
        var items = new[]
        {
            // 🌿 РАСТЕНИЯ 🌿 (25 шт.)
            new { Name = "Монстера Делициоза", Category = "Растения", Price = 2500m, Desc = "Тропическое растение с крупными резными листьями" },
            new { Name = "Фикус Бенджамина", Category = "Растения", Price = 1800m, Desc = "Популярное деревце для дома и офиса" },
            new { Name = "Сансевиерия", Category = "Растения", Price = 1200m, Desc = "Неприхотливое растение, очищает воздух" },
            new { Name = "Спатифиллум", Category = "Растения", Price = 1500m, Desc = "Женское счастье, цветет белыми цветами" },
            new { Name = "Драцена Маргината", Category = "Растения", Price = 2200m, Desc = "Симпатичная пальма с тонкими листьями" },
            new { Name = "Алоэ Вера", Category = "Растения", Price = 900m, Desc = "Лекарственное растение, неприхотлив в уходе" },
            new { Name = "Замиокулькас", Category = "Растения", Price = 2800m, Desc = "Долларовое дерево, теневыносливое" },
            new { Name = "Кротон", Category = "Растения", Price = 1600m, Desc = "Имеет яркие разноцветные листочки" },
            new { Name = "Папоротник Нефролепис", Category = "Растения", Price = 1100m, Desc = "Любит влажность и рассеянный свет" },
            new { Name = "Орхидея Фаленопсис", Category = "Растения", Price = 1900m, Desc = "Элегантное цветущее растение" },
            new { Name = "Кактус Эхинокактус", Category = "Растения", Price = 700m, Desc = "Круглый кактус для солнечного подоконника" },
            new { Name = "Эхеверия", Category = "Растения", Price = 600m, Desc = "Суккулент в форме розетки" },
            new { Name = "Плющ Обыкновенный", Category = "Растения", Price = 800m, Desc = "Вьющееся растение для вертикального озеленения" },
            new { Name = "Аспарагус Перистый", Category = "Растения", Price = 950m, Desc = "Воздушное ажурное растение" },
            new { Name = "Хлорофитум", Category = "Растения", Price = 750m, Desc = "Отлично очищает воздух, безопасен для животных" },
            new { Name = "Герань Зональная", Category = "Растения", Price = 850m, Desc = "Яркое цветение всё лето" },
            new { Name = "Бегония Вечноцветущая", Category = "Растения", Price = 650m, Desc = "Компактное растение с непрерывным цветением" },
            new { Name = "Фиалка Узамбарская", Category = "Растения", Price = 550m, Desc = "Классический комнатный цветок" },
            new { Name = "Антуриум", Category = "Растения", Price = 2100m, Desc = "Мужское счастье, ярко-красные соцветия-покрывала" },
            new { Name = "Аглаонема", Category = "Растения", Price = 1400m, Desc = "Тенелюбивое растение с пёстрыми листочками" },
            new { Name = "Шефлера", Category = "Растения", Price = 2400m, Desc = "Древовидное растение-зонтик" },
            new { Name = "Калатея", Category = "Растения", Price = 1700m, Desc = "Поднимает листья на ночь, любит влажность" },
            new { Name = "Сингониум", Category = "Растения", Price = 1300m, Desc = "Быстрорастущая лиана" },
            new { Name = "Маранта", Category = "Растения", Price = 1250m, Desc = "Молитвенное растение с узорчатыми листочками" },
            new { Name = "Стапелия", Category = "Растения", Price = 900m, Desc = "Суккулент с необычными звездчатыми цветочками" },

            // 🪴 ГОРШКИ 🪴 (3 шт.)
            new { Name = "Горшочек керамический S", Category = "Горшки", Price = 450m, Desc = "Диаметр 10 см. Идеально подойдёт для кактусов и суккулентов" },
            new { Name = "Горшочек керамический M", Category = "Горшки", Price = 750m, Desc = "Диаметр 15 см. Отлично подходит для большинства комнатных растений" },
            new { Name = "Горшок керамический L", Category = "Горшки", Price = 1200m, Desc = "Диаметр 20 см. Для крупных растений и пальм" },

            // 🛠 ИНСТРУМЕНТЫ И УХОД 🛠 (7 шт.)
            new { Name = "Совок садовый", Category = "Инструменты", Price = 350m, Desc = "Нержавеющая сталь, удобная ручка" },
            new { Name = "Грабельки ручные", Category = "Инструменты", Price = 280m, Desc = "Для рыхления почвы в горшках" },
            new { Name = "Лейка 1.5л", Category = "Уход", Price = 650m, Desc = "Пластик, длинный носик для точного полива" },
            new { Name = "Опрыскиватель", Category = "Уход", Price = 420m, Desc = "Для увлажнения листьев и создания микроклимата" },
            new { Name = "Грунт универсальный 5л", Category = "Уход", Price = 380m, Desc = "Питательная смесь для всех видов растений" },
            new { Name = "Удобрение жидкое", Category = "Уход", Price = 290m, Desc = "Комплекс NPK для роста и цветения" },
            new { Name = "Керамзит дренажный 1л", Category = "Уход", Price = 150m, Desc = "Обеспечивает воздухообмен в корнях" }
        };

        foreach (var item in items)
        {
            context.Products.Add(new PlantCatalog.Components.Models.Product
            {
                Name = item.Name,
                Category = item.Category,
                Price = item.Price,
                Description = item.Desc,
                ImageUrl = $"https://placehold.co/400x300/2E8B57/FFFFFF?text={item.Name.Replace(" ", "+")}"
            });
        }
        
        context.SaveChanges();
        Console.WriteLine("✅ База данных успешно заполнена 35 товарами!");
    }
}
// -----------------------------------------

app.Run();