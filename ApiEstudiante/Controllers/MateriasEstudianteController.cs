using ApiEstudiante.AccesoDatos.Materia_Estudiante;
using ApiEstudiante.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace ApiEstudiante.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MateriasEstudianteController: ControllerBase
    {
        [HttpGet(Name = "GetMateriaEstudiantes")]
        public ActionResult<List<string>> Get(string id)
        {
            try
            {
                DatosMateriasEstudiante dat = new DatosMateriasEstudiante();
                return dat.ConsultarMateriasEstudiante(id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
