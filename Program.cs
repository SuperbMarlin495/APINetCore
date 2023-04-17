using Microsoft.EntityFrameworkCore;
using APICoreWeb.Models;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Authentication.JwtBearer;//Es para el uso de los paquetes instalados
using Microsoft.IdentityModel.Tokens;//Uso de lso tokens de JWT
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region JWT

//Obtencion de la secretkey que esta en appsettings
builder.Configuration.AddJsonFile("appsettings.json"); //es para agregar el archivo json de appsetting

var secretkey = builder.Configuration.GetSection("setting").GetSection("secretkey").ToString();//Se le da el valor a secretkey que esta en appsettings
var keyByte = Encoding.UTF8.GetBytes(secretkey);//Estamos conviertiendo la llave en bytes

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //Darle el valor del esquema y sea por default
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true, //Validacion de que el usuario tenga el token
        IssuerSigningKey = new SymmetricSecurityKey(keyByte), //le pasamos el parametro de keybat que dijimos arriba de la clase 
        ValidateIssuer = false, //la validacion de usuario
        ValidateAudience = false
    };

});



#endregion



#region Add services to the container

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion



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




#region Referencias
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
   
//}

//Primeras referencia
app.UseSwagger();
app.UseSwaggerUI();



//Segundas referencias
app.UseCors(misRuleCors);//Aqui los activamos y en el controller le decimos donde aplicarlo

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


#endregion