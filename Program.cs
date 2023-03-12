using Aeroliner.Models;
using Aeroliner.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
          builder =>
          {
              builder.WithOrigins("http://localhost:3000")
                     .AllowAnyMethod()
                     .AllowAnyHeader();
          });
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Mongo Store Database Settings and Flights Service
builder.Services.Configure<MongoStoreDatabaseSettings>(builder.Configuration.GetSection("MongoStoreDatabase"));
builder.Services.AddSingleton<FlightsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseFileServer(); //for serving static files and default file types

app.UseRouting();

app.UseCors("AllowAnyOrigin");

app.MapFallbackToFile("index.html");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers().RequireCors("AllowAnyOrigin");
});

app.Run();

