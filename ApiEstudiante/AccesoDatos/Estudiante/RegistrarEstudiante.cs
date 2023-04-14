using MySql.Data.MySqlClient;
using ApiEstudiante.Entidades;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Data;

namespace ApiEstudiante.AccesoDatos.Estudiante
{
    public class RegistrarEstudiante
    {
        Conexion conection = new Conexion();
        private static MySqlConnection _connection { get; set; }
        public string InsertarEstudiante;
        public string ConsulEstudiante;
        public string ConsulCorreo;
        public string RegistrosEstudiante;

        public RegistrarEstudiante() {

            InsertarEstudiante = @"INSERT INTO estudiante
                                                        (
                                                            nombre,
                                                            correo_electronico,
                                                            contrasena,
                                                            fecha_grabacion
                                                        )
                                                        VALUES(@Nombre, @Email, @Pass, now()); SELECT LAST_INSERT_ID();";
            ConsulEstudiante = @"SELECT id_estudiante FROM estudiante WHERE correo_electronico=@Email AND contrasena=@Pass";
            ConsulCorreo = @"SELECT id_estudiante FROM estudiante WHERE correo_electronico=@Email";
            RegistrosEstudiante = @"SELECT * FROM estudiante";
        }


        public int RegisEstudiante(ApiEstudiante.Entidades.Estudiante estudiante) {
            int idestudiante = 0;
            using MySqlConnection conn = new MySqlConnection(conection.GetDatosConexionMySQL());
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = InsertarEstudiante;
                cmd.Parameters.Add(new MySqlParameter("@Nombre", estudiante.nombre));
                cmd.Parameters.Add(new MySqlParameter("@Email", estudiante.email));
                cmd.Parameters.Add(new MySqlParameter("@Pass", estudiante.password));
                idestudiante = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
            return idestudiante;
        }

        public bool ConsultarEmail(ApiEstudiante.Entidades.Estudiante estudiante)
        {
            bool resp = false;
            try
            {
                using (_connection = new MySqlConnection(conection.GetDatosConexionMySQL()))
                {
                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }

                    using (var cmd = new MySqlCommand(string.Format("{0}", ConsulCorreo), _connection))
                    {
                        cmd.Parameters.Add(new MySqlParameter("@Email", estudiante.email));
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                resp = true;
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
            return resp;
        }

        public int ConsultarEstudiante(ApiEstudiante.Entidades.Estudiante estudiante) {
            int idestudiante = 0;
            try
            {
                using (_connection = new MySqlConnection(conection.GetDatosConexionMySQL()))
                {
                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }

                    using (var cmd = new MySqlCommand(string.Format("{0}", ConsulEstudiante), _connection))
                    {
                        cmd.Parameters.Add(new MySqlParameter("@Email", estudiante.email));
                        cmd.Parameters.Add(new MySqlParameter("@Pass", estudiante.password));
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idestudiante= Convert.ToInt32(reader["id_estudiante"].ToString());
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
            return idestudiante;
        }

        public List<ApiEstudiante.Entidades.Estudiante> ConsultarRegisEstudiante() {
            List<ApiEstudiante.Entidades.Estudiante> datos = new List<ApiEstudiante.Entidades.Estudiante>();
            try
            {
                using (_connection = new MySqlConnection(conection.GetDatosConexionMySQL()))
                {
                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }

                    using (var cmd = new MySqlCommand(string.Format("{0}", RegistrosEstudiante), _connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ApiEstudiante.Entidades.Estudiante resp = new ApiEstudiante.Entidades.Estudiante()
                                    {
                                        nombre = reader["nombre"].ToString(),
                                        email = reader["correo_electronico"].ToString(),
                                        password = ""

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
