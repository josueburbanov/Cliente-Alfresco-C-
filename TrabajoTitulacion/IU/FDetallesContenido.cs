using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos;

namespace TrabajoTitulacion.IU
{
    public partial class FDetallesContenido : Form
    {
        private Node nodoSeleccionado;
        public FDetallesContenido()
        {
            InitializeComponent();
        }
        public FDetallesContenido(Node nodoSeleccionado)
        {
            InitializeComponent();
            this.nodoSeleccionado = nodoSeleccionado;
        }

        private void FDetallesContenido_Load(object sender, EventArgs e)
        {
            lnklblNombre.Text = nodoSeleccionado.Name;
            lblModificado.Text = "Modificado el " + 
                nodoSeleccionado.ModifiedAt.ToShortDateString() + " por: " +
                nodoSeleccionado.ModifiedByUser.DisplayName;
            //lblVersion.Text = nodoSeleccionado.version?

            //dynamic propiedades = nodoSeleccionado.Properties.Title;
            
        }


    }
}
