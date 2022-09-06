using Api.Epsas.DataContext;
using Api.Epsas.Interfaces;
using Api.Epsas.Interfaces.common;
using Api.Epsas.Services.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;
    var env = builder.Environment;

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(op =>
    {
        op.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateActor = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"])),
        };
    });

    services.AddDbContext<AplicationDbContext>(options =>
                                                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
                                                        .UseSnakeCaseNamingConvention());


    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
       // x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;        
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    services.AddHttpContextAccessor();
   
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(op =>
    {
        op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Name = "Autorization",
            Description = "Token Autenticacion Api Biogenesis",
            Type = SecuritySchemeType.Http,
        });
        op.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type=ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        });
    });

    builder.Host.UseSystemd();

    // configuraciones de inyeccion de servicios comunes
    services.AddScoped<ICurrentUserService, CurrentUserService>();
    services.AddScoped<ILogErrors, LogErrors>();
    services.AddScoped<ITokenService, TokenService>();      
    services.AddTransient<IDateTimeService, DateTimeService>();


    //TODO: 004 - Crear la interfaz en la carpeta Interfaces
    //TODO: 005 - Crear el servicio de implementacion enn la carpeta Services
    //TODO: 006 - Registrar la iyeccion de dependencias ejemplo aqui ..
    services.AddScoped<IUsuariosServices, UserService>();



}


var app = builder.Build();
{

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    app.UseAuthentication();
    app.UseAuthorization();

    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.MapControllers();
}

app.Run();
