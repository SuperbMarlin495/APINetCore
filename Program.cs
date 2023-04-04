using Microsoft.EntityFrameworkCore;
using APICoreWeb.Models;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



#region Configuracion BDContext
builder.Services.AddDbContext<CrudApiContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));
//Lista el constructor para los controladores
#endregion



#region Resolviendo referencias ciclicas

builder.Services.AddControllers().AddJsonOptions(opt =>
    {
        //Estamos obligando con la serializacion de json a que ignore las referencias ciclicas
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });



#endregion



#region Activando los CORS
//Son para que cualquiera pueda usar nuestra API

var misRuleCors = "ReglasCors";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: misRuleCors, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        //Damos permiso a cualquier origen, cualquier cabezara y que use cualquier metodo
    });
});


#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(misRuleCors);//Aqui los activamos y en el controller le decimos donde aplicarlo

app.UseAuthorization();

app.MapControllers();

app.Run();