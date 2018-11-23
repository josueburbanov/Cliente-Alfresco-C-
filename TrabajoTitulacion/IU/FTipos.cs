using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos.CMM;
using TrabajoTitulacion.Modelos.CoreAPI;
using TrabajoTitulacion.Modelos.Utils;
using TrabajoTitulacion.Servicios.CMM.TiposPersonalizados;
using TrabajoTitulacion.Servicios.Core.Personas;
using TrabajoTitulacion.Servicios.Group;

namespace TrabajoTitulacion.IU
{
    public partial class FTipos : Form
    {
        private Model modelo;
        private FGestorModelos fgestorModelos;
        public FTipos()
        {
            InitializeComponent();
        }
        public FTipos(FGestorModelos fgestorModelos, Model modelo)
        {
            InitializeComponent();
            this.modelo = modelo;
            this.fgestorModelos = fgestorModelos;
        }

        private async void FTipos_Load(object sender, EventArgs e)
        {
            btnModeloNav.Text = modelo.Name;
            btnModeloNav.AutoSize = true;
            await PoblarDtgv();
        }
        private async Task PoblarDtgv()
        {
            List<Modelos.CMM.Type> tipos = await TiposPersonalizadosStatic.ObtenerTiposPersonalizados(modelo.Name);
            dtgviewDatos.AutoGenerateColumns = false;
            dtgviewDatos.DataSource = tipos;
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
            List<Modelos.CMM.Type> tiposActivos = await TiposPersonalizadosStatic.ObtenerTiposActivos();
            foreach (var tipo in tiposActivos)
            {
                cmbxPadre.Items.Add(tipo.PrefixedName);
            }
            cmbxPadre.Items.Add("cm:content");
            cmbxPadre.Items.Add("cm:folder");
        }

        private void btnModeloNav_Click(object sender, EventArgs e)
        {

        }

        private async void btnAceptarTipo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || cmbxPadre.SelectedItem == null)//////
            {
                MessageBox.Show("Es obligatorio llenar el campo: Nombre y Seleccionar una opción en Padre", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {
                FLoading fPrincipalLoading = new FLoading();
                try
                {
                    if (btnAceptarTipo.Text == "Crear")
                    {
                        Modelos.CMM.Type tipoCrear = new Modelos.CMM.Type();
                        tipoCrear.Name = txtNombre.Text;
                        tipoCrear.ParentName = cmbxPadre.SelectedItem.ToString();
                        tipoCrear.Description = txtDescripcion.Text;
                        tipoCrear.Title = txtTitulo.Text;
                        tipoCrear.ModeloPerteneciente.Name = modelo.Name;
                        await TiposPersonalizadosStatic.CrearTipoPersonalizado(tipoCrear);
                        MessageBox.Show("El tipo ha sido creado exitosamente.", "Creado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await PoblarDtgv();
                    }
                    else if (btnAceptarTipo.Text == "Editar")
                    {
                        Modelos.CMM.Type tipoActualizar = new Modelos.CMM.Type();
                        tipoActualizar.Name = txtNombre.Text;
                        tipoActualizar.ParentName = cmbxPadre.SelectedItem.ToString();
                        tipoActualizar.Description = txtDescripcion.Text;
                        tipoActualizar.Title = txtTitulo.Text;
                        tipoActualizar.ModeloPerteneciente.Name = modelo.Name;
                        await TiposPersonalizadosStatic.ActualizarTipoPersonalizado(tipoActualizar);
                        MessageBox.Show("El tipo ha sido actualizado exitosamente.", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await PoblarDtgv();
                        NuevaPlantilla();
                    }
                }
                catch (TypeException exception)
                {
                    if (exception.Codigo == 409)
                    {
                        MessageBox.Show("Uno de los nombres especificados ya se está utlizando", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fPrincipalLoading.Close();
                    }else if(exception.Codigo == 403)
                    {
                        MessageBox.Show("Permisos insuficientes", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fPrincipalLoading.Close();
                    }
                }
            }
        }

        private async void tlstripCrearTipo_Click(object sender, EventArgs e)
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

        private async void tlstripEditar_Click(object sender, EventArgs e)
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
            Modelos.CMM.Type tipoEditar = await TiposPersonalizadosStatic.ObtenerTipoPersonalizado(
                modelo.Name, dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
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
            List<GroupMember> groupMembers = await GruposStatic.ObtenerMiembrosGrupoAdministradorModelos();
            if (!(groupMembers.Find(x => x.Id == PersonasStatic.PersonaAutenticada.Id) is null))
            {
                FLoading fPrincipalLoading = new FLoading();
                fPrincipalLoading.Show();
                await TiposPersonalizadosStatic.EliminarTipoPersonalizado(modelo.Name,
                    dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
                fPrincipalLoading.Close();
                MessageBox.Show("El tipo ha sido eliminado exitosamente.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await PoblarDtgv();
                dtgviewDatos.Refresh();
            }
            else
            {
                MessageBox.Show("No tiene los permisos suficientes para realizar esta acción. Usted no pertenece al grupo ALFRESCO_MODEL_ADMINISTRATORS.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        private async void tlstripPropiedades_Click(object sender, EventArgs e)
        {
            Modelos.CMM.Type tipoSeleccionado = await TiposPersonalizadosStatic.ObtenerTipoPersonalizado(
               modelo.Name, dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
            fgestorModelos.AbrirPropiedades(modelo, tipoSeleccionado, "TIPOS");
        }
    }
}
