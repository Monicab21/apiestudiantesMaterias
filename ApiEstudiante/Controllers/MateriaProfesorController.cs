using ApiEstudiante.AccesoDatos.DatosMateriaProfesor;
using ApiEstudiante.AccesoDatos.Estudiante;
using ApiEstudiante.Entidades;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Relational;
using static Mysqlx.Expect.Open.Types.Condition.Types;

namespace ApiEstudiante.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MateriaProfesorController : ControllerBase
    {
        [HttpGet(Name = "GetMateriaProfesor")]
        public ActionResult<List<MateriaProfesor>> Get()
        {
            try
            {
                DatosMateriaProfesor matpro = new DatosMateriaProfesor();
                return matpro.ConsultarMateriaProfesor();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    };
}
