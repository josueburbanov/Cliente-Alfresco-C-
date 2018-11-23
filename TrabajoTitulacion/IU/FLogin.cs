using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.IU;
using TrabajoTitulacion.Servicios;

namespace TrabajoTitulacion.UI
{
    public partial class FLogin : Form
    {
        FLoading fPrincipalLoading = new FLoading();
        public FLogin()
        {
            InitializeComponent();
        }

        private async void btnIngresar_Click(object sender, EventArgs e)
        {
            //Muestra la ventana de loading...
            fPrincipalLoading.Show();

            //Llamada asíncrona a Login
            await IniciarSesion();

            //Una vez logueado elimina la ventana de carga e ingresa al dashboard
            fPrincipalLoading.Close();
            IngresarDashboard();
        }

        /// <summary>
        /// Inicia sesión
        /// </summary>
        /// <returns></returns>
        private async Task IniciarSesion()
        {
            try
            {
                //autenticación asíncrona
                await AutenticacionStatic.IniciarSesion(txtNombreUsuario.Text, txtContraseña.Text);
            }
            catch (UnauthorizedAccessException)
            {
                //Autenticación no exitosa. Mensaje de error y limpieza de campos
                lblErrorAutenticacion.Visible = true;
                txtContraseña.Clear();
                txtNombreUsuario.Clear();
            }
        }
        
        private async void txtNombreUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Muestra la ventana de loading...
                fPrincipalLoading.Show();

                //Llamada asíncrona a Login
                await IniciarSesion();

                //Una vez logueado elimina la ventana de carga e ingresa al dashboard
                fPrincipalLoading.Close();
                IngresarDashboard();

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
                await IniciarSesion();

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
