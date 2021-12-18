using AutoMapper;
using FPLCompanion.ApplicationServices.Requests.Player.Commands;
using FPLCompanion.DataService;
using FPLCompanion.DataService.Abstractions;
using FPLCompanion.DataService.Services;
using FPLCompanion.Dependencies;
using FPLCompanion.HostedServices;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton(ConfigureMapper());
builder.Services.AddMediatR(typeof(ImportPlayerDataCommand).Assembly);

builder.Services.AddScoped<IElementDataService, ElementDataService>();

builder.Services.AddControllers();
//builder.Services.AddHostedService<PremierLeagueApiWorker>();

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

IMapper ConfigureMapper()
{
    var mappingConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new ConfigureMapper());
    });

    return mappingConfig.CreateMapper();
}
