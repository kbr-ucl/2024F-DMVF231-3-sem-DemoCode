using System.Diagnostics;
using DomainCentricDemo.Web.Data;
using DomainCentricDemo.Web.Data.DataInitializer;
using DomainCentricDemo.Web.Infrastructure;
using DomainCentricDemo.Web.Infrastructure.Implementation;
using DomainCentricDemo.Web.UserManagement;
using DomainCentricDemo.Web.UserManagement.Handler;
using DomainCentricDemo.Web.UserManagement.Requirement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<IBookApiProxy, BookApiProxy>
(
    client =>
    {
        client.BaseAddress =
            new Uri(builder.Configuration["BackendBaseUri"] ?? String.Empty);
    }
);

builder.Services.AddHttpClient<IWetherApiProxy, WetherApiProxy>
(
    client =>
    {
        client.BaseAddress =
            new Uri(builder.Configuration["WetherBaseUri"] ?? String.Empty);
    }
);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add-Migration InitUser -Project DomainCentricDemo.Web -Context ApplicationDbContext
// Update-Database -Context ApplicationDbContext
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Lockout.AllowedForNewUsers = true;
        options.Password.RequiredLength = 3;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();

builder.Services.AddRazorPages();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        "IsAdminPolicy",
        policyBuilder => policyBuilder
            .RequireClaim(ClaimsTypes.Admin));
    options.AddPolicy(
        "IsSoleAuthorOrAdminPolicy",
        policyBuilder => policyBuilder.AddRequirements(
            new IsSoleAuthorOrAdminRequirement()
        ));
});

builder.Services.AddScoped<IAuthorizationHandler, IsSoleAuthorOrAdminHandler>();

// https://docs.automapper.org/en/stable/Getting-started.html
// https://stackoverflow.com/questions/71216149/how-to-setup-automapper-in-asp-net-core-6
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies(), ServiceLifetime.Scoped);

var app = builder.Build();

SeedUsers();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();


void SeedUsers()
{
    using var scope = app.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        UserAndRoleDataInitializer.SeedDataAsync(userManager, roleManager).Wait();
    }
    catch (Exception ex)
    {
        Debug.WriteLine(ex.Message);
    }
}