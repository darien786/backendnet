using Microsoft.EntityFrameworkCore;
using backendnet.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DataContext");
builder.Services.AddDbContext<DataContext>(options => {
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(
        policy => {
            policy.WithOrigins("http://localhost:3001", "http://localhost:8080")
            .AllowAnyHeader()
            .WithMethods("GET", "POST", "PUT", "DELETE");
        }
    );
});

//Agrega la funcionalidad de los controladores
builder.Services.AddControllers();
// Agrega la documentacion de la API
builder.Services.AddSwaggerGen();
// Construye la aplicación web 
var app = builder.Build();

// Si queremos mostrar la documentacion de la API en la raiz
if( app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Redirige a HTTPS
app.UseHttpsRedirection();
//Utiliza rutas para los endpoints de los controladores
app.UseRouting();
// Usa Cors con la policy definida anteriormente
app.UseCors();
// Establece el uso de rutas sin especificar una por default
app.MapControllers();

app.Run();