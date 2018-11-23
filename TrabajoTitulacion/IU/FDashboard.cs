using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos.CoreAPI;
using TrabajoTitulacion.Servicios;
using TrabajoTitulacion.Servicios.Core.Personas;
using TrabajoTitulacion.UI;

namespace TrabajoTitulacion.IU
{
    public partial class FDashboard : Form
    {
        private string idPersona;
        public Person PersonaActual { get; set; }
        
        public FDashboard()
        {
            InitializeComponent();
        }
        public FDashboard(string idPersona)
        {
            InitializeComponent();
            this.idPersona = idPersona;
        }

        private async void FDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                PersonaActual = await PersonasStatic.ObtenerPersona(idPersona);                                
                tlstripMenuUsuario.Text = PersonaActual.FirstName;
                AñadirFormsHijos();
                AbrirInicio();
            }
            catch(UnauthorizedAccessException)
            {                
                MessageBox.Show("Lo sentimos, ocurrió un error al cargar sus datos");
                await Logout();
            }
        }



        /// <summary>
        /// Añade los formularios hijos al formulario principal, asigna indices a cada form, 
        /// 0:Inicio, 1:Repositorio, etc. Para evitar crear múltiples veces y mantener el estado.
        /// </summary>
        private void AñadirFormsHijos()
        {
            FInicio fInicio = new FInicio();
            fInicio.MdiParent = this;
            FRepositorio fRespositorio = new FRepositorio();
            fRespositorio.MdiParent = this;
            FGestorModelos fGestorModelos = new FGestorModelos();
            fGestorModelos.MdiParent = this;
            FSync fSync = new FSync();
            fSync.MdiParent = this;
            FBusqueda fBusqueda = new FBusqueda();
            fBusqueda.MdiParent = this;
            //forms to add            
        }
                
        /// <summary>
        /// Abre el formulario de FInicio
        /// </summary>
        public void AbrirInicio()
        {
            EsconderFormsNoActuales(0);
            MdiChildren[0].Dock = DockStyle.Fill;
            MdiChildren[0].Show();
        }

        /// <summary>
        /// Abre el repositorio de Usuario (FRepositorio)
        /// </summary>
        public void AbrirRepositorio()
        {
            EsconderFormsNoActuales(1);
            MdiChildren[1].Dock = DockStyle.Fill;
            MdiChildren[1].Show();
        }

        /// <summary>
        /// Abre el gestor de modelos (FModelos) y esconde los demás
        /// </summary>
        public void AbrirGestorModelos()
        {
            EsconderFormsNoActuales(2);
            MdiChildren[2].Dock = DockStyle.Fill;
            MdiChildren[2].Show();
        }
        /// <summary>
        /// Abre el formulario de sincronización (FSync)
        /// </summary>
        public void AbrirSincronizacion()
        {
            EsconderFormsNoActuales(3);
            MdiChildren[3].Dock = DockStyle.Fill;
            MdiChildren[3].Show();
        }

        public void AbrirBusqueda()
        {
            EsconderFormsNoActuales(4);
            MdiChildren[4].Dock = DockStyle.Fill;
            MdiChildren[4].Show();
        }

        /// <summary>
        /// Esconde los formularios que no han sido seleccionados, mantiene su estado
        /// </summary>
        /// <param name="indiceForm"></param>
        private void EsconderFormsNoActuales(int indiceForm)
        {
            foreach (var formHijo in MdiChildren)
            {
                if (MdiChildren[indiceForm].Equals(formHijo)) continue;
                formHijo.Hide();
            }
        }


        /// <summary>
        /// Se elimina ticket de autenticación local y remoto
        /// </summary>
        /// <returns></returns>
        private async Task Logout()
        {
            try
            {
                //Se envía petición para cerrar sesión
                await AutenticacionStatic.CerrarSesion(idPersona);
                MessageBox.Show("Su sesión ha sido finalizada correctamente");

            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Hubo un problema cerrando su sesión");
            }
            finally
            {
                //De cualquier manera, cuando se cierra sesión, se abre el FPrincipal (De inicio de sesión)
                FLogin fprincipal = new FLogin();
                Hide();
                fprincipal.ShowDialog();
                Close();
            }
        }

        private async Task cerrarSesiónToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            await Logout();
        }


        private void toolStripMenuRepositorio_Click(object sender, EventArgs e)
        {
            AbrirRepositorio();
        }


        private void toolStripMenuInicio_Click(object sender, EventArgs e)
        {
            AbrirInicio();
        }

        private void toolStripMenuGestorDeModelos_Click(object sender, EventArgs e)
        {
            AbrirGestorModelos();
        }

        private void tlstripMenuSincronizacion_Click(object sender, EventArgs e)
        {
            AbrirSincronizacion();
        }

        private void tlstripMenuBusqueda_Click(object sender, EventArgs e)
        {
            AbrirBusqueda();
        }
    }
}
