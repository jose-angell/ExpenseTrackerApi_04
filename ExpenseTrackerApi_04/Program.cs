using ExpenseTrackerApi_04.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// A) Registrar tu manejador global de excepciones
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
// B) Registrar los servicios para la generación estandarizada de ProblemDetails
builder.Services.AddProblemDetails();

var app = builder.Build();

// ¡IMPORTANTE!: app.UseExceptionHandler() debe ir al inicio del pipeline.
// Esto permite que envuelva y capture las excepciones de todos los middlewares
// que se ejecuten después de él (como la autenticación, controladores, etc.)
app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
