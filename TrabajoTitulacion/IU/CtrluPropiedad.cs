using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabajoTitulacion.IU
{
    public partial class CtrluPropiedad : UserControl
    {
        public string Tipo { get; set; }
        public CtrluPropiedad()
        {
            InitializeComponent();
            dtpValorPropiedad.Format = DateTimePickerFormat.Custom;
            dtpValorPropiedad.CustomFormat = " ";
        }

        private void dtpValorPropiedad_ValueChanged(object sender, EventArgs e)
        {
            string startDate = Convert.ToString(dtpValorPropiedad.Value);
            dtpValorPropiedad.Format = DateTimePickerFormat.Custom;
            if (Tipo == "d:date")
            {
                dtpValorPropiedad.CustomFormat = "yyyy-MM-dd";
            }
            else if (Tipo == "d:datetime")
            {
                dtpValorPropiedad.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            }

        }
    }
}
