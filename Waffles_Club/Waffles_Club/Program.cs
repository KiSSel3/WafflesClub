using NLog;
using NLog.Web;
using Waffles_Club.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

builder.AddDataBase();
builder.AddAuthentication();
builder.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();