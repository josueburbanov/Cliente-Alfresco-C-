using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos.CMM;
using TrabajoTitulacion.Servicios.CMM.ModelosPersonalizados;

namespace TrabajoTitulacion.IU
{
    public partial class FModelos : Form
    {
        private FGestorModelos fgestorModelos;
        public FModelos()
        {
            InitializeComponent();
        }
        public FModelos(FGestorModelos formPrincipal)
        {
            InitializeComponent();
            this.fgestorModelos= formPrincipal;
        }


        private async void FModelos_Load(object sender, EventArgs e)
        {
            await PoblarDtgvModelos();
        }
        private async Task PoblarDtgvModelos()
        {
            List<Model> modelos = await ModelosPersonalizadosServicioStatic.ObtenerModelosPersonalizados();
            dtgviewDatos.AutoGenerateColumns = false;
            dtgviewDatos.DataSource = modelos;
            dtgviewDatos.Columns["clmNombreModelo"].DataPropertyName = "Name";
            dtgviewDatos.Columns["clmEspacioNombres"].DataPropertyName = "NamespaceUri";
            dtgviewDatos.Columns["clmEstadoModelo"].DataPropertyName = "Status";
        }

        private async void btnAceptarModelo_Click(object sender, EventArgs e)
        {
            if (btnAceptarModelo.Text == "Crear")
            {
                Model modelo = new Model();
                modelo.Name = txtNombreModelo.Text;
                modelo.NamespaceUri = txtEspacioModelo.Text;
                modelo.NamespacePrefix = txtPrefijoModelo.Text;
                modelo.Description = txtDescripcionModelo.Text;
                var mdiParent = MdiParent as FDashboard;
                string autorModelo = mdiParent.PersonaActual.FirstName + mdiParent.PersonaActual.LastName;
                modelo.Author = autorModelo;
                modelo.Status = "DRAFT";
                FLoading fPrincipalLoading = new FLoading();
                fPrincipalLoading.Show();
                await ModelosPersonalizadosServicioStatic.CrearModeloPersonalizado(modelo);
                fPrincipalLoading.Close();
                MessageBox.Show("El nuevo modelo ha sido creado con éxito");
                await PoblarDtgvModelos();
                dtgviewDatos.Refresh();
                panelModelo.Visible = false;
            }
            else if (btnAceptarModelo.Text == "Editar")
            {
                Model modelo = new Model();
                modelo.Name = txtNombreModelo.Text;
                modelo.NamespaceUri = txtEspacioModelo.Text;
                modelo.NamespacePrefix = txtPrefijoModelo.Text;
                modelo.Description = txtDescripcionModelo.Text;
                var mdiParent = MdiParent as FDashboard;
                FLoading fPrincipalLoading = new FLoading();
                fPrincipalLoading.Show();
                await ModelosPersonalizadosServicioStatic.ActualizarModeloPersonalizado(modelo);
                fPrincipalLoading.Close();
                MessageBox.Show("El modelo " + modelo.Name + " ha sido actualizado con éxito");
                await PoblarDtgvModelos();
                dtgviewDatos.Refresh();
                panelModelo.Visible = false;
                PlantillaNuevoModelo();
            }
        }

        private void btnNuevoModelo_Click(object sender, EventArgs e)
        {
            PlantillaNuevoModelo();
        }
        private void PlantillaNuevoModelo()
        {
            txtNombreModelo.Enabled = true;
            panelModelo.Visible = true;
            btnAceptarModelo.Text = "Crear";
            lblEstadoModelo.Text = "Creando Modelo";
            txtNombreModelo.Clear();
            txtEspacioModelo.Clear();
            txtPrefijoModelo.Clear();
            txtDescripcionModelo.Clear();
        }

        private void tlstripCrearModelo_Click(object sender, EventArgs e)
        {
            PlantillaNuevoModelo();
        }

