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
            

            //Llamada asíncrona a Login
            await IniciarSesion();

            
        }

        /// <summary>
        /// Inicia sesión
        /// </summary>
        /// <returns></returns>
        private async Task IniciarSesion()
        {
            try
            {
                //Muestra la ventana de loading...
                fPrincipalLoading.Show();
                //autenticación asíncrona
                await AutenticacionStatic.IniciarSesion(txtNombreUsuario.Text, txtContraseña.Text);
                fPrincipalLoading.Close();
                IngresarDashboard();
                //Una vez logueado elimina la ventana de carga e ingresa al dashboard
                
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
                //Llamada asíncrona a Login
                await IniciarSesion();
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
                //Llamada asíncrona a Login
                await IniciarSesion();
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
