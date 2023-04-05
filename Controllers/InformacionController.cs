using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



using Microsoft.EntityFrameworkCore; //Uso del entityframework agregado por mi
using APICoreWeb.Models; //Referencia para usar el BDcontext y es nombre del programa y la carpeta models
using Microsoft.AspNetCore.Cors; //Intanciar los 



namespace APICoreWeb.Controllers
{

    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class InformacionController : ControllerBase
    {
        public readonly CrudApiContext _dbContext;
        
        
        public InformacionController(CrudApiContext _context)
        {
            _dbContext = _context; //Le doy el valor _context a _dbContext para poder usarlo
        }


        #region API para listar
        //API para poder listar todos los productos


        [HttpGet]  //Peticion http del metodo get
        [Route("Listado")]//Ruta para la peticion del api
        public IActionResult Lista() {   //El nombre no tiene que ser el mismo que la ruta pero es para referencia 
             List<Informacion> Listado = new List<Informacion>();  //Creacion de una lista con la tabla de la base de datos para uso del modelo


            try
            {
                #region Listado sin info de trabajo
                //Listado = _dbContext.Informacions.ToList();

                //El listado es donde de trae toda la informacion de retorno
                //Haces una lista con el retorno de la informacion se loa asignas a la varieble Listado
                /* El listado de esa  manera (Aqui arriba) no incluye la informacion de la oInformacion que es la que hace referencia en la llave foranea
                hacia la otra tabla de trabajo

                Para incluir esa informacion es : */

                #endregion

                #region agregando info de Trabajo para que se vea la info de referencia 

                /*Estando asi se hacen referencias  ciclicas*/

                Listado = _dbContext.Informacions.Include(d => d.Trabajos).ToList();

                /*En program.cs se resuelven las referencias ciclicas por las que da
                error en la region -> Resolviendo referencias ciclicas*/

                #endregion

                //retorno de los valores con el mensaje
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = Listado });//retorna un codigo de servidor junto con el Listado
            }
            catch (Exception ex) {

                //retorno del error en mensaje
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = Listado });//retorna codigo de servidor juntos con el error
            }
        }

        #endregion


        #region API Obtener

        [HttpGet]  
        [Route("Obtener/{idInformacion}:int")]  //con las llaves {} pedimos los parametros para la funcion, debe de hacer referencia con el parametro
        public IActionResult Obtener(int idInformacion)
        {   
            Informacion oInformacion = _dbContext.Informacions.Find(idInformacion); 
            /*Se quita la lista porque solo sera una respuesta no varias como en el anterior
            el .Fin es para realizar una busqueda en el idInformcion*/

             if (oInformacion == null) { 
                return BadRequest("Informacion no encintrada");
            }

            try
            {

                oInformacion = _dbContext.Informacions.Include(t => t.Trabajos).Where(p => p.Id == idInformacion).FirstOrDefault();
                /*p => p. es para dar un alias, -- Id se compara con el campo lo que obtuvo idInfromacion*/

                //retorno de los valores con el mensaje
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oInformacion });//retorna un codigo de servidor junto con el Listado
            }
            catch (Exception ex)
            {

                //retorno del error en mensaje
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = oInformacion });//retorna codigo de servidor juntos con el error
            }
        }


        #endregion


        #region Api Guardar datos
        [HttpPost]
        [Route("Escribir")]

        public IActionResult Escribir ([FromBody] Informacion objeto)
        {
            try
            {
                _dbContext.Informacions.Add(objeto);//Se agrega el objeto dentro del model de informacion
                //El objeto son los parametro recibidos en la funcion que vienen desde el http

                _dbContext.SaveChanges(); //Con esto se esta guardando la informacion en la tabla
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok"});//retorno de mensaje de guardado
            }
            catch (Exception ex){
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});
            }

        }

        #endregion


        #region API Editar
        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar ([FromBody] Informacion objeto)
        {
            Informacion oInformacion = _dbContext.Informacions.Find(objeto.Id);

            if (oInformacion == null)
            {
                return BadRequest("Informacion no encontrada");
            }
            try
            {

                //Estas son para mantener los valores que ya tiene y solo se modifique el deseado

                //Tabla Informacion
                oInformacion.Nombre = objeto.Nombre is null ? oInformacion.Nombre : objeto.Nombre;
                oInformacion.Apellido = objeto.Apellido is null ? oInformacion.Apellido : objeto.Apellido;
                


                _dbContext.Informacions.Update(oInformacion);//Se agrega el objeto dentro del model de informacion
                //El objeto son los parametro recibidos en la funcion que vienen desde el http

                _dbContext.SaveChanges(); //Con esto se esta guardando la informacion en la tabla
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });//retorno de mensaje de guardado
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }


        #endregion


        #region API borrar


        [HttpDelete]
        [Route("Eliminar/{idProducto:int}")]

        public IActionResult Eliminar(int idProducto)
        {
            Informacion oInformacion = _dbContext.Informacions.Find(idProducto);

            if (oInformacion == null)
            {
                return BadRequest("Informacion no encontrada");
            }
            try
            {
                _dbContext.Informacions.Remove(oInformacion);//Se agrega el objeto dentro del model de informacion
                //El objeto son los parametro recibidos en la funcion que vienen desde el http

                _dbContext.SaveChanges(); //Con esto se esta guardando la informacion en la tabla
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });//retorno de mensaje de guardado
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }


        }



        #endregion

    }
}
