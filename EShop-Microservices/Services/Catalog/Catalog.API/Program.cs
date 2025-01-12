


using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);


//Add services to the container


var assemply = typeof(Program).Assembly;


builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assemply);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assemply);

builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{

    options.Connection(builder.Configuration.GetConnectionString("DataBase")!);
   

}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure th HTTP request pipeline

app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
