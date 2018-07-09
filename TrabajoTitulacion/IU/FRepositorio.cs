using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos;
using TrabajoTitulacion.Servicios.Core.Nodos;

namespace TrabajoTitulacion.IU
{
    public partial class FRepositorio : Form
    {
        private Node nodoRoot;
        private List<Node> nodosDeRoot;

        public FRepositorio()
        {
            InitializeComponent();
        }

        private void FRepositorio_Load(object sender, EventArgs e)
        {
            try
            {
                //Se obtiene la lista de los nodos hijos de root (1er nivel) y el nodo root
                nodosDeRoot = NodosStatic.ObtenerListaNodosHijos("-root-");
                nodoRoot = NodosStatic.ObtenerNodo("-root-");
                nodoRoot.NodosHijos = nodosDeRoot;

                //Se pobla recursivamente todos los nodos
                NodosStatic.PoblarNodosHijos(nodosDeRoot);

                //Se agrega los nodos al treeview                
                treeViewRepositorio.Nodes.Add(nodoRoot.Id, "Mis archivos");
                treeViewRepositorio.Nodes[nodoRoot.Id].Tag = nodoRoot;
                AñadirNodosTV(nodosDeRoot, treeViewRepositorio.Nodes[nodoRoot.Id]);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Hubo un problema cargando sus archivos.");
            }
        }

        private void AñadirNodosTV(List<Node> nodosPadres, TreeNode nodoTvAbuelo)
        {
            foreach (var nodoPadre in nodosPadres)
            {
                if (nodoPadre.IsFolder)
                {
                    nodoTvAbuelo.Nodes.Add(nodoPadre.Id, nodoPadre.Name);
                    TreeNode nodoTvPadre = nodoTvAbuelo.Nodes[nodoPadre.Id];
                    nodoTvPadre.Tag = nodoPadre;
                    if (!(nodoPadre.NodosHijos is null) && nodoPadre.IsFolder)
                        AñadirNodosTV(nodoPadre.NodosHijos, nodoTvPadre);
                }
            }
        }

        private void treeViewRepositorio_Click(object sender, EventArgs e)
        {

        }

        private void LnklblDescargar_MouseClick(object sender, MouseEventArgs e)
        {
            flwlypanelNodosHijos.Controls.Clear();
        }

        private void treeViewRepositorio_AfterSelect(object sender, TreeViewEventArgs e)
        {
            flwlypanelNodosHijos.Controls.Clear();
            flwlypanelNavegacion.Controls.Clear();
            Node nodoSeleccionado = (Node)treeViewRepositorio.SelectedNode.Tag;

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
                Node nodoPadre = (Node)nodoPadreTV.Tag;
                Node nodoPadreAux = nodoPadre;

                while (!nodoPadreAux.Equals(nodoRoot))
                {
                    nodoPadreTV = nodoPadreTV.Parent;
                    nodoPadreAux = (Node)nodoPadreTV.Tag;
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

        private void DibujarContenido(Node nodoHijo)
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

        }


    }
}
