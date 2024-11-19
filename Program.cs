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

// Mapeia as configurações do JWT no appsettings.json para a classe JwtConfiguration
var jwtSettings = builder.Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
builder.Services.AddSingleton(jwtSettings);  // Corrigido o erro de digitação

// Configura autenticação JWT
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

// Configuração da string de conexão do MongoDB
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
	var jwtSecret = jwtSettings.Secret; // Obtemos o segredo diretamente da configuração já injetada
	return new UserService(userRepository, jwtSecret, jwtSettings.Issuer, jwtSettings.Audience);
});

// Adiciona outros repositórios e serviços
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservetionService>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceService, ServiceService>();

// Adiciona os controladores
builder.Services.AddControllers();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Adiciona autenticação
app.UseAuthorization();

app.MapControllers();

app.Run();
