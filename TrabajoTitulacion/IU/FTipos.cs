﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos.CMM;
using TrabajoTitulacion.Servicios.CMM.TiposPersonalizados;

namespace TrabajoTitulacion.IU
{
    public partial class FTipos : Form
    {
        private string nombreModelo;
        private FGestorModelos fgestorModelos;
        public FTipos()
        {
            InitializeComponent();
        }
        public FTipos(FGestorModelos fgestorModelos ,string nombreModelo)
        {
            InitializeComponent();
            this.nombreModelo = nombreModelo;
            this.fgestorModelos = fgestorModelos;
        }

        private async void FTipos_Load(object sender, EventArgs e)
        {
            btnModeloNav.Text = nombreModelo;
            btnModeloNav.AutoSize = true;
            await PoblarDtgv();
            NuevaPlantilla();
        }
        private async Task PoblarDtgv()
        {
            List<Type1> modelos = await TiposPersonalizadosServicioStatic.ObtenerTiposPersonalizados(nombreModelo);
            dtgviewDatos.AutoGenerateColumns = false;
            dtgviewDatos.DataSource = modelos;
            dtgviewDatos.Columns["clmNombreTipo"].DataPropertyName = "Name";
            dtgviewDatos.Columns["clmEtiquetaPresentacionTipo"].DataPropertyName = "Title";
            dtgviewDatos.Columns["clmPadreTipo"].DataPropertyName = "ParentName";
        }

        private void btnVolverModelos_Click(object sender, EventArgs e)
        {
            fgestorModelos.AbrirModelos();
        }

        private void btnCerrarPlantilla_Click(object sender, EventArgs e)
        {
            panelTipo.Visible = false;
        }

        private async void NuevaPlantilla()
        {
            txtNombre.Clear();
            txtTitulo.Clear();
            txtDescripcion.Clear();
            await cargarCmbxPadres();
            btnAceptarTipo.Text = "Crear";
            lblEstado.Text = "Creando";
        }

        private async Task cargarCmbxPadres()
        {
            List<Type1> tiposActivos = await TiposPersonalizadosServicioStatic.ObtenerTiposActivos();
            foreach (var tipo in tiposActivos)
            {
                cmbxPadre.Items.Add(tipo.PrefixedName);
            }
            cmbxPadre.Items.Add("cm:content");
            cmbxPadre.Items.Add("cm:folder");
        }

        private void btnCrearTipo_Click(object sender, EventArgs e)
        {
            NuevaPlantilla();            
        }

        private async void btnAceptarTipo_Click(object sender, EventArgs e)
        {
            if(btnAceptarTipo.Text == "Crear")
            {
                Type1 tipoCrear = new Type1();
                tipoCrear.Name = txtNombre.Text;
                tipoCrear.ParentName = cmbxPadre.SelectedItem.ToString();
                tipoCrear.Description = txtDescripcion.Text;
                tipoCrear.Title = txtTitulo.Text;
                tipoCrear.ModeloPerteneciente.Name = nombreModelo;
                await TiposPersonalizadosServicioStatic.CrearTipoPersonalizado(tipoCrear);
                MessageBox.Show("Tipo creado exitosamente");
                await PoblarDtgv();
            }else if(btnAceptarTipo.Text == "Editar")
            {
                Type1 tipoActualizar = new Type1();
                tipoActualizar.Name = txtNombre.Text;
                tipoActualizar.ParentName = cmbxPadre.SelectedItem.ToString();
                tipoActualizar.Description = txtDescripcion.Text;
                tipoActualizar.Title = txtTitulo.Text;
                tipoActualizar.ModeloPerteneciente.Name = nombreModelo;
                await TiposPersonalizadosServicioStatic.ActualizarTipoPersonalizado(tipoActualizar);
                MessageBox.Show("Tipo actualizado exitosamente");
                await PoblarDtgv();
                NuevaPlantilla();
            }
        }

        private void tlstripCrearTipo_Click(object sender, EventArgs e)
        {
            NuevaPlantilla();
        }

        private void dtgviewTipos_MouseDown(object sender, MouseEventArgs e)
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

        private void tlstripEditar_Click(object sender, EventArgs e)
        {
            PlantillaEditar();
        }

        private async void PlantillaEditar()
        {
            Type1 tipoEditar = await TiposPersonalizadosServicioStatic.ObtenerTipoPersonalizado(
                nombreModelo, dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
            panelTipo.Visible = true;
            btnAceptarTipo.Text = "Editar";
            lblEstado.Text = "Editando Tipo";
            txtNombre.Text = tipoEditar.Name;
            txtNombre.Enabled = false;
            await cargarCmbxPadres();
            cmbxPadre.SelectedIndex = cmbxPadre.Items.IndexOf(tipoEditar.ParentName);
            txtTitulo.Text = tipoEditar.Title;
            txtDescripcion.Text = tipoEditar.Description;
        }

        private async void tlstripEliminar_Click(object sender, EventArgs e)
        {
            FLoading fPrincipalLoading = new FLoading();
            fPrincipalLoading.Show();
            await TiposPersonalizadosServicioStatic.EliminarTipoPersonalizado( nombreModelo,
                dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
            fPrincipalLoading.Close();
            MessageBox.Show("El tipo ha sido eliminado");
            await PoblarDtgv();
            dtgviewDatos.Refresh();
        }
    }
}
