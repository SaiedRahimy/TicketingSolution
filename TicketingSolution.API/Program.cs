using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TicketingSolution.Core;
using TicketingSolution.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = "DataSource=:memory:";
var connection =new SqliteConnection(connectionString);

connection.Open();

builder.Services.AddDbContext<TicketingSolutionDbContext>(opt => opt.UseSqlite(connection));

builder.Services.AddScoped<ITicketBookingRequestHandler, TicketBookingRequestHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
