using backend_gestorinv.Context;
using backend_gestorinv.Services;
using backend_gestorinv.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Inyección de dependencias
builder.Services.AddScoped<JwtServices>();
builder.Services.AddTransient<IRolService, RolService>();
builder.Services.AddTransient<ICategoriaService, CategoriaService>();
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IProviderService, ProviderService>();
builder.Services.AddTransient<IInventoryService, InventoryService>();
builder.Services.AddTransient<IMovementService, MovementService>();

// Configuración de autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
        options.RequireHttpsMetadata = false; // Permite peticiones HTTP en desarrollo
        options.SaveToken = true;
    });

builder.Services.AddAuthorization();

var myCorsPolicy = "_myCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myCorsPolicy,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Reemplaza con la URL de tu frontend
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials(); // Necesario si usas autenticación con cookies o tokens en headers
        });
});

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Muestra errores detallados en desarrollo
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("_myCorsPolicy"); // Se usa la política correctamente
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
