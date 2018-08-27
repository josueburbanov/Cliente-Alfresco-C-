using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos.CMM;

namespace TrabajoTitulacion.IU
{
    public partial class FPropiedades : Form
    {
        private string nombreModelo;
        private string nombreSubModelo; //Tipo o Aspecto
        private FGestorModelos fgestorModelos;
        public FPropiedades()
        {
            InitializeComponent();
        }
        public FPropiedades(FGestorModelos fgestorModelos, string nombreModelo, string nombreSubModelo)
        {
            InitializeComponent();
            this.nombreModelo = nombreModelo;
            this.nombreSubModelo = nombreSubModelo;
            this.fgestorModelos = fgestorModelos;
        }
        private async void FPropiedades_Load(object sender, EventArgs e)
        {
            lnklblModeloNav.Text = nombreModelo;
            lnklblSubmodeloNav.Text = nombreSubModelo;
            //await PoblarDtgv();
        }

    //    private async Task PoblarDtgv()
    //    {
    //        List<Property> modelos = await AspectosPersonalizadosServicioStatic.ObtenerAspectosPersonalizados(nombreModelo);
    //        dtgviewDatos.AutoGenerateColumns = false;
    //        dtgviewDatos.DataSource = modelos;
    //        dtgviewDatos.Columns["clmNombreTipo"].DataPropertyName = "Name";
    //        dtgviewDatos.Columns["clmEtiquetaPresentacionTipo"].DataPropertyName = "Title";
    //        dtgviewDatos.Columns["clmPadreTipo"].DataPropertyName = "ParentName";
    //    }

    //    private void btnVolverModelos_Click(object sender, EventArgs e)
    //    {
    //        fgestorModelos.AbrirModelos();
    //    }

    //    private void btnCerrarPlantilla_Click(object sender, EventArgs e)
    //    {
    //        panelTipo.Visible = false;
    //    }

    //    private async void NuevaPlantilla()
    //    {
    //        txtNombre.Clear();
    //        txtTitulo.Clear();
    //        txtDescripcion.Clear();
    //        await cargarCmbxPadres();
    //        btnAceptar.Text = "Crear";
    //        lblEstado.Text = "Creando";
    //    }

    //    private async Task cargarCmbxPadres()
    //    {
    //        List<Aspect> aspectosActivos = await AspectosPersonalizadosServicioStatic.ObtenerAspectosActivos();
    //        foreach (var aspecto in aspectosActivos)
    //        {
    //            cmbxPadre.Items.Add(aspecto.PrefixedName);
    //        }
    //        cmbxPadre.Items.Add("g2d:editingInGoogle");
    //        cmbxPadre.Items.Add("g2d:sharedInGoogle");
    //    }

    //    private async void btnAceptarTipo_Click(object sender, EventArgs e)
    //    {
    //        if (btnAceptar.Text == "Crear")
    //        {
    //            Aspect aspectoCrear = new Aspect();
    //            aspectoCrear.Name = txtNombre.Text;
    //            aspectoCrear.ParentName = cmbxPadre.SelectedItem.ToString();
    //            aspectoCrear.Description = txtDescripcion.Text;
    //            aspectoCrear.Title = txtTitulo.Text;
    //            aspectoCrear.ModeloPerteneciente.Name = nombreModelo;
    //            await AspectosPersonalizadosServicioStatic.CrearAspectoPersonalizado(aspectoCrear);
    //            MessageBox.Show("Aspecto creado exitosamente");
    //            await PoblarDtgv();
    //        }
    //        else if (btnAceptar.Text == "Editar")
    //        {
    //            Aspect aspectoActualizar = new Aspect();
    //            aspectoActualizar.Name = txtNombre.Text;
    //            aspectoActualizar.ParentName = cmbxPadre.SelectedItem.ToString();
    //            aspectoActualizar.Description = txtDescripcion.Text;
    //            aspectoActualizar.Title = txtTitulo.Text;
    //            aspectoActualizar.ModeloPerteneciente.Name = nombreModelo;
    //            await AspectosPersonalizadosServicioStatic.ActualizarAspectoPersonalizado(aspectoActualizar);
    //            MessageBox.Show("Aspecto actualizado exitosamente");
    //            await PoblarDtgv();
    //        }
    //    }
    //    private void tlstripCrear_Click(object sender, EventArgs e)
    //    {
    //        NuevaPlantilla();
    //    }

    //    private void dtgviewDatos_MouseDown(object sender, MouseEventArgs e)
    //    {
    //        if (e.Button == MouseButtons.Right)
    //        {
    //            try
    //            {
    //                var indicesDtgviewDatos = dtgviewDatos.HitTest(e.X, e.Y);
    //                dtgviewDatos.ClearSelection();
    //                dtgviewDatos.Rows[indicesDtgviewDatos.RowIndex].Selected = true;
    //                dtgviewDatos.ContextMenuStrip = cntxMenuAcciones;
    //            }
    //            catch (ArgumentOutOfRangeException)
    //            {
    //                dtgviewDatos.ContextMenuStrip = cntxMenuGeneral;
    //            }
    //        }
    //    }

    //    private void tlstripEditar_Click_1(object sender, EventArgs e)
    //    {
    //        PlantillaEditar();
    //    }

    //    private async void PlantillaEditar()
    //    {
    //        Aspect aspectoEditar = await AspectosPersonalizadosServicioStatic.ObtenerAspectoPersonalizado(
    //            nombreModelo, dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
    //        panelTipo.Visible = true;
    //        btnAceptar.Text = "Editar";
    //        lblEstado.Text = "Editando Tipo";
    //        txtNombre.Text = aspectoEditar.Name;
    //        txtNombre.Enabled = false;
    //        await cargarCmbxPadres();
    //        cmbxPadre.SelectedIndex = cmbxPadre.Items.IndexOf(aspectoEditar.ParentName);
    //        txtTitulo.Text = aspectoEditar.Title;
    //        txtDescripcion.Text = aspectoEditar.Description;
    //    }

    //    private async void tlstripEliminar_Click(object sender, EventArgs e)
    //    {
    //        FLoading fPrincipalLoading = new FLoading();
    //        fPrincipalLoading.Show();
    //        await AspectosPersonalizadosServicioStatic.EliminarAspectoPersonalizado(nombreModelo,
    //            dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
    //        fPrincipalLoading.Close();
    //        MessageBox.Show("El aspecto ha sido eliminado");
    //        await PoblarDtgv();
    //        dtgviewDatos.Refresh();
    //    }

    //    private void lnklblVolverNav_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    //    {
    //        fgestorModelos.AbrirModelos();
    //    }

    //    private void tlstripCrearAspecto_Click(object sender, EventArgs e)
    //    {
    //        NuevaPlantilla();
    //    }


    }
}
