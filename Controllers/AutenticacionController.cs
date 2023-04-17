using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using APICoreWeb.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Diagnostics.Eventing.Reader;

namespace APICoreWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {

        private readonly string secretkey;//Esta es una variable del tipo string que es privada
        private SecurityTokenDescriptor tokenDescriptor;

        //Metodo para traer el secretkey de appsettigs
        public AutenticacionController(IConfiguration config)
        {
            secretkey = config.GetSection("setting").GetSection("secretkey").ToString();//aqui toma el valor de la secretkey
        }

        public SigningCredentials SigningCredentials { get; private set; }

        //Metodo para autenticar al usuario
        [HttpPost]
        [Route("Validar")]
        public IActionResult Validar([FromBody] Usuario request) //FromBody es para que reciba los parametros del cuerpo
        {
            Claves claves = new Claves();

            if (request.correo == claves.correo && request.clave == claves.contraseña) {
                
                var keybytes = Encoding.ASCII.GetBytes(secretkey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.correo));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),   //Es para traer la fecha actual con la hora y agregarle 5 min
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keybytes), SecurityAlgorithms.HmacSha256)
                //SecurityAlgorithms.HmacSha256 es un cifrado de tokens brindado por la pauqteria JWT
                };


                //Lectura del token
                var tokenHandler = new JwtSecurityTokenHandler();//instancia a clase de JWT para el decodifique json 
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);  //Es para crear el token con la descripcion de arriba

                string tokencreado = tokenHandler.WriteToken(tokenConfig);//Obtener el token creado

                return StatusCode(StatusCodes.Status200OK, new { token = tokencreado });//le pasamos un json llamado token y el va obtner el token
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
            }
            


        }



    }
}
