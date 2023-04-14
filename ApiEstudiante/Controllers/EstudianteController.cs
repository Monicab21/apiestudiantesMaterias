using ApiEstudiante.AccesoDatos.DatosMateriaProfesor;
using ApiEstudiante.AccesoDatos.Estudiante;
using ApiEstudiante.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiEstudiante.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudianteController : ControllerBase
    {
        [HttpPost(Name = "PostEstudiante")]
        public ActionResult<int> Post(Estudiante estudiante)
        {
            int idestudiante = 0;
            try
            {
                RegistrarEstudiante regestudiante = new RegistrarEstudiante();
                if (!regestudiante.ConsultarEmail(estudiante))
                {
                
                   idestudiante = regestudiante.RegisEstudiante(estudiante);
                    
                }
                else {
                    idestudiante = regestudiante.ConsultarEstudiante(estudiante);
                    if (idestudiante == 0)
                    {
                        throw new ApplicationException("Credenciales Invalidas");
                    }
                     
                }

            }
            catch (ApplicationException ex)
            {

                return BadRequest(ex.Message);
            }
            return idestudiante;
        }

        [HttpGet(Name = "GetRegistroEstudiantes")]
        public ActionResult<List<ApiEstudiante.Entidades.Estudiante>> Get()
        {
            try
            {
                RegistrarEstudiante regestudiante = new RegistrarEstudiante();
                return regestudiante.ConsultarRegisEstudiante();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    };


}
