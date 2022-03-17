using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpLogging;
using NetTopologySuite.AspNetCore.Extensions;
using NetTopologySuite.AspNetCore.Extensions.Swagger;

using GeoMarker.Infrastucture.Filters;
using GeoMarker.Infrastucture.EFCore;
using GeoMarker.Infrastucture.Exceptions.Demo_Jwt;
using GeoMarker.Infrastucture.Configuration;
using GeoMarker.Services;
using Microsoft.OpenApi.Models;

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
    .AddSingleton<IJwtService, JwtService>()
    .AddScoped<ITenantInfo, TenantInfo>()
    .AddSingleton(typeof(GlobleUtils));

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

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            System.Array.Empty<string>()
        }
    });

    options.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GeoMarker.xml"));
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

    app.MapPost("/token", ([FromBody] LoginDto dto, [FromServices] IJwtService jwtService) =>
    {
        var token = jwtService.GenerateToken(new[] { new Claim(ClaimTypes.NameIdentifier, dto.UserName) });
        return Results.Ok(token);
    });
}

app.UseHttpLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


internal record LoginDto(string UserName, string Password);