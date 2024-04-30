var builder = WebApplication.CreateBuilder(args);

// If the app is started from Dapr, use the Dapr sidecar's HTTP port.
var apiHttpPort = Environment.GetEnvironmentVariable("APP_PORT");
if (!string.IsNullOrEmpty(apiHttpPort))
{
    builder.WebHost.UseUrls($"http://localhost:{apiHttpPort.Trim()}");
}

// Add services to the container.
builder.Services.AddDaprClient();

builder.Services.AddControllers();//.AddDapr(); //You can use builder.Services.AddControllers().AddDapr() or builder.Services.AddDaprClient() as your please.
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

app.UseAuthorization();

// Use Cloud Events
app.UseCloudEvents();

app.MapControllers();

// app.MapSubscribeHandler(); # This line is not needed in "Declarative" PubSub.

app.Run();
