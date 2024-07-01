using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba
{
    public class Program
    {
        static void Main(string[] args)
        {
            DataEmpleado acceso = new DataEmpleado("localhost", "empresa");

            Empleado nuevoEmpleado = new Empleado();
            nuevoEmpleado.Id = 5;
            nuevoEmpleado.Nombre = "Victoria";
            nuevoEmpleado.Puesto = "Contabilidad";
            nuevoEmpleado.Salario = 3500F;

            acceso.ModificarEmpleado(nuevoEmpleado);
            //acceso.EliminarEmpleado(1);

            //acceso.InsertarEmpleado(nuevoEmpleado);

            //List<Empleado> miLista;
            //miLista = acceso.SeleccionarEmpleados();

            //foreach (Empleado empleado in miLista)
            //{
            //    Console.WriteLine(empleado.ToString());
            //}
            Console.ReadKey();
        }
    }
}
