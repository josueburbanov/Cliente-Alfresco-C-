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
        private Model modelo;
        private dynamic subModelo; //Tipo o Aspecto
        private FGestorModelos fgestorModelos;
        private string proveniente;
        public FPropiedades()
        {
            InitializeComponent();
        }
        public FPropiedades(FGestorModelos fgestorModelos, Model modelo, object subModelo, string proveniente)
        {
            InitializeComponent();
            this.modelo = modelo;
            this.fgestorModelos = fgestorModelos;
            this.proveniente = proveniente;
            if (proveniente == "TIPOS") this.subModelo = (Modelos.CMM.Type)subModelo;
            if (proveniente == "ASPECTOS") this.subModelo = (Aspect)subModelo;
        }
        private async void FPropiedades_Load(object sender, EventArgs e)
        {
            lnklblModeloNav.Text = modelo.Name;
            lnklblSubmodeloNav.Text = subModelo.Name;
            await PoblarDtgv();
            NuevaPlantilla();
        }

        private async Task PoblarDtgv()
        {
            if (proveniente == "ASPECTOS")
                subModelo = await AspectosPersonalizadosStatic.ObtenerAspectoPersonalizado(modelo.Name, subModelo.Name);
            if (proveniente == "TIPOS")
            {

            }

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
            if (proveniente == "TIPOS") fgestorModelos.AbrirTipos(modelo);
            if (proveniente == "ASPECTOS") fgestorModelos.AbrirAspectos(modelo);
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
            cmbxTipoDato.SelectedItem = "d:text";
            cmbxRequerido.SelectedItem = "Opcional";
            cmbxRestriccion.SelectedItem = "Ninguno";
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
                PropertiesBodyUpdate propertiesBodyCreate = new PropertiesBodyUpdate(subModelo.Name, propiedadesCrear);
                if (proveniente == "ASPECTOS")
                {
                    await AspectosPersonalizadosStatic.AñadirPropiedadeAspecto(
                        modelo.Name,
                        subModelo.Name,
                        propertiesBodyCreate);
                    MessageBox.Show("Propiedad creada exitosamente");
                }
                if (proveniente == "TIPOS") fgestorModelos.AbrirAspectos(modelo);
                await PoblarDtgv();


            }
            else if (btnAceptar.Text == "Editar")
            {
                if (modelo.Status == "DRAFT")
                {
                    Property propiedadEditar = new Property();
                    propiedadEditar.Name = txtNombre.Text;
                    propiedadEditar.Description = txtDescripcion.Text;
                    propiedadEditar.Title = txtTitulo.Text;
                    propiedadEditar.Datatype = cmbxTipoDato.SelectedItem.ToString();
                    propiedadEditar.MultiValued = chkboxMultiple.Checked;
                    if (cmbxRequerido.SelectedItem.ToString() == "Opcional") propiedadEditar.Mandatory = false;
                    else { propiedadEditar.Mandatory = true; }
                    if (cmbxRequerido.SelectedItem.ToString() == "Ninguno") propiedadEditar.Constraints = null;
                    else { }
                    if (cmbxIndexacion.SelectedItem.ToString() == "Ninguno")
                    {
                        propiedadEditar.Facetable = "UNSET";
                        propiedadEditar.IndexTokenisationMode = "TRUE";
                        propiedadEditar.Indexed = false;
                        propiedadEditar.MandatoryEnforced = false;
                    }
                    List<Property> propiedades = new List<Property>();
                    propiedades.Add(propiedadEditar);
                    PropertiesBodyUpdate propertiesBodyUpdate = new PropertiesBodyUpdate(subModelo.Name, propiedades);

                    if (proveniente == "ASPECTOS")
                    {
                        await AspectosPersonalizadosStatic.ActualizarPropiedadAspecto(
                        modelo.Name,
                        subModelo.Name,
                        propiedadEditar.Name,
                        propertiesBodyUpdate);
                        MessageBox.Show("Propiedad actualizada exitosamente");
                    }
                    if (proveniente == "TIPOS") fgestorModelos.AbrirAspectos(modelo);
                    await PoblarDtgv();
                }
                else
                {
                    MessageBox.Show("Para Editar una propiedad el modelo debe estar desactivado");
                }
            }
        }

        private void tlstripCrear_Click(object sender, EventArgs e)
        {
            NuevaPlantilla();
        }

        private void tlstripEditar_Click(object sender, EventArgs e)
        {
            PlantillaEditar();
        }

        private void dtgviewDatos_MouseDown(object sender, MouseEventArgs e)
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
                }
            }
        }

        private void PlantillaEditar()
        {
            Property propiedadEditar = new Property();
            if (proveniente == "ASPECTOS")
            {
                Aspect aspectoActual = (Aspect)subModelo;
                propiedadEditar = (from propiedad in aspectoActual.Properties
                                   where propiedad.Name == dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString()
                                   select propiedad).FirstOrDefault();
            }
            else if (proveniente == "TIPOS")
            {
                Modelos.CMM.Type aspectoActual = (Modelos.CMM.Type)subModelo;
                propiedadEditar = (from propiedad in aspectoActual.Properties
                                   where propiedad.Name == dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString()
                                   select propiedad).FirstOrDefault();
            }

            flwlypanelPropiedades.Visible = true;
            btnAceptar.Text = "Editar";
            lblEstado.Text = "Editando Propiedad";
            txtNombre.Text = propiedadEditar.Name;
            txtNombre.Enabled = false;
            txtTitulo.Text = propiedadEditar.Title;
            txtDescripcion.Text = propiedadEditar.Description;
            cmbxTipoDato.SelectedIndex = cmbxTipoDato.Items.IndexOf(propiedadEditar.Datatype);
            chkboxMultiple.Checked = propiedadEditar.MultiValued;
            if (propiedadEditar.Mandatory)
                cmbxRequerido.SelectedIndex = cmbxRequerido.Items.IndexOf("Obligatorio");
            else cmbxRequerido.SelectedIndex = cmbxRequerido.Items.IndexOf("Opcional");
            if (propiedadEditar.Constraints is null)
                cmbxRestriccion.SelectedIndex = cmbxRestriccion.Items.IndexOf("Ninguno");
            else { }
            if (propiedadEditar.Indexed)
                cmbxIndexacion.SelectedIndex = cmbxIndexacion.Items.IndexOf("Ninguno");
        }

        private async void tlstripEliminar_Click(object sender, EventArgs e)
        {
            FLoading fPrincipalLoading = new FLoading();
            fPrincipalLoading.Show();
            await AspectosPersonalizadosStatic.EliminarPropiedadAspecto(
                modelo.Name,
                subModelo.Name,
                dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
            fPrincipalLoading.Close();
            MessageBox.Show("La propiedad ha sido eliminada");
            await PoblarDtgv();
            dtgviewDatos.Refresh();
        }
    }
}
