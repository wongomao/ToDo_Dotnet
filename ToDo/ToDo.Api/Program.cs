using ToDo.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IToDoRepository, ToDoRepository>();
// Console.WriteLine("Connection String: " + builder.Configuration.GetConnectionString("ToDoDatabase"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
