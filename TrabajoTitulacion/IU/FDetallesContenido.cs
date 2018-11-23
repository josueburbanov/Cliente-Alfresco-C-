using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos.CMM;
using TrabajoTitulacion.Modelos.Core;
using TrabajoTitulacion.Modelos.Utils;
using TrabajoTitulacion.Servicios.CMM.AspectosPersonalizados;
using TrabajoTitulacion.Servicios.CMM.ModelosPersonalizados;
using TrabajoTitulacion.Servicios.CMM.TiposPersonalizados;
using TrabajoTitulacion.Servicios.Core.Nodos;

namespace TrabajoTitulacion.IU
{
    public partial class FDetallesContenido : Form
    {
        private Nodo nodoSeleccionado;
        private bool editable = false;
        public FDetallesContenido()
        {
            InitializeComponent();
        }
        public FDetallesContenido(Nodo nodoSeleccionado)
        {
            InitializeComponent();
            this.nodoSeleccionado = nodoSeleccionado;
        }

        private async void FDetallesContenido_Load(object sender, EventArgs e)
        {
            await PresentarNodo();
        }


        private async Task PresentarNodo()
        {
            try
            {
                nodoSeleccionado = await NodosStatic.ObtenerNodoPersonalizado(nodoSeleccionado.Id);
                //Presentación de Tipo y Aspectos del Nodo
                cmbxAspectos.Items.Clear();
                cmbxTipos.Items.Clear();
                cmbxAspectos.Visible = false;
                cmbxTipos.Visible = false;
                chkboxCambiarAspecto.Checked = false;
                chkboxCambiarTipo.Checked = false;
                lblTipo.Text = nodoSeleccionado.NodeType;
                CargarListBoxAspectosPropios();


                flwypanelPropiedades.Controls.Clear();
                //Mapeo de atributos del Nodo para su visualización en el formulario
                lnklblNombre.Text = nodoSeleccionado.Name;
                lblModificado.Text = "Modificado el " +
                    nodoSeleccionado.ModifiedAt.ToShortDateString() + " por: " +
                    nodoSeleccionado.ModifiedByUser.DisplayName;

                List<Aspect> aspectosDisponibles = Aspect.Aspects();

                //Nota: [1] y [0] Depende del orden de creacion de los aspectos-propiedades por defecto
                if (!(nodoSeleccionado.Aspectos is null) || nodoSeleccionado.Aspectos.Find(x => x.Name == aspectosDisponibles[1].Name) is null)
                {
                    lblVersion.Text = "1.0";
                }
                else
                {
                    lblVersion.Text = nodoSeleccionado.Aspectos.Find(x => x.Name == aspectosDisponibles[1].Name).Properties[0].Value;
                }

                //Propiedades(Metadatos):

                //Del nodo (principales):            
                DibujarPropiedad("Nombre", nodoSeleccionado.Name, "ATRIBUTO");

                if (!(nodoSeleccionado.Content is null))
                {
                    DibujarPropiedad("Tipo MIME", nodoSeleccionado.Content.MimeType, "ATRIBUTO");
                }

                //De los aspectos del nodo:
                if (!(nodoSeleccionado.Aspectos is null))
                {
                    foreach (var aspecto in nodoSeleccionado.Aspectos)
                    {
                        if (aspecto.Showable && aspecto.PrefixedName != "sync:Sincronizable")
                        {
                            foreach (var propiedad in aspecto.Properties)
                            {
                                if (!(propiedad.Title is null))
                                {
                                    DibujarPropiedad(propiedad.Title, propiedad.Value, "ASPECTO");
                                }
                            }
                        }
                    }
                }

                //Del tipo de nodo(NodeType)
                if (!(nodoSeleccionado.TipoNodo is null))
                {
                    if (!(nodoSeleccionado.TipoNodo.Properties is null))
                    {
                        foreach (var propiedad in nodoSeleccionado.TipoNodo.Properties)
                        {
                            if (!(propiedad.Title is null))
                            {
                                DibujarPropiedad(propiedad.Title, propiedad.Value, "TIPO");
                            }
                        }
                    }
                }

            }
            catch (ModelException)
            {
                MessageBox.Show("Se ha producido un error al cargar un modelo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarListBoxAspectosPropios()
        {
            lstboxAspectos.Items.Clear();
            if (!(nodoSeleccionado.Aspectos is null))
            {
                foreach (var item in nodoSeleccionado.Aspectos)
                {
                    if (item.Showable && item.Name != "cm:author" && item.Name != "cm:titled" && item.PrefixedName != "sync:Sincronizable")
                        lstboxAspectos.Items.Add(item);
                }
            }
        }

        private async Task CargarModelos()
        {
            List<Model> modelosDisponibles = await ModelosPersonalizadosStatic.ObtenerModelosPersonalizados();

            foreach (var item in modelosDisponibles)
            {
                await CargarAspectos(item);
                await CargarTipos(item);
            }
            CargarTiposXDefecto();            
            modelosDisponibles.Clear();
        }

        private async Task CargarTipos(Model modeloSeleccionado)
        {
            List<Modelos.CMM.Type> tipos = await TiposPersonalizadosStatic.ObtenerTiposPersonalizados(modeloSeleccionado.Name);            

            if (nodoSeleccionado.IsFile)
            {
                foreach (var item in tipos)
                {
                    if ((item.PrefixedName != nodoSeleccionado.NodeType) && !(await CheckHijoFolder(item)))
                    {
                        cmbxTipos.Items.Add(item);
                    }
                }
            }
            else
            {
                foreach (var item in tipos)
                {
                    if ((item.PrefixedName != nodoSeleccionado.NodeType) && await CheckHijoFolder(item))
                    {
                        cmbxTipos.Items.Add(item);
                    }
                }
            }
            
        }

        private void CargarTiposXDefecto()
        {
                if (!nodoSeleccionado.IsFile && nodoSeleccionado.NodeType != "cm:folder")
                {
                    Modelos.CMM.Type carpetaXDefecto = new Modelos.CMM.Type()
                    {
                        Name = "cm:folder",
                        ParentName = "cm:cmobject",
                        Title = "Folder",
                        PrefixedName = "cm:folder"
                    };
                    cmbxTipos.Items.Add(carpetaXDefecto);
                }
                else if(nodoSeleccionado.NodeType != "cm:content" && nodoSeleccionado.IsFile)
                {
                    Modelos.CMM.Type archivoXDefecto = new Modelos.CMM.Type()
                    {
                        Name = "cm:content",
                        ParentName = "cm:cmobject",
                        Title = "Content",
                        PrefixedName = "cm:content"
                    };
                    cmbxTipos.Items.Add(archivoXDefecto);
                }

        }

        private async Task<bool> CheckHijoFolder(Modelos.CMM.Type item)
        {
            if (item.ParentName == "cm:folder" || item.Name == "cm:folder") return true;
            else
            {
                string prefijoModeloNodo = item.ParentName.Split(':')[0];
                string nombreTipoNodo = item.ParentName.Split(':')[1];
                Model modelo = await ModelosPersonalizadosStatic.ObtenerModeloPersonalizadoxPrefijo(prefijoModeloNodo);
                if (modelo == null) return false;
                return await CheckHijoFolder(await TiposPersonalizadosStatic.ObtenerTipoPersonalizado(modelo.Name, nombreTipoNodo));
            }
        }

        private async Task CargarAspectos(Model modeloSeleccioado)
        {
            List<Aspect> aspectos = await AspectosPersonalizadosStatic.ObtenerAspectosPersonalizados(modeloSeleccioado.Name);
            foreach (var item in aspectos)
            {
                if ((nodoSeleccionado.Aspectos.Find(x => x.PrefixedName == item.PrefixedName) is null))
                {
                    if(item.PrefixedName == "sync:Sincronizable")continue;
                    cmbxAspectos.Items.Add(item);
                }

            }
        }

        private void DibujarPropiedad(string tituloPropiedad, object valorPropiedad, string proveniente)
        {
            CtrluPropiedad ctrluPropiedad = new CtrluPropiedad();
            ctrluPropiedad.LblNombrePropiedad.Text = tituloPropiedad;
            if (!(valorPropiedad is null)) ctrluPropiedad.TxtValorPropiedad.Text = valorPropiedad.ToString();
            ctrluPropiedad.Name = tituloPropiedad;
            ctrluPropiedad.Tag = proveniente;
            flwypanelPropiedades.Controls.Add(ctrluPropiedad);
        }

        private void btnEditarPropiedades_Click(object sender, EventArgs e)
        {
            editable = true;
            foreach (var ctruPropiedad in flwypanelPropiedades.Controls)
            {
                if(((CtrluPropiedad)ctruPropiedad).LblNombrePropiedad.Text != "Tipo MIME"){
                    ((CtrluPropiedad)ctruPropiedad).TxtValorPropiedad.Enabled = true;
                }
            }
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            if (editable)
            {
                nodoSeleccionado.Name = ((CtrluPropiedad)(flwypanelPropiedades.Controls.Find("Nombre", false)[0])).TxtValorPropiedad.Text;                

                foreach (var control in flwypanelPropiedades.Controls)
                {
                    CtrluPropiedad controlPropiedad = (CtrluPropiedad)control;
                    if (controlPropiedad.Tag.Equals("ASPECTO"))
                    {
                        var propiedadBuscada = (from aspecto in nodoSeleccionado.Aspectos
                                                from propiedad in aspecto.Properties
                                                where propiedad.Title == controlPropiedad.Name
                                                select propiedad).FirstOrDefault();
                        propiedadBuscada.Value = controlPropiedad.TxtValorPropiedad.Text;
                    }
                    else if (controlPropiedad.Tag.Equals("TIPO"))
                    {
                        var propiedadBuscada = (from propiedad in nodoSeleccionado.TipoNodo.Properties
                                                where propiedad.Title == controlPropiedad.Name
                                                select propiedad).FirstOrDefault();
                        propiedadBuscada.Value = controlPropiedad.TxtValorPropiedad.Text;
                    }
                }

                await NodosStatic.ActualizarPropiedadesNodo(nodoSeleccionado);
                MessageBox.Show("Propiedades Actualizadas", "Alfresco");
                Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void chkboxCambiarTipo_CheckedChanged(object sender, EventArgs e)
        {
            cmbxTipos.Items.Clear();
            if (chkboxCambiarTipo.Checked)
            {
                cmbxTipos.Visible = true;
                btnCambiarTipo.Visible = true;
                await CargarModelos();
            }
            else
            {
                cmbxTipos.Visible = false;
                btnCambiarTipo.Visible = false;
            }

        }

        private async void chkboxCambiarAspecto_CheckedChanged(object sender, EventArgs e)
        {
            cmbxAspectos.Items.Clear();
            if (chkboxCambiarAspecto.Checked)
            {
                cmbxAspectos.Visible = true;
                btnAgregarAspecto.Visible = true;
                await CargarModelos();
            }
            else
            {
                cmbxAspectos.Visible = false;
                btnAgregarAspecto.Visible = false;
            }

        }

        private async void btnEliminarAspecto_Click(object sender, EventArgs e)
        {
            if (!(lstboxAspectos.SelectedItem is null))
            {
                DialogResult dialogResult = MessageBox.Show("Eliminar un aspecto implica perder los valores de las propiedades inherentes a tal Aspecto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.OK)
                {
                    foreach (var item in nodoSeleccionado.AspectNames)
                    {
                        Aspect aspectoEliminar = (Aspect)lstboxAspectos.SelectedItem;
                        if (item == aspectoEliminar.PrefixedName)
                        {
                            if (item == "sync:Sincronizable")
                            {
                                MessageBox.Show("El aspecto Sincronizable no se puede eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                            nodoSeleccionado.AspectNames.Remove(item);
                            MessageBox.Show("Aspecto eliminado exitosamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                    }
                    await NodosStatic.ActualizarAspectosNodo(nodoSeleccionado);
                    await PresentarNodo();
                }

            }
            lstboxAspectos.Refresh();
        }

        private async void btnCambiarTipo_Click(object sender, EventArgs e)
        {
            if (!(cmbxTipos.SelectedItem is null))
            {
                DialogResult dialogResult = MessageBox.Show("Cambiar el tipo a un nodo implica perder todas los aspectos anteriores y propiedades del tipo, y de sus contenidos en caso de ser carpeta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.OK)
                {
                    Modelos.CMM.Type nuevoTipo = (Modelos.CMM.Type)(cmbxTipos.SelectedItem);
                    nodoSeleccionado.NodeType = nuevoTipo.PrefixedName;
                    nodoSeleccionado = await NodosStatic.CambiarTipo(nodoSeleccionado);
                    await PresentarNodo();
                }
            }
        }

        private async void btnAgregarAspecto_Click(object sender, EventArgs e)
        {
            if (!(cmbxAspectos.SelectedItem is null))
            {
                Aspect aspectoSeleccionado = (Aspect)cmbxAspectos.SelectedItem;
                if (!(nodoSeleccionado.Aspectos.Find(x => x.PrefixedName != aspectoSeleccionado.PrefixedName) is null))
                {
                    nodoSeleccionado.AspectNames.Add(aspectoSeleccionado.PrefixedName);
                    await NodosStatic.ActualizarAspectosNodo(nodoSeleccionado);
                    await PresentarNodo();
                }
            }

        }
    }
}
