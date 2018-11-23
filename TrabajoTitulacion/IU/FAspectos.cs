using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos.CMM;
using TrabajoTitulacion.Modelos.CoreAPI;
using TrabajoTitulacion.Modelos.Utils;
using TrabajoTitulacion.Servicios.CMM.AspectosPersonalizados;
using TrabajoTitulacion.Servicios.Core.Personas;
using TrabajoTitulacion.Servicios.Group;

namespace TrabajoTitulacion.IU
{
    public partial class FAspectos : Form
    {
        private Model modelo;
        private FGestorModelos fgestorModelos;
        public FAspectos()
        {
            InitializeComponent();
        }
        public FAspectos(FGestorModelos fgestorModelos, Model modelo)
        {
            InitializeComponent();
            this.modelo = modelo;
            this.fgestorModelos = fgestorModelos;
        }

        private async void FAspectos_Load(object sender, EventArgs e)
        {
            lnklblActualNav.Text = modelo.Name;
            await PoblarDtgv();
        }
        private async Task PoblarDtgv()
        {
            List<Aspect> aspectos = await AspectosPersonalizadosStatic.ObtenerAspectosPersonalizados(modelo.Name);
            dtgviewDatos.AutoGenerateColumns = false;
            dtgviewDatos.DataSource = aspectos;
            dtgviewDatos.Columns["clmNombreTipo"].DataPropertyName = "Name";
            dtgviewDatos.Columns["clmEtiquetaPresentacionTipo"].DataPropertyName = "Title";
            dtgviewDatos.Columns["clmPadreTipo"].DataPropertyName = "ParentName";
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
            btnAceptar.Text = "Crear";
            lblEstado.Text = "Creando";
        }

        private async Task cargarCmbxPadres()
        {
            List<Aspect> aspectosActivos = await AspectosPersonalizadosStatic.ObtenerAspectosActivos();
            foreach (var aspecto in aspectosActivos)
            {
                cmbxPadre.Items.Add(aspecto.PrefixedName);
            }
            cmbxPadre.Items.Add("gd2:editingInGoogle");
            cmbxPadre.Items.Add("gd2:sharedInGoogle");
        }

        private async void tlstripCrear_Click(object sender, EventArgs e)
        {
            List<GroupMember> groupMembers = await GruposStatic.ObtenerMiembrosGrupoAdministradorModelos();
            if (!(groupMembers.Find(x => x.Id == PersonasStatic.PersonaAutenticada.Id) is null))
            {
                NuevaPlantilla();
            }
            else
            {
                MessageBox.Show("No tiene los permisos suficientes para realizar esta acción. Usted no pertenece al grupo ALFRESCO_MODEL_ADMINISTRATORS.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
                    dtgviewDatos.ContextMenuStrip = cntxMenuGeneral;
                }
            }
        }

