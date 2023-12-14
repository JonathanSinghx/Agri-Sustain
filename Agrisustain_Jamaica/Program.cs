using Agrisustain_Jamaica.DataAccess;
using Agrisustain_Jamaica.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Agrisustain_Jamaica.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Agrisustain_JamaicaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Agrisustain_JamaicaContext") ?? throw new InvalidOperationException("Connection string 'Agrisustain_JamaicaContext' not found.")));

// Add services to the container.
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
    // Replace "YourConnectionString" with your actual database connection string
    //options.UseSqlServer("YourConnectionString");
//});

// Add other services to the container.
builder.Services.AddTransient<JSONFileIrrigationService, JSONFileIrrigationService>();
builder.Services.AddTransient<JSONFilePlantingGuidesService, JSONFilePlantingGuidesService>();
builder.Services.AddTransient<JSONFileCropCareService, JSONFileCropCareService>();
//builder.Services.AddTransient<WeatherService, WeatherService>();
builder.Services.AddScoped<IWeatherService, WeatherForecastService>();
//builder.Services.AddHttpClient<WeatherForecastService>();
builder.Services.AddScoped<AddToAgrisustainDB,  AddToAgrisustainDB>();
builder.Services.AddScoped<RetrieveFromAgrisustainDB,  RetrieveFromAgrisustainDB>();
builder.Services.AddScoped<UpdateAgrisustainDB, UpdateAgrisustainDB>();
builder.Services.AddScoped<DeleteFromAgrisustainDB,  DeleteFromAgrisustainDB>();
builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Apply any pending migrations
//using (var scope = app.Services.CreateScope())
//{
    //var services = scope.ServiceProvider;
    //var dbContext = services.GetRequiredService<ApplicationDbContext>();
   // dbContext.Database.Migrate();
//}

//app.MapGet("/", () => Results.Text("Hello World!"));

app.Run();
