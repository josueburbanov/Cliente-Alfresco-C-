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
using TrabajoTitulacion.Servicios.CMM.ModelosPersonalizados;
using TrabajoTitulacion.Servicios.CMM.TiposPersonalizados;
using TrabajoTitulacion.Servicios.Core.Nodos;

namespace TrabajoTitulacion.IU
{
    public partial class FInicio : Form
    {
        public FInicio()
        {
            InitializeComponent();
        }

        private void FInicio_Load(object sender, EventArgs e)
        {
            
        }

        private void btnRepositorio_Click(object sender, EventArgs e)
        {
            var mdiParent = MdiParent as FDashboard;
            mdiParent.AbrirRepositorio();
        }

        private void btnGestorModelos_Click(object sender, EventArgs e)
        {

        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {

        }
    }
}

