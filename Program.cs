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

// Mapeia as configura��es do JWT no appsettings.json para a classe JwtConfiguration
var jwtSettings = builder.Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
builder.Services.AddSingleton(jwtSettings);  // Corrigido o erro de digita��o

// Configura autentica��o JWT
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
		ValidIssuer = jwtSettings.Issuer,
		ValidAudience = jwtSettings.Audience,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
	};
});

builder.Services.AddAuthorization();

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
	var jwtSecret = jwtSettings.Secret; // Obtemos o segredo diretamente da configura��o j� injetada
	return new UserService(userRepository, jwtSecret, jwtSettings.Issuer, jwtSettings.Audience);
});

// Adiciona outros reposit�rios e servi�os
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservetionService>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceService, ServiceService>();

// Adiciona os controladores
builder.Services.AddControllers();

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
