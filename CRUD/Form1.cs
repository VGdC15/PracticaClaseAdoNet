using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clases;

namespace CRUD
{
    public partial class Form1 : Form
    {
        List<Empleado> listaEmpleados;
        DataEmpleado data;
        public Form1()
        {
            InitializeComponent();
            data = new DataEmpleado("localhost", "empresa");
            //hay que traer la lista de la base de datos
            listaEmpleados = data.SeleccionarEmpleados();
            listBox1.DataSource = listaEmpleados;
            //datasource es una propiedad que enlaza los datos(binding) entre un origen de datos que es la lista de datos en este caso.
            //mapea en el listbox la lista. No hace falta recorrerla.
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_Agregar_Click(object sender, EventArgs e)
        {
            txt_Id.Text = "";
            string nombre = txt_Nombre.Text;
            string puesto = txt_Puesto.Text;
            float salario = float.Parse(txt_Salario.Text);

            Empleado miEmpleado = new Empleado();
            miEmpleado.Nombre = nombre;
            miEmpleado.Puesto = puesto;
            miEmpleado.Salario = salario;
            try
            {
                data.InsertarEmpleado(miEmpleado);//lo guardo en la db
                                                  //listaEmpleados.Add(miEmpleado); //esto lo usaría si yo quisiera mostras determinado dato y no todo
                listaEmpleados = data.SeleccionarEmpleados();
                listBox1.DataSource = listaEmpleados;
                LimpiarTxt();
            }
            catch (Exception ex) //linkear MySqlException
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void LimpiarTxt()
        {
            // voy a ahacer un metodo polimorfico que permita recorrer
            // la lista de controles y concentrarme en los controles q quiero
            foreach (Control c in this.Controls)
            {
                if (c is TextBox) 
                { 
                    TextBox tb = (TextBox)c;
                    tb.Clear(); // aca estaria limpiando los textbox siempre y cuando
                                // esten dentro de la la coleccion de controles del form
                }
            }
        }

        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txt_Id.Text);

            try
            {
                data.EliminarEmpleado(id);
                listaEmpleados = data.SeleccionarEmpleados();
                listBox1.DataSource = listaEmpleados;
                LimpiarTxt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Modificar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txt_Id.Text);
            string nombre = txt_Nombre.Text;
            string puesto = txt_Puesto.Text;
            float salario = float.Parse(txt_Salario.Text);

            Empleado miEmpleado = new Empleado();
            miEmpleado.Nombre = nombre;
            miEmpleado.Puesto = puesto;
            miEmpleado.Salario = salario;

            try
            {
                data.ModificarEmpleado(miEmpleado);
                listaEmpleados = data.SeleccionarEmpleados();
                listBox1.DataSource = listaEmpleados;
                LimpiarTxt();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Limpiar_Click(object sender, EventArgs e)
        {
            LimpiarTxt();
        }

        // es cuando el indice de la lista cambia
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)//cuando presione en la lista un elemento, cambia el índice
        {
            int index = listBox1.SelectedIndex;
            Empleado emp = listaEmpleados[index];

            txt_Id.Text = emp.Id.ToString();
            txt_Nombre.Text = emp.Nombre;
            txt_Puesto.Text = emp.Puesto;
            txt_Salario.Text = emp.Salario.ToString();

        }
    }
}
