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
using TrabajoTitulacion.Modelos.CoreAPI;
using TrabajoTitulacion.Modelos.Utils;
using TrabajoTitulacion.Servicios.CMM.ModelosPersonalizados;
using TrabajoTitulacion.Servicios.Core.Personas;
using TrabajoTitulacion.Servicios.Group;

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
            this.fgestorModelos = formPrincipal;
        }


        private async void FModelos_Load(object sender, EventArgs e)
        {
            await PoblarDtgvModelos("Sin filtro");
        }
        /// <summary>
        /// Pobla el datagridView con los modelos de usuario
        /// </summary>
        /// <returns></returns>
        private async Task PoblarDtgvModelos(string filtro)
        {
            List<Model> modelos = new List<Model>();
            if (filtro == "Autor")
            {
                modelos = await ModelosPersonalizadosStatic.ObtenerModelosPersonalizadosxAuthor();
                lblFiltradoPor.Text = "Filtrado por: Mis modelos";
            }
            else if (filtro == "Sin filtro")
            {
                modelos = await ModelosPersonalizadosStatic.ObtenerModelosPersonalizados();
                lblFiltradoPor.Text = "Filtrado por: Sin filtro";
            }
            else if (filtro == "Activos")
            {
                modelos = await ModelosPersonalizadosStatic.ObtenerModelosPersonalizados();
                List<Model> modelosActivos = new List<Model>();
                foreach (var item in modelos)
                {
                    if (item.Status == "ACTIVE")
                    {
                        modelosActivos.Add(item);
                    }
                }
                modelos = modelosActivos;
                lblFiltradoPor.Text = "Filtrado por: Activos";
            }
            else if (filtro == "Desactivados")
            {
                modelos = await ModelosPersonalizadosStatic.ObtenerModelosPersonalizados();
                List<Model> modelosActivos = new List<Model>();
                foreach (var item in modelos)
                {
                    if (item.Status != "ACTIVE")
                    {
                        modelosActivos.Add(item);
                    }
                }
                modelos = modelosActivos;
                lblFiltradoPor.Text = "Filtrado por: Desactivados";
            }

            dtgviewDatos.AutoGenerateColumns = false;
            dtgviewDatos.DataSource = modelos;
            dtgviewDatos.Columns["clmNombreModelo"].DataPropertyName = "Name";
            dtgviewDatos.Columns["clmEspacioNombres"].DataPropertyName = "NamespaceUri";
            dtgviewDatos.Columns["clmAuthor"].DataPropertyName = "Author";
            dtgviewDatos.Columns["clmEstadoModelo"].DataPropertyName = "Status";
        }

        private async void btnAceptarModelo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombreModelo.Text) || string.IsNullOrEmpty(txtEspacioModelo.Text) || string.IsNullOrEmpty(txtPrefijoModelo.Text))
            {
                MessageBox.Show("Es obligatorio llenar los campos: Nombre, Espacio de Nombres y Prefijo ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

            else
            {
                if (btnAceptarModelo.Text == "Crear")
                {
                    FLoading fPrincipalLoading = new FLoading();
                    try
                    {
                        Model modelo = new Model();
                        modelo.Name = txtNombreModelo.Text;
                        modelo.NamespaceUri = txtEspacioModelo.Text;
                        modelo.NamespacePrefix = txtPrefijoModelo.Text;
                        modelo.Description = txtDescripcionModelo.Text;
                        var mdiParent = MdiParent as FDashboard;
                        string autorModelo = PersonasStatic.PersonaAutenticada.FirstName + PersonasStatic.PersonaAutenticada.LastName;
                        modelo.Author = autorModelo;
                        modelo.Status = "DRAFT";
                        fPrincipalLoading.Show();
                        await ModelosPersonalizadosStatic.CrearModeloPersonalizado(modelo);
                        fPrincipalLoading.Close();
                        MessageBox.Show("El nuevo modelo ha sido creado con éxito");
                        await PoblarDtgvModelos("Sin filtro");
                        dtgviewDatos.Refresh();
                        panelModelo.Visible = false;
                    }
                    catch (ModelException exception)
                    {
                        if (exception.Codigo == 409)
                        {
                            fPrincipalLoading.Close();
                            MessageBox.Show("Uno de los nombres especificados ya se está utlizando", "Nombres duplicados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else if (btnAceptarModelo.Text == "Editar")
                {
                    FLoading fPrincipalLoading = new FLoading();
                    try
                    {
                        Model modelo = new Model();
                        modelo.Name = txtNombreModelo.Text;
                        modelo.NamespaceUri = txtEspacioModelo.Text;
                        modelo.NamespacePrefix = txtPrefijoModelo.Text;
                        modelo.Description = txtDescripcionModelo.Text;
                        var mdiParent = MdiParent as FDashboard;
                        fPrincipalLoading.Show();
                        await ModelosPersonalizadosStatic.ActualizarModeloPersonalizado(modelo);
                        fPrincipalLoading.Close();
                        MessageBox.Show("El modelo " + modelo.Name + " ha sido actualizado con éxito", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await PoblarDtgvModelos("Sin filtro");
                        dtgviewDatos.Refresh();
                        panelModelo.Visible = false;
                        PlantillaNuevoModelo();
                    }
                    catch (ModelException exception)
                    {
                        if (exception.Codigo == 409)
                        {
                            fPrincipalLoading.Close();
                            MessageBox.Show("Uno de los nombres especificados ya se está utlizando", "Nombres duplicados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
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

        private async void tlstripCrearModelo_Click(object sender, EventArgs e)
        {
            List<GroupMember> groupMembers = await GruposStatic.ObtenerMiembrosGrupoAdministradorModelos();
            if (!(groupMembers.Find(x => x.Id == PersonasStatic.PersonaAutenticada.Id) is null))
            {
                PlantillaNuevoModelo();
            }
            else
            {
                MessageBox.Show("No tiene los permisos suficientes para realizar esta acción. Usted no pertenece al grupo ALFRESCO_MODEL_ADMINISTRATORS.", "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void tlstripEditar_Click(object sender, EventArgs e)
        {
            List<GroupMember> groupMembers = await GruposStatic.ObtenerMiembrosGrupoAdministradorModelos();
            if (!(groupMembers.Find(x => x.Id == PersonasStatic.PersonaAutenticada.Id) is null))
            {
                PlantillaEditarModelo();
            }
            else
            {
                MessageBox.Show("No tiene los permisos suficientes para realizar esta acción. Usted no pertenece al grupo ALFRESCO_MODEL_ADMINISTRATORS.", "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void PlantillaEditarModelo()
        {
            Model modeloEditar = await ModelosPersonalizadosStatic.ObtenerModeloPersonalizado(
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
            FLoading fPrincipalLoading = new FLoading();
            try
            {
                Model modeloCambiarEstado = await ModelosPersonalizadosStatic.ObtenerModeloPersonalizado(
                dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
                fPrincipalLoading.Show();
                await ModelosPersonalizadosStatic.CambiarEstadoModeloPersonalizado(modeloCambiarEstado);
                fPrincipalLoading.Close();
                MessageBox.Show("El modelo " + modeloCambiarEstado.Name + " ha sido cambiado de estado", "Cambio de estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await PoblarDtgvModelos("Sin filtro");
                dtgviewDatos.Refresh();
            }
            catch (ModelException exception)
            {
                if (exception.Codigo == 409)
                {
                    fPrincipalLoading.Close();
                    MessageBox.Show("No se puede cambiar el estado. El modelo se está utilizando, o un tipo o aspecto de este modelo es padre de algún tipo o aspecto que se está utilizando", "Conflicto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void tlstripEliminar_Click(object sender, EventArgs e)
        {
            FLoading fPrincipalLoading = new FLoading();
            try
            {
                List<GroupMember> groupMembers = await GruposStatic.ObtenerMiembrosGrupoAdministradorModelos();
                if (!(groupMembers.Find(x => x.Id == PersonasStatic.PersonaAutenticada.Id) is null))
                {
                    fPrincipalLoading.Show();
                    await ModelosPersonalizadosStatic.EliminarModeloPersonalizado(
                        dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
                    fPrincipalLoading.Close();
                    MessageBox.Show("El modelo ha sido eliminado exitosamente", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await PoblarDtgvModelos("Sin filtro");
                    dtgviewDatos.Refresh();
                }
                else
                {
                    MessageBox.Show("No tiene los permisos suficientes para realizar esta acción. Usted no pertenece al grupo ALFRESCO_MODEL_ADMINISTRATORS.", "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }catch(ModelException exception)
            {
                fPrincipalLoading.Close();
                if (exception.Codigo == 409)
                {
                    MessageBox.Show("El modelo debe estar inactivo para poder eliminarse", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Transacción abortada, hubo un error.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void tlstripTiposPersonalizados_Click(object sender, EventArgs e)
        {
            Model modeloSeleccionado = await ModelosPersonalizadosStatic.ObtenerModeloPersonalizado(
                dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
            fgestorModelos.AbrirTipos(modeloSeleccionado);
        }

        private void btnVolverInicio_Click(object sender, EventArgs e)
        {
            fgestorModelos.VolverInicio();
        }

        private async void dtgviewModelos_DoubleClick(object sender, EventArgs e)
        {
            Model modeloSeleccionado = await ModelosPersonalizadosStatic.ObtenerModeloPersonalizado(
                dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
            fgestorModelos.AbrirTipos(modeloSeleccionado);
        }

        private async void aspectosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Model modeloSeleccionado = await ModelosPersonalizadosStatic.ObtenerModeloPersonalizado(
                dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
            fgestorModelos.AbrirAspectos(modeloSeleccionado);
        }

        private void txtNombreModelo_TextChanged(object sender, EventArgs e)
        {

        }

        private async void tlstripCrearAspecto_Click(object sender, EventArgs e)
        {
            List<GroupMember> groupMembers = await GruposStatic.ObtenerMiembrosGrupoAdministradorModelos();
            if (!(groupMembers.Find(x => x.Id == PersonasStatic.PersonaAutenticada.Id) is null))
            {
                PlantillaNuevoModelo();
            }
            else
            {
                MessageBox.Show("No tiene los permisos suficientes para realizar esta acción. Usted no pertenece al grupo ALFRESCO_MODEL_ADMINISTRATORS.", "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void autorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await PoblarDtgvModelos("Autor");
        }

        private async void sinFiltrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await PoblarDtgvModelos("Sin filtro");
        }

        private async void activosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await PoblarDtgvModelos("Activos");
        }

        private async void desactivadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await PoblarDtgvModelos("Desactivados");
        }

        private void txtNombreModelo_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtPrefijoModelo_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtEspacioModelo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //permite el ingreso solo de numeros, letras mayusculas y minusculas, tecla de borrar , signo - y _ además el punto '.'
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar == 8) || e.KeyChar == '-' || e.KeyChar == '_' || e.KeyChar == '.')
                e.Handled = false;
            else
            {
                MessageBox.Show("Utilice números, letras y caracteres URI solamente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void ascendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtgviewDatos.Sort(dtgviewDatos.Columns["clmNombreModelo"], ListSortDirection.Ascending);
        }

        private void descendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtgviewDatos.Sort(dtgviewDatos.Columns["clmNombreModelo"], ListSortDirection.Descending);
        }
    }
}