        private void tlstripEditar_Click(object sender, EventArgs e)
        {
            PlantillaEditarModelo();
        }
        private async void PlantillaEditarModelo()
        {
            Model modeloEditar = await ModelosPersonalizadosServicioStatic.ObtenerModeloPersonalizado(
                dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
            panelModelo.Visible = true;
            btnAceptarModelo.Text = "Editar";
            lblEstadoModelo.Text = "Editando Modelo";
            txtNombreModelo.Text = modeloEditar.Name;
            txtNombreModelo.Enabled = false;
            txtEspacioModelo.Text = modeloEditar.NamespaceUri;
            txtPrefijoModelo.Text = modeloEditar.NamespacePrefix;
            txtDescripcionModelo.Text = modeloEditar.Description;
        }

        private void dtgviewModelos_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    var indicesDtgviewDatos = dtgviewDatos.HitTest(e.X, e.Y);
                    dtgviewDatos.ClearSelection();
                    dtgviewDatos.Rows[indicesDtgviewDatos.RowIndex].Selected = true;
                    dtgviewDatos.ContextMenuStrip = cntxMenuAcciones;
                }
                catch (ArgumentOutOfRangeException)
                {
                    dtgviewDatos.ContextMenuStrip = cntxMenuGeneral;
                }
            }
        }

        private async void tlstripActivarDesactivar_Click(object sender, EventArgs e)
        {
            Model modeloCambiarEstado = await ModelosPersonalizadosServicioStatic.ObtenerModeloPersonalizado(
                dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
            FLoading fPrincipalLoading = new FLoading();
            fPrincipalLoading.Show();
            await ModelosPersonalizadosServicioStatic.CambiarEstadoModeloPersonalizado(modeloCambiarEstado);
            fPrincipalLoading.Close();
            MessageBox.Show("El modelo " + modeloCambiarEstado.Name + " ha sido cambiado de estado");
            await PoblarDtgvModelos();
            dtgviewDatos.Refresh();
        }

        private async void tlstripEliminar_Click(object sender, EventArgs e)
        {
            FLoading fPrincipalLoading = new FLoading();
            fPrincipalLoading.Show();
            await ModelosPersonalizadosServicioStatic.EliminarModeloPersonalizado(
                dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
            fPrincipalLoading.Close();
            MessageBox.Show("El modelo ha sido eliminado");
            await PoblarDtgvModelos();
            dtgviewDatos.Refresh();
        }

        private void btnCerrarPlantilla_Click(object sender, EventArgs e)
        {
            panelModelo.Visible = false;
        }

        private async void tlstripTiposPersonalizados_Click(object sender, EventArgs e)
        {
            Model modeloSeleccionado = await ModelosPersonalizadosServicioStatic.ObtenerModeloPersonalizado(
                dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
            fgestorModelos.AbrirTipos(modeloSeleccionado.Name);
        }

        private void btnNuevoModeloNav_Click(object sender, EventArgs e)
        {
            PlantillaNuevoModelo();
        }

        private void btnVolverInicio_Click(object sender, EventArgs e)
        {
            fgestorModelos.VolverInicio();
        }

        private async void dtgviewModelos_DoubleClick(object sender, EventArgs e)
        {
            Model modeloSeleccionado = await ModelosPersonalizadosServicioStatic.ObtenerModeloPersonalizado(
                dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
            fgestorModelos.AbrirTipos(modeloSeleccionado.Name);
        }

        private async void aspectosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Model modeloSeleccionado = await ModelosPersonalizadosServicioStatic.ObtenerModeloPersonalizado(
                dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
            fgestorModelos.AbrirAspectos(modeloSeleccionado.Name);
        }

        private void txtNombreModelo_TextChanged(object sender, EventArgs e)
        {
            if ((new Regex("^(?!(.*[\\\"\\*\\\\\\>\\<\\?\\/\\:\\|]+.*) | (.*[\\.]?.*[\\.] +$)| (.*[ ] +$))")).IsMatch(txtNombreModelo.Text))
            {
                txtNombreModelo.ForeColor = Color.Black;
            }
            else
            {
                txtNombreModelo.ForeColor = Color.Red;
            }
        }
    }
}
