using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.AspNetCore.SpaServices.Extensions;
using Aeroliner.Models;
using Aeroliner.Services;

var builder = WebApplication.CreateBuilder(args);

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

app.MapFallbackToFile("index.html");

//app.UseSpa(spa =>
//{
//    spa.Options.SourcePath = "ClientApp";


//    if (app.Environment.IsDevelopment())
//    {
//        // Start both Angular and React development servers
//        spa.UseConcurrently(
//            "start:angular": "cd angular-app && ng serve",
//            "start:react": "cd react-app && npm start"
//        );
//    }
//    else
//    {
//        // Serve pre-built bundles for production
//        spa.UseReactDevelopmentServer("react-app");
//        spa.UseAngularCliServer("angular-app");
//    }
//});

app.Run();

