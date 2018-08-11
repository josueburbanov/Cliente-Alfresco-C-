using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos.CMM;
using TrabajoTitulacion.Modelos.Core;
using TrabajoTitulacion.Servicios.Core.Nodos;

namespace TrabajoTitulacion.IU
{
    public partial class FDetallesContenido : Form
    {
        private Nodo nodoSeleccionado;
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
            NodosServicioStatic.ActualizarNodo(nodoSeleccionado);

            //Mapeo de atributos del Nodo para su visualización en el formulario
            lnklblNombre.Text = nodoSeleccionado.Name;
            lblModificado.Text = "Modificado el " + 
                nodoSeleccionado.ModifiedAt.ToShortDateString() + " por: " +
                nodoSeleccionado.ModifiedByUser.DisplayName;
            List<Aspect> aspectosDisponibles = Aspect.Aspects();

            //Nota: [1] y [0] Depende del orden de creacion de los aspectos-propiedades por defecto
            lblVersion.Text = nodoSeleccionado.Aspectos.Find(x => x.Name == aspectosDisponibles[1].Name).Properties[0].Value;

            //Propiedades del Nodo (Metadatos)

            //Del nodo:            
            DibujarPropiedad("Nombre: ", nodoSeleccionado.Name);            
            DibujarPropiedad("Tipo MIME: ",nodoSeleccionado.Content.MimeType);

            //De los aspectos del nodo:
            foreach (var aspecto in nodoSeleccionado.Aspectos)
            {
                if (aspecto.Showable)
                {
                    foreach (var propiedad in aspecto.Properties)
                    {
                        if (!(propiedad.Title is null))
                        {
                            DibujarPropiedad(propiedad.Title, propiedad.Value);
                        }
                    }
                }
                
            }

            //Del tipo de nodo(NodeType)
            if (!(nodoSeleccionado.NodeType == "cm:content"))
            {
                foreach (var propiedad in nodoSeleccionado.TipoNodo.Properties)
                {
                    if (!(propiedad.Title is null))
                    {
                        DibujarPropiedad(propiedad.Title, propiedad.Value);
                    }
                }
            }


        }

        private void DibujarPropiedad(string tituloPropiedad, string valorPropiedad)
        {
            CtrluPropiedad ctrluPropiedad = new CtrluPropiedad();
            ctrluPropiedad.LblNombrePropiedad.Text = tituloPropiedad;
            ctrluPropiedad.TxtValorPropiedad.Text = valorPropiedad;
            flwypanelPropiedades.Controls.Add(ctrluPropiedad);
        }
    }
}
