using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.IU;
using TrabajoTitulacion.Servicios;

namespace TrabajoTitulacion.UI
{
    public partial class FPrincipal : Form
    {
        FLoading fPrincipalLoading = new FLoading();
        public FPrincipal()
        {
            InitializeComponent();
        }

        private async void btnIngresar_Click(object sender, EventArgs e)
        {
            //Muestra la ventana de loading...
            fPrincipalLoading.Show();

            //Llamada asíncrona a Login
            await Login();

            //Una vez logueado elimina la ventana de carga e ingresa al dashboard
            fPrincipalLoading.Close();
            IngresarDashboard();
        }

        private async void txtNombreUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Muestra la ventana de loading...
                fPrincipalLoading.Show();

                //Llamada asíncrona a Login
                await Login();

                //Una vez logueado elimina la ventana de carga e ingresa al dashboard
                fPrincipalLoading.Close();
                IngresarDashboard();

            }
        }

        private async Task Login()
        {
            try
            {
                await AutenticacionStatic.Login(txtNombreUsuario.Text, txtContraseña.Text);
            }
            catch (UnauthorizedAccessException)
            {
                lblErrorAutenticacion.Visible = true;
                txtContraseña.Clear();
                txtNombreUsuario.Clear();
            }
        }

        private void IngresarDashboard()
        {
            lblErrorAutenticacion.Visible = false;
            FDashboard fDashborad = new FDashboard(txtNombreUsuario.Text);
            Hide();
            fDashborad.ShowDialog();
            Close();
        }

        private async void txtContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Muestra la ventana de loading...
                fPrincipalLoading.Show();

                //Llamada asíncrona a Login
                await Login();

                //Una vez logueado elimina la ventana de carga e ingresa al dashboard
                fPrincipalLoading.Close();
                IngresarDashboard();

            }
        }

        private void FPrincipal_Load(object sender, EventArgs e)
        {
            
        }

        private void FPrincipal_Shown(object sender, EventArgs e)
        {
            
        }
    }
}
