using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryDiFiniInstanciaEvaluativa1
{
    internal class clsRubros
    {
        //Se agregan variables para los archivos que se van a usar
        public string NombreArchivo = "RUBROS.csv";
        public string NombreArchivoArticulos = "ARTICULOS.csv";

        // Cargar los rubros de RUBBROS.csv en el ComboBox
        public void GuardarDatos(ComboBox Combo)
        {
            string DatosLeidos;
            string[] VecDatos;

            StreamReader AD = new StreamReader(NombreArchivo); //Lee el archivo

            Combo.Items.Clear();

            //Valida que se agregue el nombre del rubro al combo mientras el archivo no esté vacio
            while ((DatosLeidos = AD.ReadLine()) != null)
            {
                VecDatos = DatosLeidos.Split(';'); //Separa los datos para trabajarlos mejor
                Combo.Items.Add(VecDatos[1]); // Guarda el nombre del rubro
            }

            AD.Close(); //Cierra archivo
            AD.Dispose();
        }

        //Carga los datos del archivo en el DataGridView segun el rubro
        public void CargarDatosGrilla(DataGridView Grilla, String RubroBuscado)
        {
            string DatosLeidos;
            string[] VecDatos;

            StreamReader AD = new StreamReader(NombreArchivoArticulos);

            Grilla.Rows.Clear();

            while ((DatosLeidos = AD.ReadLine()) != null)
            {
                VecDatos = DatosLeidos.Split(';');

                //Variable para cada valor de la grilla
                String Codigo = VecDatos[0];
                String Descripcion = VecDatos[1];
                Decimal Costo = Convert.ToDecimal(VecDatos[2]);
                String Rubro = VecDatos[3];
                Int32 Stock = Convert.ToInt32(VecDatos[4]);

                //Valida que muestre los datos solo del rubro elegido
                if (Rubro == RubroBuscado)
                {
                    //Calcula el valor del stock solo de ese rubro
                    Decimal ValorStock = Costo * Stock;
                    Grilla.Rows.Add(Codigo, Descripcion, Costo, Stock, ValorStock);
                }
            }

            AD.Close();
            AD.Dispose();
        }
    }
}
