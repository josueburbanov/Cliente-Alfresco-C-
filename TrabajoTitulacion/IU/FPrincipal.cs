using System;
using System.Windows.Forms;
using TrabajoTitulacion.IU;
using TrabajoTitulacion.Servicios;

namespace TrabajoTitulacion.UI
{
    public partial class FPrincipal : Form
    {
        public FPrincipal()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void txtNombreUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }

        private void Login()
        {
            try
            {
                lblErrorAutenticacion.Visible = false;
                AutenticacionStatic.Login(txtNombreUsuario.Text, txtContraseña.Text);
                FDashboard fdashborad = new FDashboard(txtNombreUsuario.Text);
                Hide();
                fdashborad.ShowDialog();
                Close();
            }
            catch (UnauthorizedAccessException)
            {
                lblErrorAutenticacion.Visible = true;
                txtContraseña.Clear();
                txtNombreUsuario.Clear();
            }
        }

        private void txtContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            Login();
        }
    }
}
