using ApiEstudiante.AccesoDatos.Asignaciones;
using ApiEstudiante.AccesoDatos.DatosMateriaProfesor;
using ApiEstudiante.Entidades;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace ApiEstudiante.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Asignaciones: ControllerBase
    {
        [HttpPost(Name = "PostGuardarMaterias")]
        public ActionResult<string> Post(GuardarMateria materia)
        {
            try
            {
                DatosAsignaciones asignaciones= new DatosAsignaciones();
                asignaciones.UpdateMaterias(materia.id);
                foreach (var item in materia.materias)
                {
                    asignaciones.GuardarMateris(materia.id, item.id_materia_profesor);
                }
              
                return JsonConvert.SerializeObject("Materias Asignadas");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet(Name = "GetMateriaEstudiante")]
        public ActionResult<List<MateriaProfesor>> Get(string id)
        {
            try
            {
                DatosAsignaciones asignaciones = new DatosAsignaciones();
                return asignaciones.ConsultarMateriaEstudiante(id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}



