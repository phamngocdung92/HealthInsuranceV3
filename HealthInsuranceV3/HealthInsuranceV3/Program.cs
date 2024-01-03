using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HealthInsuranceV3.Data;
using HealthInsuranceV3.Areas.Identity.Data;
using HealthInsuranceV3.Areas.User.Repositories.ListInsuranceRepository;
using HealthInsuranceV3.Areas.User.Services.ListInsuranceService;
using HealthInsuranceV3.Entities;
using HealthInsuranceV3.Areas.User.Repositories.InsuranceDetailRepository;
using HealthInsuranceV3.Areas.User.Services.InsuranceDetailService;
using HealthInsuranceV3.Areas.User.Repositories.UserInsuranceManagerRepository;
using HealthInsuranceV3.Areas.User.Services.UserInsuranceManagerService;
using HealthInsuranceV3.Areas.User.Repositories.ForManagerRepository;
using HealthInsuranceV3.Areas.User.Services.ForManagerService;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("HealthInsuranceV3ContextConnection") ?? throw new InvalidOperationException("Connection string 'HealthInsuranceV3ContextConnection' not found.");
builder.Services.AddDbContext<HealthInsuranceV3Context>(options => options.UseSqlServer(connectionString));

string mainDbConnectionString = builder.Configuration.GetConnectionString("MainDBContextConnection");
builder.Services.AddDbContext<HealthInsuranceContext>(
    options => options.UseSqlServer(mainDbConnectionString)
);

//add new service and repo here
builder.Services.AddScoped<IListInsuranceRepository, ListInsuranceRepository>();
builder.Services.AddScoped<IListInsuranceService, ListInsuranceService>();
builder.Services.AddScoped<IInsuranceDetailRepository, InsuranceDetailRepository>();
builder.Services.AddScoped<IInsuranceDetailService, InsuranceDetailService>();
builder.Services.AddScoped<IUserInsuranceManagerRepository, UserInsuranceManagerRepository>();
builder.Services.AddScoped<IUserInsuranceManagerService, UserInsuranceManagerService>();
builder.Services.AddScoped<IForManagerRepository, ForManagerRepository>();
builder.Services.AddScoped<IForManagerService, ForManagerService>();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<HealthInsuranceV3Context>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