        private async void tlstripEditar_Click_1(object sender, EventArgs e)
        {
            List<GroupMember> groupMembers = await GruposStatic.ObtenerMiembrosGrupoAdministradorModelos();
            if (!(groupMembers.Find(x => x.Id == PersonasStatic.PersonaAutenticada.Id) is null))
            {
                PlantillaEditar();
            }
            else
            {
                MessageBox.Show("No tiene los permisos suficientes para realizar esta acción. Usted no pertenece al grupo ALFRESCO_MODEL_ADMINISTRATORS.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void PlantillaEditar()
        {
            Aspect aspectoEditar = await AspectosPersonalizadosStatic.ObtenerAspectoPersonalizado(
                modelo.Name, dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
            panelTipo.Visible = true;
            btnAceptar.Text = "Editar";
            lblEstado.Text = "Editando Tipo";
            txtNombre.Text = aspectoEditar.Name;
            txtNombre.Enabled = false;
            await cargarCmbxPadres();
            cmbxPadre.SelectedIndex = cmbxPadre.Items.IndexOf(aspectoEditar.ParentName);
            txtTitulo.Text = aspectoEditar.Title;
            txtDescripcion.Text = aspectoEditar.Description;
        }

        private async void tlstripEliminar_Click(object sender, EventArgs e)
        {
            List<GroupMember> groupMembers = await GruposStatic.ObtenerMiembrosGrupoAdministradorModelos();
            if (!(groupMembers.Find(x => x.Id == PersonasStatic.PersonaAutenticada.Id) is null))
            {
                FLoading fPrincipalLoading = new FLoading();
                fPrincipalLoading.Show();
                await AspectosPersonalizadosStatic.EliminarAspectoPersonalizado(modelo.Name,
                    dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
                fPrincipalLoading.Close();
                MessageBox.Show("El aspecto ha sido eliminado");
                await PoblarDtgv();
                dtgviewDatos.Refresh();
            }
            else
            {
                MessageBox.Show("No tiene los permisos suficientes para realizar esta acción. Usted no pertenece al grupo ALFRESCO_MODEL_ADMINISTRATORS.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void lnklblVolverNav_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fgestorModelos.AbrirModelos();
        }

        private async void tlstripCrearAspecto_Click(object sender, EventArgs e)
        {
            List<GroupMember> groupMembers = await GruposStatic.ObtenerMiembrosGrupoAdministradorModelos();
            if (!(groupMembers.Find(x => x.Id == PersonasStatic.PersonaAutenticada.Id) is null))
            {
                NuevaPlantilla();
            }
            else
            {
                MessageBox.Show("No tiene los permisos suficientes para realizar esta acción. Usted no pertenece al grupo ALFRESCO_MODEL_ADMINISTRATORS.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || cmbxPadre.SelectedItem == null)
            {
                MessageBox.Show("Es obligatorio llenar el campo: Nombre y Seleccionar una opción en Padre", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                FLoading fPrincipalLoading = new FLoading();
                try
                {
                    if (btnAceptar.Text == "Crear")
                    {
                        Aspect aspectoCrear = new Aspect();
                        aspectoCrear.Name = txtNombre.Text;
                        aspectoCrear.ParentName = cmbxPadre.SelectedItem.ToString();
                        aspectoCrear.Description = txtDescripcion.Text;
                        aspectoCrear.Title = txtTitulo.Text;
                        aspectoCrear.ModeloPerteneciente.Name = modelo.Name;
                        await AspectosPersonalizadosStatic.CrearAspectoPersonalizado(aspectoCrear);
                        MessageBox.Show("Aspecto creado exitosamente");
                        await PoblarDtgv();
                    }
                    else if (btnAceptar.Text == "Editar")
                    {
                        Aspect aspectoActualizar = new Aspect();
                        aspectoActualizar.Name = txtNombre.Text;
                        aspectoActualizar.ParentName = cmbxPadre.SelectedItem.ToString();
                        aspectoActualizar.Description = txtDescripcion.Text;
                        aspectoActualizar.Title = txtTitulo.Text;
                        aspectoActualizar.ModeloPerteneciente.Name = modelo.Name;
                        await AspectosPersonalizadosStatic.ActualizarAspectoPersonalizado(aspectoActualizar);
                        MessageBox.Show("Aspecto actualizado exitosamente");
                        await PoblarDtgv();
                        NuevaPlantilla();
                    }
                }
                catch (AspectException exception)
                {
                    if (exception.Codigo == 409)
                    {
                        MessageBox.Show("Uno de los nombres especificados ya se está utlizando", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fPrincipalLoading.Close();
                    }
                    else if (exception.Codigo == 403)
                    {
                        MessageBox.Show("Permisos insuficientes", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fPrincipalLoading.Close();
                    }
                }
            }
        }

        private async void tlstripPropiedades_Click(object sender, EventArgs e)
        {
            Aspect aspectoSeleccionado = await AspectosPersonalizadosStatic.ObtenerAspectoPersonalizado(
                modelo.Name, dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
            fgestorModelos.AbrirPropiedades(modelo, aspectoSeleccionado, "ASPECTOS");
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            //permite el ingreso solo de numeros, letras mayusculas y minusculas, tecla de borrar , signo - y _
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar == 8) || e.KeyChar == '-' || e.KeyChar == '_')
                e.Handled = false;
            else
            {
                MessageBox.Show("Utilice números, letras, guiones (-) y guiones bajos (_) solamente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}
