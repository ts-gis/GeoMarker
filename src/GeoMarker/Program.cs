using NetTopologySuite.AspNetCore.Extensions;
using NetTopologySuite.AspNetCore.Extensions.Swagger;

using GeoMarker.Filters;
using GeoMarker.Services;
using GeoMarker.EFCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(optionBuilder =>
{
    optionBuilder
        .UseNpgsql(builder.Configuration.GetConnectionString("Default"), options => options.UseNetTopologySuite())
        .UseLowerCaseNamingConvention();
});

builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
    options.Filters.Add<TenantValidateFilter>();
})
.AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.Converters.AddWktJsonConverter();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SchemaFilter<WKTSchemaFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
