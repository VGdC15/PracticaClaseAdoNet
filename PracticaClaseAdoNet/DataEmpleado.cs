using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
//using: administrador de contexto

namespace Clases
{
    public class DataEmpleado
    {
        //es un atributo para conectar a la base de datos que queremos 
        string connectionString;

        //constructor que recibe cadena de colección
        public DataEmpleado(string server, string db)
        {
            this.connectionString = $"Server={server}; Database={db}; User ID=root; Password=; SslMode=none;";
        }

        //aca establecemos una instancia de la conexion con la base de datos 
        //la creación de conexiones se hacen dentro de la misma clase, no por fuera
        private MySqlConnection ObtenerConexion() //el metodo esta privado porq no voy a establecer conexiones a la base de datos por fuera de esta clase
        {
            return new MySqlConnection(connectionString);
        } 

        public List<Empleado> SeleccionarEmpleados()
        {
            List<Empleado> listaEmpleados = new List<Empleado>();
            //si creo la conexion dentro de este using, despues no hace falta cerrar la conexion
            //si voy a tener q abrirla, pero no cerrarla

            using (var conexion = ObtenerConexion())//creo conexión y la abro
            {
                //CRUD: Create, Read, Update, Delete
                conexion.Open();
                string query = "SELECT * FROM empleados";
                MySqlCommand cmd = new MySqlCommand(query, conexion);//le tengo q pasar una instancia de una conexion activa, comando que quiero ejecutar y la conexión activa
                MySqlDataReader reader = cmd.ExecuteReader();//lee lo q le pedimos q lea, representa un puntero en memoria, y es de sólo avance y solo lectura. Va leyendo y avanzando la lista o coleccion de la consulta.
                while (reader.Read())//tiene que repetirse por cada lectura que tiene del objeto, por eso lo recorro
                                     //el mismo reader es el q se encarga de avanzar en cada objeto
                {
                    var empleado = new Empleado
                    {
                        Id = reader.GetInt32("id"),
                        Nombre = reader.GetString("nombre"),
                        Puesto = reader.GetString("puesto"),
                        Salario = reader.GetFloat("salario")
                    };

                    listaEmpleados.Add(empleado);

                }
            }

            return listaEmpleados;
        }

        public void InsertarEmpleado(Empleado nuevoEmpleado)
        {
            using(var conexion = ObtenerConexion())
            {
                conexion.Open();
                //$"VALUES ({unEmpleado.Nombre},{unEmpleado.Puesto},{unEmpleado.Salario})"; // no es una buena practica
                string query = "INSERT INTO empleados(nombre,puesto,salario)" +
                    $"VALUES(@nombre,@puesto,@salario)";//el arroba es convención
                MySqlCommand comando = new MySqlCommand(query, conexion);
                //aca se configuran los parametros, todavia no se ejecutan
                comando.Parameters.AddWithValue("@nombre", nuevoEmpleado.Nombre);  //hay q decirle cuales son los parametros q voy a encapsular
                comando.Parameters.AddWithValue("@puesto", nuevoEmpleado.Puesto);
                comando.Parameters.AddWithValue("@salario", nuevoEmpleado.Salario);

                comando.ExecuteNonQuery();//cuando quiero ejecutar una consulta de insercion, de eliminacion o actualizacion
            }
        }

        public void ModificarEmpleado(Empleado nuevoEmpleado)
        {
            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                string query = "UPDATE empleados SET nombre = @nombre, puesto = @puesto, salario = @salario " +
                    $"WHERE id = @id";
                MySqlCommand comando = new MySqlCommand(query, conexion);
                //aca se configuran los parametros, todavia no se ejecutan
                comando.Parameters.AddWithValue("@id", nuevoEmpleado.Id); //hay q decirle cuales son los parametros q voy a encapsular
                comando.Parameters.AddWithValue("@nombre", nuevoEmpleado.Nombre);
                comando.Parameters.AddWithValue("@puesto", nuevoEmpleado.Puesto);
                comando.Parameters.AddWithValue("@salario", nuevoEmpleado.Salario);

                comando.ExecuteNonQuery();//cuando quiero ejecutar una consulta de insercion, de eliminacion o actualizacion
            }
        }

        public void EliminarEmpleado(int id)//después puedo sobrecargarlo y eliminar de acuerdo a otras condiciones
        {
            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                string query = "DELETE FROM empleados WHERE id = @id";//borra todo un registro
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id", id);

                comando.ExecuteNonQuery();//
            }
        }
    }
}
