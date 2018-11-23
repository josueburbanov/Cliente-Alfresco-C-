using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos.CMM;
using TrabajoTitulacion.Modelos.Core;
using TrabajoTitulacion.Servicios.CMM.AspectosPersonalizados;
using TrabajoTitulacion.Servicios.CMM.ModelosPersonalizados;
using TrabajoTitulacion.Servicios.CMM.TiposPersonalizados;
using TrabajoTitulacion.Servicios.Search;

namespace TrabajoTitulacion.IU
{
    public partial class FBusqueda : Form
    {
        public FBusqueda()
        {
            InitializeComponent();
        }

        private async void FBusqueda_Load(object sender, EventArgs e)
        {
            await CargarCmbxModelos();
        }

        private async Task CargarCmbxModelos()
        {
            List<Model> modelosDisponibles = await ModelosPersonalizadosStatic.ObtenerModelosPersonalizados();

            foreach (var item in modelosDisponibles)
            {
                cmbxModelo.Items.Add(item);
            }
        }

        private async void cmbxModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioBtnTipo.Checked)
            {
                await CargarTipos((Model)cmbxModelo.SelectedItem);
            }
            else if (radioBtnAspecto.Checked)
            {
                await CargarAspectos((Model)cmbxModelo.SelectedItem);
            }
            btnBuscar.Enabled = false;
            chkboxValor.Checked = false;

        }

        private async Task CargarAspectos(Model modeloSeleccioado)
        {
            List<Aspect> aspectos = await AspectosPersonalizadosStatic.ObtenerAspectosPersonalizados(modeloSeleccioado.Name);
            cmbxTipoAspecto.Items.Clear();
            cmbxPropiedad.Items.Clear();
            foreach (var item in aspectos)
            {
                cmbxTipoAspecto.Items.Add(item);
            }
        }

        private async Task CargarTipos(Model modeloSeleccionado)
        {
            List<Modelos.CMM.Type> tipos = await TiposPersonalizadosStatic.ObtenerTiposPersonalizados(modeloSeleccionado.Name);
            cmbxTipoAspecto.Items.Clear();
            cmbxPropiedad.Items.Clear();
            foreach (var item in tipos)
            {
                cmbxTipoAspecto.Items.Add(item);
            }
        }

        private async void radioBtnAspecto_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnAspecto.Checked)
            {
                await CargarAspectos((Model)cmbxModelo.SelectedItem);
            }
        }

        private async void radioBtnTipo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnTipo.Checked)
            {
                await CargarTipos((Model)cmbxModelo.SelectedItem);
            }
        }

        private void cmbxPropiedad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EstablecerTipoDato(Property propiedadSeleccionada)
        {
            switch (propiedadSeleccionada.Datatype)
            {
                case "d:boolean":
                    lblTipoDato.Text = "True o False";
                    txtValor.Visible = false;
                    dtpkrValor.Visible = false;
                    cmbxValor.Visible = true;
                    cmbxOperacion.Items.Clear();
                    cmbxOperacion.Items.AddRange(new[] { "Coincidencia exacta" });
                    cmbxOperacion.SelectedIndex = 0;
                    break;
                case "d:text":
                    lblTipoDato.Text = "Texto";
                    txtValor.Visible = true;
                    dtpkrValor.Visible = false;
                    cmbxValor.Visible = false;
                    cmbxOperacion.Items.Clear();
                    cmbxOperacion.Items.AddRange(new[] { "Coincidencia exacta", "Coincidencia + corrector ortográfico" });
                    cmbxOperacion.SelectedIndex = 0;
                    break;
                case "d:mltext":
                    lblTipoDato.Text = "Texto";
                    txtValor.Visible = true;
                    dtpkrValor.Visible = false;
                    cmbxValor.Visible = false;
                    cmbxOperacion.Items.Clear();
                    cmbxOperacion.Items.AddRange(new[] { "Coincidencia exacta", "Coincidencia + corrector ortográfico" });
                    cmbxOperacion.SelectedIndex = 0;
                    break;
                case "d:float":
                    lblTipoDato.Text = "Número decimal";
                    txtValor.Visible = true;
                    dtpkrValor.Visible = false;
                    cmbxValor.Visible = false;
                    cmbxOperacion.Items.Clear();
                    cmbxOperacion.Items.AddRange(new[] { "Igual", "Mayor que", "Menor que", "Mayor o igual que", "Menor o igual que", "Rango[]", "Rango()", "Rango[)", "Rango(]" });
                    cmbxOperacion.SelectedIndex = 0;
                    break;
                case "d:double":
                    lblTipoDato.Text = "Número decimal";
                    txtValor.Visible = true;
                    dtpkrValor.Visible = false;
                    cmbxValor.Visible = false;
                    cmbxOperacion.Items.Clear();
                    cmbxOperacion.Items.AddRange(new[] { "Igual", "Mayor que", "Menor que", "Mayor o igual que", "Menor o igual que", "Rango[]", "Rango()", "Rango[)", "Rango(]" });
                    cmbxOperacion.SelectedIndex = 0;
                    break;
                case "d:int":
                    lblTipoDato.Text = "Número Entero";
                    txtValor.Visible = true;
                    dtpkrValor.Visible = false;
                    cmbxValor.Visible = false;
                    cmbxOperacion.Items.Clear();
                    cmbxOperacion.Items.AddRange(new[] { "Igual", "Mayor que", "Menor que", "Mayor o igual que", "Menor o igual que", "Rango[]", "Rango()", "Rango[)", "Rango(]" });
                    cmbxOperacion.SelectedIndex = 0;
                    break;
                case "d:datetime":
                    lblTipoDato.Text = "Fecha";
                    txtValor.Visible = false;
                    dtpkrValor.Visible = true;
                    cmbxValor.Visible = false;
                    cmbxOperacion.Items.Clear();
                    cmbxOperacion.Items.AddRange(new[] { "Menor que", "Mayor que", "Menor o igual que", "Mayor o igual que", "Rango[]", "Rango()", "Rango[)", "Rango(]" });
                    cmbxOperacion.SelectedIndex = 0;
                    break;
                case "d:date":
                    lblTipoDato.Text = "Fecha";
                    txtValor.Visible = false;
                    dtpkrValor.Visible = true;
                    cmbxValor.Visible = false;
                    cmbxOperacion.Items.Clear();
                    cmbxOperacion.Items.AddRange(new[] { "Menor que", "Mayor que", "Menor o igual que", "Mayor o igual que", "Rango[]", "Rango()", "Rango[)", "Rango(]" });
                    cmbxOperacion.SelectedIndex = 0;
                    break;
                case "d:long":
                    lblTipoDato.Text = "Número Entero";
                    txtValor.Visible = true;
                    dtpkrValor.Visible = false;
                    cmbxValor.Visible = false;
                    cmbxOperacion.Items.Clear();
                    cmbxOperacion.Items.AddRange(new[] { "Igual", "Mayor que", "Menor que", "Mayor o igual que", "Menor o igual que", "Rango[]", "Rango()", "Rango[)", "Rango(]" });
                    cmbxOperacion.SelectedIndex = 0;
                    break;
                default:
                    break;
            }
            lblTipoDato.Visible = true;
            cmbxOperacion.Visible = true;
        }

        private async void cmbxTipoAspecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(cmbxTipoAspecto.SelectedItem is null))
            {
                await CargarPropiedades((Model)cmbxModelo.SelectedItem, cmbxTipoAspecto.SelectedItem);
            }
            btnBuscar.Enabled = true;
        }

        private async Task CargarPropiedades(Model modelo, dynamic aspectoTipo)
        {
            List<Property> propiedades = new List<Property>();
            if (radioBtnAspecto.Checked)
            {
                propiedades = (await AspectosPersonalizadosStatic.ObtenerAspectoPersonalizado(modelo.Name, aspectoTipo.Name)).Properties;
            }
            else if (radioBtnTipo.Checked)
            {
                propiedades = (await TiposPersonalizadosStatic.ObtenerTipoPersonalizado(modelo.Name, aspectoTipo.Name)).Properties;
            }
            cmbxPropiedad.Items.Clear();
            foreach (var item in propiedades)
            {
                cmbxPropiedad.Items.Add(item);
            }
        }

        private void chkboxValor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkboxValor.Checked)
            {
                EstablecerTipoDato((Property)cmbxPropiedad.SelectedItem);
            }
            else
            {
                txtValor.Visible = false;
                dtpkrValor.Visible = false;
                cmbxValor.Visible = false;
                lblTipoDato.Visible = false;
                cmbxOperacion.Visible = false;
            }
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            lblNotFound.Visible = false;
            flwlypanelNodos.Controls.Clear();

            List<Nodo> nodosEncontrados = new List<Nodo>();
            if (cmbxPropiedad.SelectedItem is null && !chkboxValor.Checked && radioBtnAspecto.Checked)
            {
                nodosEncontrados = await BusquedaStatic.BuscarNodosPorAspecto((Aspect)cmbxTipoAspecto.SelectedItem);
            }
            else if (!(cmbxPropiedad is null) && !chkboxValor.Checked)
            {
                nodosEncontrados = await BusquedaStatic.BuscarNodosPorPropiedad((Property)cmbxPropiedad.SelectedItem);
            }
            else if (!(cmbxPropiedad is null) && chkboxValor.Checked)
            {
                string valor = "";
                if (!(txtValor.Text == "") && cmbxOperacion is null)
                {
                    valor = txtValor.Text;
                }
                else if (!(dtpkrValor.Visible != true) && cmbxOperacion is null)
                {
                    DateTime valor1 = dtpkrValor.Value;
                    TimeSpan ts = new TimeSpan(23, 59, 59);
                    valor1 = valor1.Date + ts;
                    nodosEncontrados = await BusquedaStatic.BuscarNodosPor1Propiedad(cmbxTipoAspecto.SelectedItem, (Property)cmbxPropiedad.SelectedItem, valor1.ToString("s"), cmbxOperacion.SelectedItem.ToString());
                    valor = "";
                }
                else if (!(cmbxValor.SelectedItem is null) && cmbxOperacion is null)
                {
                    valor = cmbxValor.SelectedItem.ToString();
                }
                else if (!(txtValor.Text == "") && !(cmbxOperacion is null))
                {
                    if (cmbxOperacion.SelectedItem.ToString() == "Rango[]" || cmbxOperacion.SelectedItem.ToString() == "Rango()" ||
                cmbxOperacion.SelectedItem.ToString() == "Rango[)" || cmbxOperacion.SelectedItem.ToString() == "Rango(]")
                    {
                        nodosEncontrados = await BusquedaStatic.BuscarNodosPor2Propiedades(cmbxTipoAspecto.SelectedItem, (Property)cmbxPropiedad.SelectedItem, txtValor.Text, txtValor2.Text, cmbxOperacion.SelectedItem.ToString());
                    }
                    else if (cmbxOperacion.SelectedItem.ToString() == "Coincidencia exacta" || cmbxOperacion.SelectedItem.ToString() == "Coincidencia + corrector ortográfico")
                    {
                        valor = txtValor.Text;
                    }
                    else
                    {
                        nodosEncontrados = await BusquedaStatic.BuscarNodosPor1Propiedad(cmbxTipoAspecto.SelectedItem, (Property)cmbxPropiedad.SelectedItem, txtValor.Text, cmbxOperacion.SelectedItem.ToString());
                    }

                    valor = "";
                }
                else if (!(dtpkrValor.Visible != true) && !(cmbxOperacion is null))
                {
                    DateTime valor1 = dtpkrValor.Value;
                    TimeSpan ts = new TimeSpan(23, 59, 59);
                    valor1 = valor1.Date + ts;
                    DateTime valor2 = dtpkrValor2.Value;
                    TimeSpan ts2 = new TimeSpan(23, 59, 59);
                    valor1 = valor2.Date + ts2;
                    if (cmbxOperacion.SelectedItem.ToString() == "Rango[]" || cmbxOperacion.SelectedItem.ToString() == "Rango()" ||
                cmbxOperacion.SelectedItem.ToString() == "Rango[)" || cmbxOperacion.SelectedItem.ToString() == "Rango(]")
                    {
                        nodosEncontrados = await BusquedaStatic.BuscarNodosPor2Propiedades(cmbxTipoAspecto.SelectedItem, (Property)cmbxPropiedad.SelectedItem, valor1.ToString("s"), valor2.ToString("s"), cmbxOperacion.SelectedItem.ToString());
                    }
                    else
                    {
                        nodosEncontrados = await BusquedaStatic.BuscarNodosPor1Propiedad(cmbxTipoAspecto.SelectedItem, (Property)cmbxPropiedad.SelectedItem, txtValor.Text, cmbxOperacion.SelectedItem.ToString());
                    }

                    valor = "";
                }
                else if (!(cmbxValor.SelectedItem is null) && !(cmbxOperacion is null))
                {
                    valor = cmbxValor.SelectedItem.ToString();
                }



                if (valor != "")
                {
                    nodosEncontrados = await BusquedaStatic.BuscarNodosPorPropiedad((Property)cmbxPropiedad.SelectedItem, valor);
                }

            }

            //Si se ha encontrado Nodos entonces dibujarlos en el flowLayoutPanel, sino mensaje de error
            if (!(nodosEncontrados is null || nodosEncontrados.Count == 0))
            {
                foreach (var nodoEncontrado in nodosEncontrados)
                {
                    DibujarContenido(nodoEncontrado);
                }
            }
            else
            {
                lblNotFound.Visible = true;
            }
        }

        private void DibujarContenido(Nodo nodo)
        {
            CtrluContenido ctrluContenido = new CtrluContenido();
            ctrluContenido.BackColor = System.Drawing.Color.Transparent;
            ctrluContenido.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            ctrluContenido.LnklblNombre.Text = nodo.Name;
            ctrluContenido.LblModificado.Text = "Modificado el " +
                nodo.ModifiedAt.ToShortDateString() + " por: " + nodo.ModifiedByUser.DisplayName;
            //ctrluContenido.LblDescripcion.Text = nodoHijo.Properties.ToString();
            ctrluContenido.Tag = nodo;
            flwlypanelNodos.Controls.Add(ctrluContenido);
            ctrluContenido.LnklblDescargar.MouseClick += LnklblDescargar_MouseClick;
            ctrluContenido.LnklblNombre.MouseClick += LnklblNombre_MouseClick;
            ctrluContenido.LnklblPropiedades.MouseClick += LnklblPropiedades_MouseClick;
        }
        private void LnklblDescargar_MouseClick(object sender, MouseEventArgs e)
        {
            //Nota: El código de este evento también está en el evento del control de usuario
            flwlypanelNodos.Controls.Clear();
        }
        private void LnklblNombre_MouseClick(object sender, MouseEventArgs e)
        {
            FDetallesContenido fDetallesContenido = new FDetallesContenido();
            fDetallesContenido.ShowDialog();
        }
        private void LnklblPropiedades_MouseClick(object sender, MouseEventArgs e)
        {
            Nodo nodoSeleccionado = (Nodo)(((LinkLabel)sender).Parent).Tag;
            FDetallesContenido fDetallesContenido = new FDetallesContenido(nodoSeleccionado);
            fDetallesContenido.ShowDialog();
        }

        private void cmbxOperacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbxOperacion.SelectedItem.ToString() == "Rango[]" || cmbxOperacion.SelectedItem.ToString() == "Rango()" ||
                cmbxOperacion.SelectedItem.ToString() == "Rango[)" || cmbxOperacion.SelectedItem.ToString() == "Rango(]")
            {
                switch (((Property)cmbxPropiedad.SelectedItem).Datatype)
                {
                    case "d:boolean":
                        txtValor2.Visible = false;
                        dtpkrValor2.Visible = false;
                        break;
                    case "d:text":
                        txtValor2.Visible = true;
                        dtpkrValor2.Visible = false;
                        break;
                    case "d:mltext":
                        txtValor2.Visible = true;
                        dtpkrValor2.Visible = false;
                        break;
                    case "d:float":
                        txtValor2.Visible = true;
                        dtpkrValor2.Visible = false;
                        break;
                    case "d:double":
                        txtValor2.Visible = true;
                        dtpkrValor2.Visible = false;
                        break;
                    case "d:int":
                        txtValor2.Visible = true;
                        dtpkrValor2.Visible = false;
                        break;
                    case "d:datetime":
                        txtValor2.Visible = false;
                        dtpkrValor2.Visible = true;
                        break;
                    case "d:date":
                        txtValor2.Visible = false;
                        dtpkrValor2.Visible = true;
                        break;
                    case "d:long":
                        txtValor2.Visible = true;
                        dtpkrValor2.Visible = false;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                txtValor2.Visible = false;
                dtpkrValor2.Visible = false;
            }
        }
    }
}