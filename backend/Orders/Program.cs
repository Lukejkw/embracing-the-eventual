using MassTransit;
using MongoDB.Driver;
using Orders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connect to MongoDB
string connectionString = builder.Configuration.GetConnectionString("MongoDb");
var mongoClient = new MongoClient(connectionString);
builder.Services.AddSingleton<IMongoClient>(mongoClient);

builder.Services.AddMassTransit(options =>
{
    options.SetKebabCaseEndpointNameFormatter();
    
    // Automatically register sagas and consumers
    options.AddSagasFromNamespaceContaining<OrderAssemblyReference>();
    options.AddConsumersFromNamespaceContaining<OrderAssemblyReference>();
    
    // This will use default credentials for everything
    // You probably want something a bit more production grade
    options.UsingRabbitMq((context, config) =>
    {
        var service = context.GetRequiredService<ILogger<IServiceCollection>>();

        service.LogInformation(
            "Host {HostName} connecting to Rabbit messaging", Environment.MachineName);

        config.ConfigureEndpoints(context);
    });
});
builder.Services.Configure<MassTransitHostOptions>(options =>
{
    options.WaitUntilStarted = true;
});

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