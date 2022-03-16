using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpLogging;
using NetTopologySuite.AspNetCore.Extensions;
using NetTopologySuite.AspNetCore.Extensions.Swagger;

using GeoMarker.Infrastucture.Filters;
using GeoMarker.Infrastucture.EFCore;
using GeoMarker.Infrastucture.Exceptions.Demo_Jwt;
using GeoMarker.Infrastucture.Configuration;
using GeoMarker.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(optionBuilder =>
{
    optionBuilder
        .UseNpgsql(builder.Configuration.GetConnectionString("Default"), options => options.UseNetTopologySuite())
        .UseLowerCaseNamingConvention();
});

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(JwtConfig.SectionName));

builder.Services
    .AddHttpContextAccessor()
    .AddJwtAuthentication(builder.Configuration)
    .AddSingleton<IJwtService, JwtService>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddScoped<ITenantInfo, TenantInfoForDev>();
}
else
{
    builder.Services.AddScoped<ITenantInfo, TenantInfo>();
}

builder.Services.AddHttpLogging(logging =>
{
    // Customize HTTP logging here.
    logging.LoggingFields = HttpLoggingFields.All;
    //logging.RequestHeaders.Add("My-Request-Header");
    //logging.ResponseHeaders.Add("My-Response-Header");
    //logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
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

using (var scope = app.Services.CreateScope())
{
    using var dbcontext = scope.ServiceProvider.GetService<AppDbContext>();
    dbcontext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
