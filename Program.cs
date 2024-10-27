using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Reservas.Configurations;
using Reservas.Data;
using Reservas.Repository;
using Reservas.Repository.Contract;
using Reservas.Services;
using Reservas.Services.Contract;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adiciona a configura��o a partir de appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// Configura��o da string de conex�o do MongoDB
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDbConnection");
var mongoDataBaseName = builder.Configuration["MongoDbSettings:DatabaseName"];

// Registra o cliente do MongoDB
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(mongoConnectionString));
builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return new MongoDbContext(client, mongoDataBaseName);
});

// Registra IUserRepository e IUserService
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>(provider =>
{
    var userRepository = provider.GetRequiredService<IUserRepository>();
    var jwtSecret = builder.Configuration["JwtSettings:Secret"];
    return new UserService(userRepository, jwtSecret);
});

// Adiciona outros reposit�rios e servi�os
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservetionService>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceService, ServiceService>();

// Adiciona os controladores
builder.Services.AddControllers();

// Configura��o de JWT
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("Jwt"));

// Adiciona autentica��o JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtConfig = builder.Configuration.GetSection("Jwt").Get<JwtConfiguration>();
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig.Issuer,
        ValidAudience = jwtConfig.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret))
    };
});

// Configura��o do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura��o do pipeline de requisi��es HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Adiciona autentica��o
app.UseAuthorization();

app.MapControllers();
app.Run();
