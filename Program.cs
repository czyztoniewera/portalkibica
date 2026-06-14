using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortalKibica.Data;
using PortalKibica.Models;

var builder = WebApplication.CreateBuilder(args);

// Połączenie z bazą danych
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Seed konta administratora
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    string email = "admin@wisla.pl";
    string password = "Admin123!";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(user, password);
    }
}

// Seed przykladowych danych (tylko jesli tabele sa puste)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!context.Players.Any())
    {
        context.Players.AddRange(
            new Player { Name = "Kamil Broda", Position = "Bramkarz", Number = 1, Description = "DoŚwiadczony bramkarz, kapitan zespoŁu." },
            new Player { Name = "Anton Chichkan", Position = "Bramkarz", Number = 31, Description = "Drugi bramkarz w sezonie 2025/2026." }
        );
        context.SaveChanges();
    }

    if (!context.News.Any())
    {
        context.News.AddRange(
            new News
            {
                Title = "Wisła wygrywa derby Krakowa",
                Content = "Dnia 7 listopada 2021 roku zespół Wisły Kraków odniósł bardzo ważne zwycięstwo w derbach miasta Krakowa. Kibice na stadionie stworzyli niesamowitą atmosferę przez całe 90 minut. Trener pochwalił zaangazowanie całej drużyny i podkreslił znaczenie tego zwycięstwa dla morali zespołu przed kolejnymi spotkaniami w lidze.",
                PublishDate = new DateTime(2026, 6, 1, 18, 0, 0)
            }
        );
        context.SaveChanges();
    }

    if (!context.Matches.Any())
    {
        context.Matches.AddRange(
            new Match { Opponent = "Widzew Łódz", MatchDate = new DateTime(2026, 9, 5, 20, 0, 0), Stadium = "Stadion Miejski im. Henryka Reymana" },
            new Match { Opponent = "Zagłębie Lublin", MatchDate = new DateTime(2026, 8, 2, 18, 0, 0), Stadium = "Stadion KGHM Zagłębie Arena" }
        );
        context.SaveChanges();
    }
}

app.Run();