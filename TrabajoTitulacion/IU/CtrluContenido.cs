using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos.CoreAPI;
using TrabajoTitulacion.Servicios.Core.Nodos;
using TrabajoTitulacion.Modelos.Core;
using System.IO;
using TrabajoTitulacion.Modelos.Sync;

namespace TrabajoTitulacion.IU
{
    public partial class CtrluContenido : UserControl
    {
        public CtrluContenido()
        {
            InitializeComponent();
        }


        private async void lnklblDescargar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var path = abrirFldbrswrSeleccionarPath();
            if (path != null)
            {
                path = path + "\\" + ((Node)Tag).Name;
                if (!Directory.Exists(path) && !File.Exists(path))
                {
                    await NodosStatic.ObtenerContenido(((Nodo)Tag), path);
                    MessageBox.Show("La descarga se está ejecutando en segundo plano. En un momento estará disponible en " +
                        path, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Ya existe un nodo con el mismo nombre. ¿Desea sobrescribirlo" +
                        "?", "Precaución", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (((Node)Tag).IsFile)
                        {
                            File.Delete(path);
                        }
                        else
                        {
                            Directory.Delete(path, true);
                        }

                        await NodosStatic.ObtenerContenido(((Nodo)Tag), path);
                        MessageBox.Show("La descarga se está ejecutando en segundo plano. En un momento estará disponible en " +
                            path, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

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

        private void CtrluContenido_Load(object sender, EventArgs e)
        {
            Nodo nodoSeleccionado = (Nodo)Tag;
            if (nodoSeleccionado.Content is null)
            {
                pctboxTipoContenido.Image = Properties.Resources.folder_icon;
            }
            else if (nodoSeleccionado.Content.MimeTypeName == "Plain Text")
            {
                pctboxTipoContenido.Image = Properties.Resources.txt_icon_256;
            }
            else if (nodoSeleccionado.Content.MimeTypeName == "Microsoft PowerPoint 2007")
            {
                pctboxTipoContenido.Image = Properties.Resources.ppt_icon;
            }
            else if (nodoSeleccionado.Content.MimeTypeName == "Microsoft Excel 2007")
            {
                pctboxTipoContenido.Image = Properties.Resources.spreadsheet;
            }
            else if (nodoSeleccionado.Content.MimeTypeName == "ZIP")
            {
                pctboxTipoContenido.Image = Properties.Resources.compress;
            }
            else if (nodoSeleccionado.Content.MimeTypeName == "Microsoft Word 2007")
            {
                pctboxTipoContenido.Image = Properties.Resources.doc_icon;
            }
            else if (nodoSeleccionado.Content.MimeTypeName == "PNG Image")
            {
                pctboxTipoContenido.Image = Properties.Resources.imagen_icon;
            }
            else if (nodoSeleccionado.Content.MimeType == "application/pdf")
            {
                pctboxTipoContenido.Image = Properties.Resources.pdf2;
            }

        }

        private async void CtrluContenido_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                Nodo nodo = (Nodo)((CtrluContenido)sender).Tag;
                await SubirMasivamente(FileList, nodo.ParentId);
            }
        }

        private async Task SubirMasivamente(string[] FileList, string idPadre)
        {
            foreach (var item in FileList)
            {
                await CrearNodoRemoto(idPadre, item);
            }
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
                        NodoLocal nodoLocalHijo = new NodoLocal(item, true);
                        await nodoLocalHijo.CrearNodoRemoto(nodoCreado.Id);
                    }

                }
                if (!(Directory.GetDirectories(PathLocal) is null))
                {
                    foreach (var item in Directory.GetDirectories(PathLocal))
                    {
                        NodoLocal nodoLocalHijo = new NodoLocal(item, false);
                        await nodoLocalHijo.CrearNodoRemoto(nodoCreado.Id);
                    }
                }
            }
        }

        private void CtrluContenido_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
    }
}
