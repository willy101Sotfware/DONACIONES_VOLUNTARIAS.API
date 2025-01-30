using Microsoft.EntityFrameworkCore;
using DONACIONES_VOLUNTARIAS.API.Persistence.Contexts;
using Microsoft.OpenApi.Models;
using MediatR;
using DONACIONES_VOLUNTARIAS.API.Interface;
using DONACIONES_VOLUNTARIAS.API.Services.Queries.DonationQuerys;
using DONACIONES_VOLUNTARIAS.API.Services.Queries.DonorQuerys;
using DONACIONES_VOLUNTARIAS.API.Services.Queries.EventQuerys;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DONACIONES_VOLUNTARIAS.API", Version = "v1" });
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
builder.Services.AddMediatR(typeof(GetDonorByIdQuery).Assembly);
builder.Services.AddMediatR(typeof(GetEventByIdQuery).Assembly);

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DONACIONES_VOLUNTARIAS.API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Usar la política de CORS
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
