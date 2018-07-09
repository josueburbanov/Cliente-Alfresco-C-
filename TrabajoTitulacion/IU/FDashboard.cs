using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.Servicios;
using TrabajoTitulacion.Servicios.Core.Personas;
using TrabajoTitulacion.UI;

namespace TrabajoTitulacion.IU
{
    public partial class FDashboard : Form
    {
        private string idPersona;
                        
        public FDashboard()
        {
            InitializeComponent();
        }
        public FDashboard(string idPersona)
        {
            InitializeComponent();
            this.idPersona = idPersona;
        }

        private void FDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                PersonasStatic.ObtenerPersona(idPersona);
                toolStripMenuUsuario.Text = PersonasStatic.PersonaActual.FirstName;
                AñadirFormsHijos();
                AbrirInicio();
            }
            catch(UnauthorizedAccessException)
            {
                MessageBox.Show("Lo sentimos, ocurrió un error al cargar sus datos");
                Logout();
            }
        }


        /// <summary>
        /// Añade los formularios hijos al formulario principal, asigna indices a cada form, 0:Inicio, 1:Repositorio, etc
        /// </summary>
        private void AñadirFormsHijos()
        {
            FInicio fInicio = new FInicio();
            fInicio.MdiParent = this;
            FRepositorio fRespositorio = new FRepositorio();            
            fRespositorio.MdiParent = this;

            //forms to add            
        }

        private void AbrirInicio()
        {
            EsconderFormsNoActuales(0);
            MdiChildren[0].Dock = DockStyle.Fill;
            MdiChildren[0].Show();
        }

        public void AbrirRepositorio()
        {
            EsconderFormsNoActuales(1);
            MdiChildren[1].Dock = DockStyle.Fill;
            MdiChildren[1].Show();
        }

        private void EsconderFormsNoActuales(int indiceForm)
        {
            foreach (var formHijo in MdiChildren)
            {
                if (MdiChildren[indiceForm].Equals(formHijo)) continue;
                formHijo.Hide();
            }
        }


        private void Logout()
        {
            try
            {
                AutenticacionStatic.Logout();
                MessageBox.Show("Su sesión ha sido finalizada correctamente");

            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Hubo un problema cerrando su sesión");
            }
            finally
            {
                FPrincipal fprincipal = new FPrincipal();
                Hide();
                fprincipal.ShowDialog();
                Close();
            }
        }

        private void cerrarSesiónToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Logout();
        }


        private void toolStripMenuRepositorio_Click(object sender, EventArgs e)
        {
            AbrirRepositorio();
        }


        private void toolStripMenuInicio_Click(object sender, EventArgs e)
        {
            AbrirInicio();
        }
    }
}
