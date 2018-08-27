using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos.CMM;
using TrabajoTitulacion.Modelos.Core;
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
            //Es necesario descargar el nodo de nuevo, por que el anterior no posee Propiedades
            nodoSeleccionado = await NodosServicioStatic.ObtenerNodo(nodoSeleccionado.Id);
            PresentarNodo();

        }

        private void PresentarNodo()
        {
            flwypanelPropiedades.Controls.Clear();
            //Mapeo de atributos del Nodo para su visualización en el formulario
            lnklblNombre.Text = nodoSeleccionado.Name;
            lblModificado.Text = "Modificado el " +
                nodoSeleccionado.ModifiedAt.ToShortDateString() + " por: " +
                nodoSeleccionado.ModifiedByUser.DisplayName;

            List<Aspect> aspectosDisponibles = Aspect.Aspects();

            //Nota: [1] y [0] Depende del orden de creacion de los aspectos-propiedades por defecto
            lblVersion.Text = nodoSeleccionado.Aspectos.Find(x => x.Name == aspectosDisponibles[1].Name).Properties[0].Value;

            //Propiedades(Metadatos):

            //Del nodo:            
            DibujarPropiedad("Nombre", nodoSeleccionado.Name, "ATRIBUTO");
            DibujarPropiedad("Tipo MIME", nodoSeleccionado.Content.MimeType, "ATRIBUTO");

            //De los aspectos del nodo:
            foreach (var aspecto in nodoSeleccionado.Aspectos)
            {
                if (aspecto.Showable)
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

            //Del tipo de nodo(NodeType)
            if (!(nodoSeleccionado.TipoNodo is null))
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

        private void DibujarPropiedad(string tituloPropiedad, object valorPropiedad, string proveniente)
        {
            CtrluPropiedad ctrluPropiedad = new CtrluPropiedad();
            ctrluPropiedad.LblNombrePropiedad.Text = tituloPropiedad;
            if(!(valorPropiedad is null))ctrluPropiedad.TxtValorPropiedad.Text = valorPropiedad.ToString();
            ctrluPropiedad.Name = tituloPropiedad;
            ctrluPropiedad.Tag = proveniente;
            flwypanelPropiedades.Controls.Add(ctrluPropiedad);
        }

        private void btnEditarPropiedades_Click(object sender, EventArgs e)
        {
            editable = true;
            foreach (var ctruPropiedad in flwypanelPropiedades.Controls)
            {
                ((CtrluPropiedad)ctruPropiedad).TxtValorPropiedad.Enabled = true;
            }
            

        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            if (editable)
            {
                nodoSeleccionado.Name = ((CtrluPropiedad)(flwypanelPropiedades.Controls.Find("Nombre", false)[0])).TxtValorPropiedad.Text;
                nodoSeleccionado.Content.MimeTypeName = ((CtrluPropiedad)(flwypanelPropiedades.Controls.Find("Tipo MIME", false)[0])).TxtValorPropiedad.Text;

                foreach(var control in flwypanelPropiedades.Controls)
                {
                    CtrluPropiedad controlPropiedad = (CtrluPropiedad)control;
                    if (controlPropiedad.Tag.Equals("ASPECTO"))
                    {
                        var propiedadBuscada = (from aspecto in nodoSeleccionado.Aspectos
                                        from propiedad in aspecto.Properties
                                        where propiedad.Title == controlPropiedad.Name
                                                select propiedad).FirstOrDefault();
                        propiedadBuscada.Value = controlPropiedad.TxtValorPropiedad.Text;
                    }else if(controlPropiedad.Tag.Equals("TIPO"))
                    {
                        var propiedadBuscada = (from propiedad in nodoSeleccionado.TipoNodo.Properties
                                                where propiedad.Title == controlPropiedad.Name
                                                select propiedad).FirstOrDefault();
                        propiedadBuscada.Value = controlPropiedad.TxtValorPropiedad.Text;
                    }
                }

                await NodosServicioStatic.ActualizarPropiedadesNodo(nodoSeleccionado);
                MessageBox.Show("Propiedades Actualizadas", "Alfresco");
                Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
