using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    //id autoincremental
    //nombre varchar 25
    //puesto varchar 25
    //salario float
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Puesto { get; set; }
        public float Salario { get; set; }

        public override string ToString()
        {
            return $"{Id}--{Nombre}--{Puesto}--{Salario}"; 
        }
    }
}
