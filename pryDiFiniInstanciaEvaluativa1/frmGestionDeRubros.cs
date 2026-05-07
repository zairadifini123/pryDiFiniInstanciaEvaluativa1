// ======================= FORM frmGestionDeRubros CORREGIDO =======================
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
            //Lleva al link a un nuevo formulario para mostrar la información del alumno
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
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            //Valida que el usuario seleccione un rubro
            if (cmbRubros.SelectedIndex != -1)
            {
                //Guarda en una nueva variable el total del valor de stock para mostrarlo en el label
                Decimal Total;
                Total = Rubros.CargarDatosGrilla(dgvArticulos, cmbRubros.Text);
                lblTotalValorStock.Text = Total.ToString("0.00");

                Int32 Cantidad;
                Cantidad = Rubros.ContarArticulosPorRubro(cmbRubros.Text);
                lblCantidadArticulosListados.Text = Cantidad.ToString();
            }
            else
            {
                MessageBox.Show("Seleccione un rubro");
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            //Llama el método
            Rubros.ExportarDatos(dgvArticulos);
        }
    }
} 