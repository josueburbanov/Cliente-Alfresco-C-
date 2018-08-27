using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos;
using TrabajoTitulacion.Modelos.Core;
using TrabajoTitulacion.Servicios.Core.Nodos;

namespace TrabajoTitulacion.IU
{
    public partial class FRepositorio : Form
    {
        private Nodo nodoRoot;
        private List<Nodo> nodosDeRoot;
        

        public FRepositorio()
        {
            InitializeComponent();
        }

        private async void FRepositorio_Load(object sender, EventArgs e)
        {
                       
            try
            {
                FLoading fPrincipalLoading = new FLoading();
                //Se obtiene la lista de los nodos hijos de root (1er nivel) y el nodo root
                nodosDeRoot = await NodosServicioStatic.ObtenerListaNodosHijos("-root-");
                nodoRoot = await NodosServicioStatic.ObtenerNodo("-root-");
                nodoRoot.NodosHijos = nodosDeRoot;

                fPrincipalLoading.Show();
                //Se pobla recursivamente todos los nodos
                await NodosServicioStatic.PoblarNodosHijos(nodosDeRoot);
                

                //Se agrega los nodos al treeview                
                treeViewRepositorio.Nodes.Add(nodoRoot.Id, "Mis archivos");                
                treeViewRepositorio.Nodes[nodoRoot.Id].Tag = nodoRoot;
                AñadirNodosTV(nodosDeRoot, treeViewRepositorio.Nodes[nodoRoot.Id]);
                
                treeViewRepositorio.Refresh();
                fPrincipalLoading.Close();

            }
            catch (UnauthorizedAccessException)
            {                
                MessageBox.Show("Hubo un problema cargando sus archivos.");
            }

        }


        private void AñadirNodosTV(List<Nodo> nodosPadres, TreeNode nodoTvAbuelo)
        {
            foreach (var nodoPadre in nodosPadres)
            {
                if (nodoPadre.IsFolder)
                {
                    nodoTvAbuelo.Nodes.Add(nodoPadre.Id, nodoPadre.Name);
                    TreeNode nodoTvPadre = nodoTvAbuelo.Nodes[nodoPadre.Id];
                    nodoTvPadre.Tag = nodoPadre;
                    if (!(nodoPadre.NodosHijos is null))
                        AñadirNodosTV(nodoPadre.NodosHijos, nodoTvPadre);
                }
            }
        }

        private void treeViewRepositorio_Click(object sender, EventArgs e)
        {

        }

        private void LnklblDescargar_MouseClick(object sender, MouseEventArgs e)
        {
            //Nota: El código de este evento también está en el evento del control de usuario
            flwlypanelNodosHijos.Controls.Clear();
        }

        private void treeViewRepositorio_AfterSelect(object sender, TreeViewEventArgs e)
        {
            flwlypanelNodosHijos.Controls.Clear();
            flwlypanelNavegacion.Controls.Clear();
            Nodo nodoSeleccionado = (Nodo)treeViewRepositorio.SelectedNode.Tag;

            //Si el nodo no tiene hijos muestra una imagen (arrastre y suelte archivos)
            if (nodoSeleccionado.NodosHijos.Count == 0)
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRepositorio));
                flwlypanelNodosHijos.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("flwlypanelNodosHijos.BackgroundImage")));
            }
            else
            {
                flwlypanelNodosHijos.BackgroundImage = null;

                //Itera cada nodo hijo del nodo seleccionado y lo coloca el panel derecho, con detalles
                foreach (var nodoHijo in nodoSeleccionado.NodosHijos)
                {
                    DibujarContenido(nodoHijo);
                }
            }

            //Coloca en la barra de navegación el path del nodo
            if (!(treeViewRepositorio.SelectedNode.Parent is null))
            {
                TreeNode nodoPadreTV = treeViewRepositorio.SelectedNode.Parent;
                Nodo nodoPadre = (Nodo)nodoPadreTV.Tag;
                Nodo nodoPadreAux = nodoPadre;

                while (!nodoPadreAux.Equals(nodoRoot))
                {
                    nodoPadreTV = nodoPadreTV.Parent;
                    nodoPadreAux = (Nodo)nodoPadreTV.Tag;
                    DibujarLinkNavegacion(nodoPadreAux.Name);
                    DibujarSeparador();
                }
                DibujarLinkNavegacion(nodoPadre.Name);
                DibujarSeparador();
            }
            if (nodoSeleccionado.IsFolder) DibujarLinkNavegacion(nodoSeleccionado.Name);
        }

        private void DibujarLinkNavegacion(string nombrePadre)
        {
            LinkLabel lnklblNodoHijo = new LinkLabel();
            lnklblNodoHijo.ActiveLinkColor = System.Drawing.Color.Green;
            lnklblNodoHijo.AutoSize = true;
            lnklblNodoHijo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            lnklblNodoHijo.ForeColor = System.Drawing.Color.Black;
            lnklblNodoHijo.LinkColor = System.Drawing.Color.Black;
            lnklblNodoHijo.Padding = new Padding(0, 5, 0, 5);
            lnklblNodoHijo.Anchor = AnchorStyles.Bottom;
            lnklblNodoHijo.Name = "lnklblNodoHijo";
            lnklblNodoHijo.Text = nombrePadre;
            lnklblNodoHijo.LinkBehavior = LinkBehavior.NeverUnderline;
            flwlypanelNavegacion.Controls.Add(lnklblNodoHijo);
        }

        private void DibujarContenido(Nodo nodoHijo)
        {
            CtrluContenido ctrluContenido = new CtrluContenido();
            ctrluContenido.BackColor = System.Drawing.Color.Transparent;
            ctrluContenido.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            ctrluContenido.LnklblNombre.Text = nodoHijo.Name;
            ctrluContenido.LblModificado.Text = "Modificado el " +
                nodoHijo.ModifiedAt.ToShortDateString() + " por: " + nodoHijo.ModifiedByUser.DisplayName;
            //ctrluContenido.LblDescripcion.Text = nodoHijo.Properties.ToString();
            ctrluContenido.Tag = nodoHijo;
            flwlypanelNodosHijos.Controls.Add(ctrluContenido);
            ctrluContenido.LnklblDescargar.MouseClick += LnklblDescargar_MouseClick;
            ctrluContenido.LnklblNombre.MouseClick += LnklblNombre_MouseClick;
            ctrluContenido.LnklblPropiedades.MouseClick += LnklblPropiedades_MouseClick;
        }

        /// <summary>
        /// El evento genera la apertura de un formulario donde se muestran las propiedades del Nodo
        /// </summary>
        private void LnklblPropiedades_MouseClick(object sender, MouseEventArgs e)
        {
            Nodo nodoSeleccionado = (Nodo)(((LinkLabel)sender).Parent).Tag;
            FDetallesContenido fDetallesContenido = new FDetallesContenido(nodoSeleccionado);
            fDetallesContenido.ShowDialog();
        }

        private void DibujarSeparador()
        {
            Label lblSeparador = new Label();
            lblSeparador.Text = ">";
            lblSeparador.AutoSize = true;
            lblSeparador.ForeColor = System.Drawing.Color.Black;
            lblSeparador.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblSeparador.Padding = new Padding(0, 5, 0, 5);
            flwlypanelNavegacion.Controls.Add(lblSeparador);
        }

        private void LnklblNombre_MouseClick(object sender, MouseEventArgs e)
        {
            FDetallesContenido fDetallesContenido = new FDetallesContenido();
            fDetallesContenido.ShowDialog();

        }

        private void FRepositorio_Shown(object sender, EventArgs e)
        {
            
        }
    }
}
