using MongoDB.Driver;
using Users.Application.Repositories;
using Users.Demo.API.Extensions;

var builder = WebApplication.CreateBuilder(args);


// Add configuration from appSettings.json
builder.Configuration.AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>();

// Configure MongoDB
var connectionString = builder.Configuration.GetSection("MongoDB:ConnectionString").Value;
var databaseName = builder.Configuration.GetSection("MongoDB:DatabaseName").Value;
var client = new MongoClient(connectionString);
var database = client.GetDatabase(databaseName);

// Add services
builder.Services.AddSingleton(database);
builder.Services.AddScoped<IUserRepository, UserRepository>();



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.RegisterUsersEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);
}