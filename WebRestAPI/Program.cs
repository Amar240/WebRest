using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
// DO NOT FORGET TO UNCOMMENT THIS LINE
using WebRest.EF.Data;
using WebRestAPI.Code;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("WebRestOracleConnection");

// DO NOT FORGET TO UNCOMMENT THIS LINE

builder.Services.AddDbContext<WebRestOracleContext>
    (options => options.UseOracle(connectionString)
    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
    );


// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
}); 

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

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
