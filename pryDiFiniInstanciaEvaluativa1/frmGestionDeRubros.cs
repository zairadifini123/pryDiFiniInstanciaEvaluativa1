using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryDiFiniInstanciaEvaluativa1
{
    public partial class frmGestionDeRubros : Form
    {
        public frmGestionDeRubros()
        {
            InitializeComponent();
        }

        //Crear un objeto de la clase clsRubros para poder usar sus métodos dentro del formulario
        clsRubros Rubros = new clsRubros(); 
        private void lnkInformacionDelAlumno_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInformacionDelAlumno frmInformacionDelAlumno = new frmInformacionDelAlumno();
            frmInformacionDelAlumno.ShowDialog(); 
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void frmGestionDeRubros_Load(object sender, EventArgs e)
        {
            //Llama el método de la clase para guardar el nombre de los rubros en el combo box
            Rubros.GuardarDatos(cmbRubros);

            //Calcular el producto de ValorStock
            Decimal ValorStock;
            ValorStock = Convert.ToDecimal(Costo) * Convert.ToDecimal(Stock); 
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            //Llama al metodo para mostrar los datos
            Rubros.CargarDatosGrilla(dgvArticulos, cmbRubros.Text); 
        }
    }
}
