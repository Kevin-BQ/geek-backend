using API.Extensiones;
using API.Middleware;
using CloudinaryDotNet;
using Data.Inicializador;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AgregarServiciosAplicacion(builder.Configuration);
builder.Services.AgregarServiciosIdentidad(builder.Configuration);

// Set your Cloudinary credentials
//=================================
Cloudinary cloudinary = new Cloudinary("cloudinary://283118925855333:swH1CWiFNjd3DbCf_5dS94m1FBE@dd2eggia1")
{
    Api = { Secure = true }
};
builder.Services.AddSingleton(cloudinary);

builder.Services.AddScoped<IdbInicializador, DbInicializador>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddlware>();
app.UseStatusCodePagesWithReExecute("/errores/{0}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod());

app.UseAuthentication();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var inicializador = services.GetRequiredService<IdbInicializador>();
        inicializador.Inicializar();
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Un Error ocurrio al ejecutar la Migracion");
    }
}

app.MapControllers();

app.Run();
