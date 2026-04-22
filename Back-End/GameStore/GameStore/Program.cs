using GameStore.Application.Services;
using GameStore.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Esta linha ignora o ciclo e permite que o JSON seja gerado
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

        // Opcional: deixa o JSON "bonitinho" no Swagger
        options.JsonSerializerOptions.WriteIndented = true;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<CarrinhoService>();
builder.Services.AddScoped<PedidoService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});




builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GameStore API",
        Version = "v1"
    });

    // 🔐 Configuração do JWT no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Digite: Bearer {seu token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});






var jwtSettings = builder.Configuration.GetSection("Jwt");

var key = builder.Configuration["Jwt:Key"]
    ?? throw new Exception("JWT Key não configurada");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"]!,
        ValidAudience = jwtSettings["Audience"]!,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key)
        )
    };
});





var app = builder.Build();

app.UseMiddleware<GameStore.Helpers.Middleware.ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
