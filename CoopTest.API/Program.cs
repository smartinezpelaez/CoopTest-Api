using CoopTest.API;
using CoopTest.BLL.DTOs;
using CoopTest.BLL.Repository;
using CoopTest.BLL.Repository.Implements;
using CoopTest.BLL.Services;
using CoopTest.BLL.Services.Implements;
using CoopTest.DAL.Models;
using MongoDB.Driver;


var builder = WebApplication.CreateBuilder(args);

// Obtener la cadena de conexión desde la configuración
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = builder.Configuration.GetSection("MongoDB:ConnectionString").Value;
    return new MongoClient(connectionString);
});

// Add services to the container.
builder.Services.AddSingleton<CoopTestContext>();

//Pruebas de conexion
var client = new MongoClient("mongodb://localhost:27017");
var connectionTest = new MongoDBConnectionTest(client);
connectionTest.CheckConnection();

// Test a la base de datos de Mongo
builder.Services.AddScoped<IGenericRepository<Fondo>>(provider =>
    new GenericRepository<Fondo>((CoopTestContext)provider.GetRequiredService<IMongoDatabase>(), "fondos"));



// Add AutoMapper Injection
builder.Services.AddAutoMapper(typeof(MapperConfig));

// Repositorios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IFondoRepository, FondoRepository>();
builder.Services.AddScoped<ITransaccionRepository, TransaccionRepository>();

// Servicios
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ITransaccionService, TransaccionService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Verificar la conexión a MongoDB al iniciar la aplicación
var mongoClient = app.Services.GetRequiredService<IMongoClient>();
var mongoDbTest = new MongoDBConnectionTest(mongoClient);
mongoDbTest.CheckConnection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
