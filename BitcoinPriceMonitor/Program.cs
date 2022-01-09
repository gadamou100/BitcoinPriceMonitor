using BitcoinPriceMonitor;
using BitCoinPriceMonitor.Infrastructure.DependencyInjection;
using BitCoinPriceMonitor.Infrastructure.MiddleWare;
using BitCoinPriceMonitor.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.InjectDependencies(connectionString);
builder.Services.AddRazorPages();
builder.Services.AddMvc()
    .AddRazorRuntimeCompilation();
builder.InjectLogger();
var app = builder.Build();

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
app.UseCustomLoggingMidleware();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}
app.Run();
