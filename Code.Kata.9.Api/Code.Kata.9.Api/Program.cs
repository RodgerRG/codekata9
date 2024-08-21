using System.Reflection;
using Code.Kata._9.Data;

var builder = WebApplication.CreateBuilder(args);

// Add mediatR
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(Assembly.Load("Code.Kata.9.AppServices"));
});

// Add EF Core Provider
builder.Services.AddKataNineDatabase();

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
app.MapControllers()
    .WithOpenApi();

app.Run();