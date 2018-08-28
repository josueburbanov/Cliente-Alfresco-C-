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
using TrabajoTitulacion.Servicios.CMM.AspectosPersonalizados;

namespace TrabajoTitulacion.IU
{
    public partial class FPropiedades : Form
    {
        private string nombreModelo;
        private dynamic subModelo; //Tipo o Aspecto
        private FGestorModelos fgestorModelos;
        private string proveniente;
        public FPropiedades()
        {
            InitializeComponent();
        }
        public FPropiedades(FGestorModelos fgestorModelos, string nombreModelo, object subModelo, string proveniente)
        {
            InitializeComponent();
            this.nombreModelo = nombreModelo;            
            this.fgestorModelos = fgestorModelos;
            this.proveniente = proveniente;
            if(proveniente == "TIPOS") this.subModelo = (Modelos.CMM.Type)subModelo;
            if (proveniente == "ASPECTOS") this.subModelo = (Aspect)subModelo;
        }
        private void FPropiedades_Load(object sender, EventArgs e)
        {
            lnklblModeloNav.Text = nombreModelo;
            lnklblSubmodeloNav.Text = subModelo.Name;
            PoblarDtgv();
            NuevaPlantilla();
        }

        private void PoblarDtgv()
        {
            dtgviewDatos.AutoGenerateColumns = false;
            dtgviewDatos.DataSource = subModelo.Properties;
            dtgviewDatos.Columns["clmNombreTipo"].DataPropertyName = "Name";
            dtgviewDatos.Columns["clmEtiquetaPresentacionTipo"].DataPropertyName = "Title";
            dtgviewDatos.Columns["clmTipoDato"].DataPropertyName = "DataType";
            dtgviewDatos.Columns["clmRequisito"].DataPropertyName = "Mandatory";
            dtgviewDatos.Columns["clmValorDefault"].DataPropertyName = "DefaultValue";
            dtgviewDatos.Columns["clmMultiplesValores"].DataPropertyName = "Multiple";

        }

        private void lnklblVolverNav_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (proveniente == "TIPOS") fgestorModelos.AbrirTipos(nombreModelo);
            if (proveniente == "ASPECTOS") fgestorModelos.AbrirAspectos(nombreModelo);
        }

        private void btnCerrarPlantilla_Click(object sender, EventArgs e)
        {
            flwlypanelPropiedades.Visible = false;
        }

