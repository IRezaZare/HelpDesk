using AspNetCoreRateLimit;
using HelpDesk.Data;
using HelpDesk.Entities;
using HelpDesk.Interfaces;
using HelpDesk.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
//Connect to the Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//Add Identity to the project

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddUserManager<CustomUserManager>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();
//Add MVC
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//DI
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddMemoryCache(options =>
{
    
});



//Rate Limit
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.GeneralRules =
    [
        new RateLimitRule
        {
            Endpoint = "*", 
            Limit = 5,
            Period = "10s" 
        }
    ];
});
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>(); //DI Rate Limit


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseIpRateLimiting();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
