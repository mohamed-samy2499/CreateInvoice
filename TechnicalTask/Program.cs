using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using DataAccessLayer.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechnicalTask.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();
//registering the mvc
builder.Services.AddControllersWithViews();
//registering the api
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(connectionString)
           );
//registering the business repositores with their inmterfaces to the DI
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//registering the automappers
builder.Services.AddAutoMapper(M => M.AddProfile(new InvoiceProfile()));


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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
//Configuring Routing for mvc
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//Configuring Routing for api
app.MapControllers();
app.MapRazorPages();

app.Run();
