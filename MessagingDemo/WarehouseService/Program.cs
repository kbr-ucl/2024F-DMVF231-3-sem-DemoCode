using MassTransit;
using WarehouseService.BusHandlers;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq();
    x.AddConsumer<ItemCreatedEventConsumer>();
});

builder.Services.AddScoped<ItemCreatedEventConsumer>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();