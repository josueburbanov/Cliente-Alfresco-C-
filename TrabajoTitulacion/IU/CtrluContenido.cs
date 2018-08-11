using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos;
using TrabajoTitulacion.Servicios.Core.Nodos;

namespace TrabajoTitulacion.IU
{
    public partial class CtrluContenido : UserControl
    {
        public CtrluContenido()
        {
            InitializeComponent();
        }


        private void lnklblDescargar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var path = abrirFldbrswrSeleccionarPath();
            if(path != null)
            {
                NodosServicioStatic.ObtenerContenido(((Node)Tag).Id, path+"\\"+((Node)Tag).Name);
            }
        }

        private string abrirFldbrswrSeleccionarPath()
        {
            DialogResult result = fldbrwsrSeleccionarPath.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fldbrwsrSeleccionarPath.SelectedPath))
            {
                return fldbrwsrSeleccionarPath.SelectedPath;
                
            }
            return null;
        }

        private void lnklblPropiedades_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void lnklblNombre_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
