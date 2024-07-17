using AutoMapper;
using FPLCompanion.ApplicationServices.Requests.General.Commands;
using FPLCompanion.DataService;
using FPLCompanion.DataService.Abstractions;
using FPLCompanion.DataService.Services;
using FPLCompanion.Dependencies;
using FPLCompanion.HostedServices;
using Microsoft.EntityFrameworkCore;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
var mongoDBSettings = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMongoDB(mongoDBSettings?.ConnectionString ?? "", mongoDBSettings?.DatabaseName ?? ""));

builder.Services.AddSingleton(ConfigureMapper());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ImportFplData).Assembly));
builder.Services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
builder.Services.AddScoped<IElementRepository, ElementRepository>();
builder.Services.AddScoped<IElementTypeRepository, ElementTypeRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IFixtureRepository, FixtureRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IElementDetailRepository, ElementDetailRepository>();
builder.Services.AddScoped<IDreamTeamRepository, DreamTeamRepository>();
builder.Services.AddScoped<IEventElementRepository, EventElementRepository>();
builder.Services.AddScoped<ImportFplData>();
builder.Services.AddSingleton<SchedulerConfigContext>();
builder.Services.AddControllers();

builder.Services.AddQuartz();
builder.Services.AddQuartzHostedService(
    q => q.WaitForJobsToComplete = true
);
builder.Services.AddHostedService<SchedulerWorker>();

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

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

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
