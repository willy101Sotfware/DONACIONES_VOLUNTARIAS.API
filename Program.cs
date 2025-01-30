using Microsoft.EntityFrameworkCore;
using DONACIONES_VOLUNTARIAS.API.Persistence.Contexts;
using Microsoft.OpenApi.Models;
using MediatR;
using DONACIONES_VOLUNTARIAS.API.Interface;
using DONACIONES_VOLUNTARIAS.API.Services.Queries.DonationQuerys;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using DONACIONES_VOLUNTARIAS.API.Services.Autentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DONACIONES_VOLUNTARIAS.API", Version = "v1" });

    // Definir el esquema de seguridad para JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor, ingrese el token JWT en este formato: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Agregar requerimientos de seguridad globales para todas las rutas
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// Configurar el contexto de la base de datos
builder.Services.AddDbContext<GestionVoluntariadoDonacionesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar la interfaz del contexto
builder.Services.AddScoped<IGestionVoluntariadoDonacionesContext>(provider => provider.GetService<GestionVoluntariadoDonacionesContext>());

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Configurar MediatR
builder.Services.AddMediatR(typeof(GetDonationByIdQuery).Assembly);
//builder.Services.AddMediatR(typeof(GetDonorByIdQuery).Assembly);
//builder.Services.AddMediatR(typeof(GetEventByIdQuery).Assembly);

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


// Configurar logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Configurar JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
// Registrar AuthService
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DONACIONES_VOLUNTARIAS.API v1"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Usar la política de CORS
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
