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

namespace TrabajoTitulacion.IU
{
    public partial class FGestorModelos : Form
    {
        public FGestorModelos()
        {
            InitializeComponent();
        }

        private void FGestorModelos_Load(object sender, EventArgs e)
        {
            AbrirModelos();
        }

        public void AbrirModelos()
        {
            if (tblpanelContenedor.Controls.Count > 0)
                tblpanelContenedor.Controls.Clear();
            FModelos fModelos = new FModelos(this);
            fModelos.TopLevel = false;
            fModelos.Dock = DockStyle.Fill;
            fModelos.Visible = true;
            tblpanelContenedor.Controls.Add(fModelos);
        }
        public void AbrirTipos(Model modelo)
        {
            if (tblpanelContenedor.Controls.Count > 0)
                tblpanelContenedor.Controls.Clear();
            FTipos ftipos = new FTipos(this, modelo)
            {
                TopLevel = false,
                Dock = DockStyle.Fill,
                Visible = true
            };
            tblpanelContenedor.Controls.Add(ftipos);
        }
        public void AbrirAspectos(Model modelo)
        {
            if (tblpanelContenedor.Controls.Count > 0)
                tblpanelContenedor.Controls.Clear();
            FAspectos faspectos = new FAspectos(this, modelo)
            {
                TopLevel = false,
                Dock = DockStyle.Fill,
                Visible = true
            };
            tblpanelContenedor.Controls.Add(faspectos);
        }
        public void VolverInicio()
        {
            var mdiParent = MdiParent as FDashboard;
            mdiParent.AbrirInicio();
        }

        internal void AbrirPropiedades(Model modelo, object submodelo, string proveniente)
        {
            if (tblpanelContenedor.Controls.Count > 0)
                tblpanelContenedor.Controls.Clear();
            FPropiedades fpropiedades = new FPropiedades(this, modelo, submodelo, proveniente)
            {
                TopLevel = false,
                Dock = DockStyle.Fill,
                Visible = true
            };
            tblpanelContenedor.Controls.Add(fpropiedades);

        }
    }
}
