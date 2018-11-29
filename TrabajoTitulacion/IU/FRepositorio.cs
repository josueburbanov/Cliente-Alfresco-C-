using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos.CoreAPI;
using TrabajoTitulacion.Modelos.Core;
using TrabajoTitulacion.Servicios.Core.Nodos;
using System.IO;
using TrabajoTitulacion.Modelos.Sync;

namespace TrabajoTitulacion.IU
{
    public partial class FRepositorio : Form
    {
        private Nodo nodoRoot;

        public FRepositorio()
        {
            InitializeComponent();
        }

        private async void FRepositorio_Load(object sender, EventArgs e)
        {
            await CargaInicial();
        }

        private async Task CargaInicial()
        {
            try
            {
                FLoading fPrincipalLoading = new FLoading();

                //Se obtiene la lista de los nodos hijos de root (1er nivel) y el nodo root
                nodoRoot = new Nodo();
                List<Nodo> nodosDeRoot;
                nodoRoot = await NodosStatic.ObtenerNodo("-root-");
                nodosDeRoot = await NodosStatic.ObtenerListaNodosHijos("-root-");
                nodoRoot.NodosHijos = nodosDeRoot;

                fPrincipalLoading.Show();
                //Se pobla recursivamente todos los nodos
                //await NodosStatic.PoblarNodosHijos(nodosDeRoot);

                //Se agrega los nodos al treeview   
                treeViewRepositorio.Nodes.Clear();
                treeViewRepositorio.Nodes.Add(nodoRoot.Id, "Mis archivos");
                treeViewRepositorio.Nodes[nodoRoot.Id].Tag = nodoRoot;
                AñadirNodosTV(nodosDeRoot, treeViewRepositorio.Nodes[nodoRoot.Id]);
                treeViewRepositorio.SelectedNode = treeViewRepositorio.Nodes.Find(nodoRoot.Id, true)[0];
                treeViewRepositorio.Nodes.Find(nodoRoot.Id, true)[0].Expand();
                fPrincipalLoading.Close();

            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Hubo un problema cargando sus archivos.");
            }
        }

        /// <summary>
        /// Añade recursivamente una lista de nodos al treeview
        /// </summary>
        /// <param name="nodosPadres">Lista de nodos a añadir</param>
        /// <param name="nodoTvAbuelo">Nodo del tree view, que se encuentra sobre 
        /// los nodos que se va a añadir</param>
        private void AñadirNodosTV(List<Nodo> nodosPadres, TreeNode nodoTvAbuelo)
        {
            nodoTvAbuelo.Nodes.Clear();
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

        private async void treeViewRepositorio_AfterExpand(object sender, TreeViewEventArgs e)
        {
            await RecargarNodosAñadirTreeView(e);

        }

        private async Task RecargarNodosAñadirTreeView(TreeViewEventArgs e)
        {
            Nodo nodoSeleccionado = null;
            TreeNode treeNode = null;
            if ((e.Node) is null)
            {
                treeNode = treeViewRepositorio.Nodes[nodoRoot.Id];
                nodoSeleccionado = nodoRoot;
            }
            else if (e.Node.Tag == nodoRoot)
            {
                treeViewRepositorio.Nodes[nodoRoot.Id].Nodes.Clear();
                treeNode = e.Node;
                nodoSeleccionado = (Nodo)treeNode.Tag;
            }
            else
            {
                treeNode = e.Node;
                nodoSeleccionado = (Nodo)treeNode.Tag;
            }

            nodoSeleccionado = await NodosStatic.ObtenerNodo(nodoSeleccionado.Id);
            nodoSeleccionado.NodosHijos = await NodosStatic.ObtenerListaNodosHijos(nodoSeleccionado.Id);

            foreach (var item in nodoSeleccionado.NodosHijos)
            {
                item.NodosHijos = await NodosStatic.ObtenerListaNodosHijos(item.Id);
            }

            AñadirNodosTV(nodoSeleccionado.NodosHijos, treeNode);
            treeViewRepositorio.Refresh();
        }

        private async void treeViewRepositorio_AfterSelect(object sender, TreeViewEventArgs e)
        {
            flwlypanelNodosHijos.Controls.Clear();
            flwlypanelNavegacion.Controls.Clear();
            Nodo nodoSeleccionado = (Nodo)treeViewRepositorio.SelectedNode.Tag;
            nodoSeleccionado.NodosHijos = await NodosStatic.ObtenerListaNodosHijos(nodoSeleccionado.Id);

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

            TreeNode nodoActualTV = treeViewRepositorio.SelectedNode;
            Nodo nodoActual = (Nodo)nodoActualTV.Tag;

            List<Nodo> nodosBackwards = new List<Nodo>();
            while (nodoActual.ParentId != null)
            {
                nodosBackwards.Add(nodoActual);
                nodoActual = (Nodo)treeViewRepositorio.Nodes.Find(nodoActual.ParentId, true)[0].Tag;
            }
            nodosBackwards.Add(nodoRoot);

            for (int i = nodosBackwards.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    DibujarLinkNavegacion(nodoSeleccionado);
                }
                else
                {
                    DibujarLinkNavegacion(nodosBackwards[i]);
                    DibujarSeparador();
                }
            }
        }