        private void NuevaPlantilla()
        {
            txtNombre.Clear();
            txtTitulo.Clear();
            txtDescripcion.Clear();
            cmbxTipoDato.SelectedItem = "Ninguno";
            cmbxRequerido.SelectedItem = "Opcional";
            cmbxRestriccion .SelectedItem = "Ninguno";
            cmbxIndexacion.SelectedItem = "Ninguno";
            chkboxMultiple.Checked = false;
            btnAceptar.Text = "Crear";
            lblEstado.Text = "Crear";
        }
        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            if (btnAceptar.Text == "Crear")
            {
                Property propiedadCrear = new Property();
                propiedadCrear.Name = txtNombre.Text;
                propiedadCrear.Description = txtDescripcion.Text;
                propiedadCrear.Title = txtTitulo.Text;
                propiedadCrear.Datatype = cmbxTipoDato.SelectedItem.ToString();
                propiedadCrear.MultiValued = chkboxMultiple.Checked;
                if (cmbxRequerido.SelectedItem.ToString() == "Opcional") propiedadCrear.Mandatory = false;
                else { propiedadCrear.Mandatory = true; }
                if (cmbxRequerido.SelectedItem.ToString() == "Ninguno") propiedadCrear.Constraints = null;
                else { }
                if (cmbxIndexacion.SelectedItem.ToString() == "Ninguno")
                {
                    propiedadCrear.Facetable = "UNSET";
                    propiedadCrear.IndexTokenisationMode = "TRUE";
                    propiedadCrear.Indexed = false;
                    propiedadCrear.MandatoryEnforced = false;
                }

                List<Property> propiedadesCrear = new List<Property>();
                propiedadesCrear.Add(propiedadCrear);
                PropertiesBodyUpdate propertiesBodyUpdate=new PropertiesBodyUpdate(subModelo.Name, propiedadesCrear);
                if (proveniente == "ASPECTOS")
                {
                    await AspectosPersonalizadosServicioStatic.AñadirPropiedadeAspecto(
                        nombreModelo,
                        subModelo.Name,
                        propertiesBodyUpdate);
                    MessageBox.Show("Propiedad creada exitosamente");
                }
                if (proveniente == "TIPOS") fgestorModelos.AbrirAspectos(nombreModelo);
                PoblarDtgv();


            }
            //else if (btnAceptar.Text == "Editar")
            //{
            //    Aspect aspectoActualizar = new Aspect();
            //    aspectoActualizar.Name = txtNombre.Text;
            //    aspectoActualizar.ParentName = cmbxPadre.SelectedItem.ToString();
            //    aspectoActualizar.Description = txtDescripcion.Text;
            //    aspectoActualizar.Title = txtTitulo.Text;
            //    aspectoActualizar.ModeloPerteneciente.Name = nombreModelo;
            //    await AspectosPersonalizadosServicioStatic.ActualizarAspectoPersonalizado(aspectoActualizar);
            //    MessageBox.Show("Aspecto actualizado exitosamente");
            //    await PoblarDtgv();
            //}
        }

        //private void tlstripCrear_Click(object sender, EventArgs e)
        //{
        //    NuevaPlantilla();
        //}

        //private void dtgviewDatos_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right)
        //    {
        //        try
        //        {
        //            var indicesDtgviewDatos = dtgviewDatos.HitTest(e.X, e.Y);
        //            dtgviewDatos.ClearSelection();
        //            dtgviewDatos.Rows[indicesDtgviewDatos.RowIndex].Selected = true;
        //            dtgviewDatos.ContextMenuStrip = cntxMenuAcciones;
        //        }
        //        catch (ArgumentOutOfRangeException)
        //        {
        //            dtgviewDatos.ContextMenuStrip = cntxMenuGeneral;
        //        }
        //    }
        //}

        //private void tlstripEditar_Click_1(object sender, EventArgs e)
        //{
        //    PlantillaEditar();
        //}

        //private async void PlantillaEditar()
        //{
        //    Aspect aspectoEditar = await AspectosPersonalizadosServicioStatic.ObtenerAspectoPersonalizado(
        //        nombreModelo, dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
        //    panelTipo.Visible = true;
        //    btnAceptar.Text = "Editar";
        //    lblEstado.Text = "Editando Tipo";
        //    txtNombre.Text = aspectoEditar.Name;
        //    txtNombre.Enabled = false;
        //    await cargarCmbxPadres();
        //    cmbxPadre.SelectedIndex = cmbxPadre.Items.IndexOf(aspectoEditar.ParentName);
        //    txtTitulo.Text = aspectoEditar.Title;
        //    txtDescripcion.Text = aspectoEditar.Description;
        //}

        //private async void tlstripEliminar_Click(object sender, EventArgs e)
        //{
        //    FLoading fPrincipalLoading = new FLoading();
        //    fPrincipalLoading.Show();
        //    await AspectosPersonalizadosServicioStatic.EliminarAspectoPersonalizado(nombreModelo,
        //        dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
        //    fPrincipalLoading.Close();
        //    MessageBox.Show("El aspecto ha sido eliminado");
        //    await PoblarDtgv();
        //    dtgviewDatos.Refresh();
        //}

        //private void lnklblVolverNav_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    fgestorModelos.AbrirModelos();
        //}

        //private void tlstripCrearAspecto_Click(object sender, EventArgs e)
        //{
        //    NuevaPlantilla();
        //}
    }
}
