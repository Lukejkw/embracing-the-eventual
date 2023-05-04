using Cart;
using MassTransit;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connect to MongoDB
string connectionString = builder.Configuration.GetConnectionString("MongoDb");
var mongo = new MongoClient(connectionString);
builder.Services.AddSingleton(mongo);

// Setup MassTransit
builder.Services.AddMassTransit(options =>
{
    options.SetKebabCaseEndpointNameFormatter();

    // Automatically register sagas and consumers
    options.AddSagasFromNamespaceContaining<CartAssemblyReference>();
    options.SetMongoDbSagaRepositoryProvider(mongoRepoOptions =>
    {
        mongoRepoOptions.ClientFactory(s => s.GetRequiredService<MongoClient>());
        mongoRepoOptions.DatabaseFactory(s => s.GetRequiredService<MongoClient>().GetDatabase("cart"));
    });

    options.AddConsumersFromNamespaceContaining<CartAssemblyReference>();

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

builder.Services.AddCors(options => options.AddDefaultPolicy(
    // Warning! Don't do this in production
    policy => policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();