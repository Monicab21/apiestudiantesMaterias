using ApiEstudiante.Entidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace ApiEstudiante.AccesoDatos.Asignaciones
{
    public class DatosAsignaciones
    {
        public string InsertAsignaciones;
        public string UpdateAsignaciones;
        public string ConsulMateriaEstudiante;
        Conexion conection = new Conexion();
        private static MySqlConnection _connection { get; set; }
        public DatosAsignaciones()
        {

            InsertAsignaciones = @"INSERT INTO asignaciones
                                                        (
                                                            id_estudiante,
                                                            id_materia_profesor,
                                                            fecha_grabacion,
                                                            active
                                                        )
                                                        VALUES(@Id, @IdMateriaProfe, now(), 1);";

            UpdateAsignaciones = @"UPDATE asignaciones SET active=0 WHERE id_estudiante=@Id AND active=1";

            ConsulMateriaEstudiante = @"SELECT mp.id_materia_profesor , CONCAT(m.nombre,' - ', p.nombre) as nombre 
                                      FROM asignaciones as a INNER JOIN materia_profesor as mp ON a.id_materia_profesor = mp.id_materia_profesor 
                                      INNER JOIN materia as m ON mp.id_materia = m.id_materia INNER JOIN profesor as p ON mp.id_profesor = p.id_profesor 
                                      WHERE a.id_estudiante=@Id AND a.active=1";
        }
        public void GuardarMateris(string id, int idmatpro) {
            using MySqlConnection conn = new MySqlConnection(conection.GetDatosConexionMySQL());
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = InsertAsignaciones;
                cmd.Parameters.Add(new MySqlParameter("@Id", id));
                cmd.Parameters.Add(new MySqlParameter("@IdMateriaProfe", idmatpro));
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
        public void UpdateMaterias(string id)
        {
            using MySqlConnection conn = new MySqlConnection(conection.GetDatosConexionMySQL());
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = UpdateAsignaciones;
                cmd.Parameters.Add(new MySqlParameter("@Id", id));
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<MateriaProfesor> ConsultarMateriaEstudiante(string id)
        {
            List<MateriaProfesor> datos = new List<MateriaProfesor>();
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
                                    MateriaProfesor resp = new MateriaProfesor()
                                    {
                                        id_materia_profesor = Convert.ToInt32(reader["id_materia_profesor"].ToString()),
                                        nombre = reader["nombre"].ToString()
                                    };
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
