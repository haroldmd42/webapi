using webapi;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlServer<TareasContext>(
    builder.Configuration.GetConnectionString("DefaultConnection")
);

// Inyección de dependencias
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ITareaService, TareaService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAddHistoryService, AddHistoryService>();
// 🔐 CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:5173",
                "https://haroldmd42.github.io"
                // Puedes agregar más orígenes como Vercel aquí
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var app = builder.Build();

// 🔍 Swagger (mostrar siempre)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
