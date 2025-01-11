

var builder = WebApplication.CreateBuilder(args);


//Add services to the container

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(options =>
{

    options.Connection(builder.Configuration.GetConnectionString("DataBase")!);
   

}).UseLightweightSessions();

var app = builder.Build();

// Configure th HTTP request pipeline

app.MapCarter();

app.Run();
