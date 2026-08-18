using Oracle.ManagedDataAccess.Client;

OracleConfiguration.WalletLocation = @"C:\OracleWallet";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseDefaultFiles();

app.UseStaticFiles();

app.MapGet("/api/probar-oracle", () =>
{
    // Aquí invocamos la clase que creamos en el paso anterior
    webportfolio.ConexionBaseDatos miConexion = new webportfolio.ConexionBaseDatos();
    string resultado = miConexion.ProbarConexion();
    bool exito = !resultado.StartsWith("Error");
    var respuesta = new
    {
        success = exito,
        message = resultado,
        timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
    };
    // Devolvemos el texto al navegador
    return Results.Json(respuesta);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
