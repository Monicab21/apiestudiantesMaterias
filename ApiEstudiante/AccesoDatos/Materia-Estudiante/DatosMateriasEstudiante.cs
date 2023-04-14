using ApiEstudiante.Entidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace ApiEstudiante.AccesoDatos.Materia_Estudiante
{
    public class DatosMateriasEstudiante
    {
        public string ConsulMateriaEstudiante;
        Conexion conection = new Conexion();
        private static MySqlConnection _connection { get; set; }

        public DatosMateriasEstudiante()
        {
            ConsulMateriaEstudiante = "DROP TABLE IF EXISTS ids;CREATE TEMPORARY TABLE ids SELECT a.id_materia_profesor FROM asignaciones as a  WHERE a.id_estudiante=@Id AND a.active=1;" +
                "SELECT CONCAT(e.nombre,' - (', m.nombre,')') as nombre FROM asignaciones as a INNER JOIN materia_profesor as mp ON a.id_materia_profesor = mp.id_materia_profesor" +
                " INNER JOIN materia as m ON mp.id_materia = m.id_materia INNER JOIN profesor as p ON mp.id_profesor = p.id_profesor " +
                " INNER JOIN estudiante as e ON a.id_estudiante = e.id_estudiante WHERE a.id_materia_profesor IN (SELECT * FROM ids) AND a.active=1 AND a.id_estudiante<>@Id;";
        }


        public List<string> ConsultarMateriasEstudiante(string id)
        {
            List<string> datos = new List<string>();
            try
            {
                using (_connection = new MySqlConnection(conection.GetDatosConexionMySQL()))
                {
                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }

                    using (var cmd = new MySqlCommand(string.Format("{0}", ConsulMateriaEstudiante), _connection))
                    {
                        cmd.Parameters.Add(new MySqlParameter("@Id", id));
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string resp = reader["nombre"].ToString();
                                    datos.Add(resp);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                _connection.Close();
            }
            return datos;
        }
    }
}
