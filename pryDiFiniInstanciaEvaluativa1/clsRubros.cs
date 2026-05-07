using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            int IND = 0; 

            StreamReader AD = new StreamReader(NombreArchivo); //Lee el archivo

            Combo.Items.Clear();

            //Valida que se agregue el nombre del rubro al combo mientras el archivo no esté vacio
            while ((DatosLeidos = AD.ReadLine()) != null)
            {
                VecDatos = DatosLeidos.Split(';'); //Separa los datos para trabajarlos mejor
                Combo.Items.Add(VecDatos[0]); // Guarda el nombre del rubro
                //incrementa el indice para pasar a la siguiente linea del archivo
                IND++; 
            }
            AD.Close(); //Cierra archivo
            AD.Dispose();
        }

        //Carga los datos del archivo en el DataGridView segun el rubro
        public Decimal CargarDatosGrilla(DataGridView Grilla, String RubroBuscado)
        {
            string DatosLeidos;
            string[] VecDatos;
            Decimal TotalValorStock = 0; // Para calcular el total del valor de stock
     
            StreamReader AD = new StreamReader(NombreArchivoArticulos);

            Grilla.Rows.Clear();

            while ((DatosLeidos = AD.ReadLine()) != null)
            {
                VecDatos = DatosLeidos.Split(';');

                //Valida que la línea tenga todos los campos necesarios
                if (VecDatos.Length >= 5)
                {
                    String Codigo = VecDatos[0];
                    String Descripcion = VecDatos[1];
                    Decimal Costo = Convert.ToDecimal(VecDatos[2]);
                    String Rubro = VecDatos[3];
                    Int32 Stock = Convert.ToInt32(VecDatos[4]);

                    //Verifica que se muestren los datos solo del rubro seleccionado en el combo box
                    if (Rubro == RubroBuscado)
                    {
                        Decimal ValorStock = Costo * Stock; //Calcula la columna de valor del stock

                        TotalValorStock = TotalValorStock + ValorStock; //Calcula el total del valor de stock con un acumulador 

                        //Agrega los datos a la grilla
                        int i = Grilla.Rows.Add();

                        Grilla.Rows[i].Cells[0].Value = Codigo;
                        Grilla.Rows[i].Cells[1].Value = Descripcion;
                        Grilla.Rows[i].Cells[2].Value = Costo;
                        Grilla.Rows[i].Cells[3].Value = Stock;
                        Grilla.Rows[i].Cells[4].Value = ValorStock;
                    }
                }
            }
            AD.Close();
            AD.Dispose();

            return TotalValorStock; //Devuelve el total del valor de stock para mostrarlo en el label
        }

        //Mètodo para contar la cantidad de artículos por rubro
        public Int32 ContarArticulosPorRubro(String RubroBuscado)
        {
            string DatosLeidos;
            string[] VecDatos;
            Int32 CantidadArticulos = 0;

            StreamReader AD = new StreamReader(NombreArchivoArticulos);

            while ((DatosLeidos = AD.ReadLine()) != null)
            {
                VecDatos = DatosLeidos.Split(';');

                if (VecDatos.Length >= 5)
                {
                    String Rubro = VecDatos[3];

                    if (Rubro == RubroBuscado)
                    {
                        CantidadArticulos = CantidadArticulos + 1;
                    }
                }
            }

            AD.Close();
            AD.Dispose();

            return CantidadArticulos;
        }

        //Mètodo para exportar los datos de la grilla, solo del rubro seleccionado, a un nuevo archivo
        public void ExportarDatos(DataGridView Grilla)
        {
            // Crea un nuevo archivo para exportar los datos, si el archivo ya existe lo sobreescribe con el "false"
            StreamWriter Exportacion = new StreamWriter("Exportacion.csv", false, Encoding.UTF8);

            //Agrega el nombre de las columnas
            Exportacion.Write("Codigo");
            Exportacion.Write(";");
            Exportacion.Write("Descripcion");
            Exportacion.Write(";");
            Exportacion.Write("Costo");
            Exportacion.Write(";");
            Exportacion.Write("Stock");
            Exportacion.Write(";");
            Exportacion.WriteLine("ValorStock");

            //Mientras la grilla tenga filas, exporta los datos 
            for (Int32 i = 0; i < Grilla.Rows.Count; i++)
            {
                if (Grilla.Rows[i].Cells[0].Value != null)
                {
                    //Separa los datos con ";" para separarlos en columnas
                    Exportacion.Write(Grilla.Rows[i].Cells[0].Value);
                    Exportacion.Write(";");
                    Exportacion.Write(Grilla.Rows[i].Cells[1].Value);
                    Exportacion.Write(";");
                    Exportacion.Write(Grilla.Rows[i].Cells[2].Value);
                    Exportacion.Write(";");
                    Exportacion.Write(Grilla.Rows[i].Cells[3].Value);
                    Exportacion.Write(";");
                    Exportacion.WriteLine(Grilla.Rows[i].Cells[4].Value); //WriteLine para agregar un salto de linea al final de cada fila
                }
            }

            Exportacion.Close(); //Cierra este archivo nuevo
            Exportacion.Dispose(); //Libera los recursos 

            MessageBox.Show("Datos exportados");
        }

    }
}
