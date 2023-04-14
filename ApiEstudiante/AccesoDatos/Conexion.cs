using ApiEstudiante.Entidades.Transversal;

namespace ApiEstudiante.AccesoDatos
{
    public class Conexion
    {

        public string GetDatosConexionMySQL()
        {
            Datos datos = new Entidades.Transversal.Datos();
            datos.servidor = "127.0.0.1";
            datos.baseDatos = "clases-estudiantes";
            datos.user = "root";
            datos.pass = "";

            return String.Format("server={0}; uid={1}; pwd={2}; database={3}", datos.servidor, datos.user, datos.pass, datos.baseDatos);
        }
    }

}
