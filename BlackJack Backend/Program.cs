using BlackJack_Backend.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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



var logger = app.Services.GetRequiredService<ILogger<Program>>();

//logger.LogInformation($"Kortleken har {cards.Count}");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