        private void DibujarLinkNavegacion(Nodo nodo)
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
            lnklblNodoHijo.Text = nodo.Name;
            lnklblNodoHijo.Tag = nodo;
            lnklblNodoHijo.LinkBehavior = LinkBehavior.NeverUnderline;
            flwlypanelNavegacion.Controls.Add(lnklblNodoHijo);
            lnklblNodoHijo.MouseClick += LnklblNodoHijo_MouseClick;
        }

        private void LnklblNodoHijo_MouseClick(object sender, MouseEventArgs e)
        {
            flwlypanelNodosHijos.Controls.Clear();
            flwlypanelNavegacion.Controls.Clear();
            Nodo nodoSeleccionado = (Nodo)(((LinkLabel)sender)).Tag;
            treeViewRepositorio.SelectedNode = treeViewRepositorio.Nodes.Find(nodoSeleccionado.Id, true)[0];
            treeViewRepositorio.Nodes.Find(nodoSeleccionado.Id, true)[0].Expand();
        }

        /// <summary>
        /// Dibuja un nodo (CtrluContenido) y lo ubica en el flowlayoutpanel
        /// </summary>
        /// <param name="nodoHijo">Nodo a dibujarse</param>
        private void DibujarContenido(Nodo nodoHijo)
        {
            CtrluContenido ctrluContenido = new CtrluContenido();
            ctrluContenido.BackColor = System.Drawing.Color.Transparent;
            ctrluContenido.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            ctrluContenido.LnklblNombre.Text = nodoHijo.Name;
            if (nodoHijo.IsFile) ctrluContenido.LnklblNombre.Enabled = false;
            ctrluContenido.LblModificado.Text = "Modificado el " +
                nodoHijo.ModifiedAt.ToShortDateString() + " por: " + nodoHijo.ModifiedByUser.DisplayName;
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

        private async void LnklblNombre_MouseClick(object sender, MouseEventArgs e)
        {

            flwlypanelNodosHijos.Controls.Clear();
            flwlypanelNavegacion.Controls.Clear();
            Nodo nodoSeleccionado = (Nodo)(((LinkLabel)sender).Parent).Tag;
            nodoSeleccionado.NodosHijos = await NodosStatic.ObtenerListaNodosHijos(nodoSeleccionado.Id);
            if (!nodoSeleccionado.IsFile)
            {
                treeViewRepositorio.SelectedNode = treeViewRepositorio.Nodes.Find(nodoSeleccionado.Id, true)[0];
                if (nodoSeleccionado.NodosHijos.Count != 0)
                {
                    treeViewRepositorio.Nodes.Find(nodoSeleccionado.Id, true)[0].Expand();
                }

            }
        }

        private void btnSubirNivel_Click(object sender, EventArgs e)
        {
            Nodo nodoSeleccionado = (Nodo)(((LinkLabel)sender).Parent).Tag;
            treeViewRepositorio.SelectedNode = treeViewRepositorio.Nodes.Find(nodoSeleccionado.Id, true)[0];
        }

        private async void flwlypanelNodosHijos_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                    Nodo nodo = (Nodo)(treeViewRepositorio.SelectedNode.Tag);
                    await SubirMasivamente(FileList, nodo.Id);
                }
            }
        }
        private async Task SubirMasivamente(string[] FileList, string idPadre)
        {
            foreach (var item in FileList)
            {
                await CrearNodoRemoto(idPadre, item);
            }
            await CargaInicial();
            MessageBox.Show("Se han cargado todos sus archivos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public async Task CrearNodoRemoto(string nodoPadre, string PathLocal)
        {
            Nodo nodo = new Nodo();
            bool EsArchivo;
            if (File.Exists(PathLocal))
            {
                EsArchivo = true;
            }
            else
            {
                EsArchivo = false;
            }

            nodo.Name = Path.GetFileName(PathLocal) ?? Path.GetDirectoryName(PathLocal);

            if (EsArchivo)
            {
                nodo.NodeType = "cm:content";
                Nodo nodoCreado = await NodosStatic.CrearNodoContenido(nodoPadre, nodo, File.ReadAllBytes(PathLocal));

            }
            else
            {
                nodo.NodeType = "cm:folder";
                Nodo nodoCreado = await NodosStatic.CrearNodo(nodoPadre, nodo);
                if (!(Directory.GetFiles(PathLocal) is null))
                {
                    foreach (var item in Directory.GetFiles(PathLocal))
                    {
                        await CrearNodoRemoto(nodoCreado.Id, item);
                    }

                }
                if (!(Directory.GetDirectories(PathLocal) is null))
                {
                    foreach (var item in Directory.GetDirectories(PathLocal))
                    {
                        await CrearNodoRemoto(nodoCreado.Id, item);
                    }
                }
            }
        }

        private void flwlypanelNodosHijos_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }


    }
}

