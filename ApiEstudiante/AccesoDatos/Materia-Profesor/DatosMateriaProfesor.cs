using MySql.Data.MySqlClient;
using ApiEstudiante.Entidades;
using System.Data;

namespace ApiEstudiante.AccesoDatos.DatosMateriaProfesor
{
    public class DatosMateriaProfesor
    {
        public string ConsulMateriaProfesor;
        Conexion conection = new Conexion();
        private static MySqlConnection _connection { get; set; }


        public DatosMateriaProfesor()
        {
            ConsulMateriaProfesor = @"SELECT
                                        mp.id_materia_profesor
                                        , CONCAT(m.nombre,' - ', p.nombre) as nombre
                                        FROM `materia_profesor` as mp
                                        INNER JOIN materia as m
                                        ON mp.id_materia = m.id_materia
                                        INNER JOIN profesor as p
                                        ON mp.id_profesor = p.id_profesor
                                        ORDER BY m.nombre ASC";
        }

        public List<MateriaProfesor> ConsultarMateriaProfesor()
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

                    using (var cmd = new MySqlCommand(string.Format("{0}", ConsulMateriaProfesor), _connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    MateriaProfesor resp = new MateriaProfesor()
                                    {
                                        id_materia_profesor = Convert.ToInt32(reader["id_materia_profesor"].ToString()),
                                        nombre= reader["nombre"].ToString()
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

