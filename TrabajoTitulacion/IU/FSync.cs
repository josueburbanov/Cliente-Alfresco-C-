using DSOFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos.Core;
using TrabajoTitulacion.Modelos.Utils;
using TrabajoTitulacion.Servicios.Core.Nodos;

namespace TrabajoTitulacion.IU
{
    public partial class FSync : Form
    {
        private Nodo nodoRoot;
        public FSync()
        {
            InitializeComponent();
        }

        private async void FSync_Load(object sender, EventArgs e)
        {
            try
            {
                await CargarRepositorio();
                PoblarListBoxRepositoriosGuardados();

            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Hubo un problema cargando sus archivos.");
            }
        }

        private async Task CargarRepositorio()
        {
            FLoading fPrincipalLoading = new FLoading();

            //Se obtiene la lista de los nodos hijos de root (1er nivel) y el nodo root
            List<Nodo> nodosDeRoot;
            nodosDeRoot = await NodosServicioStatic.ObtenerListaNodosHijos("-root-");
            nodoRoot = await NodosServicioStatic.ObtenerNodo("-root-");
            nodoRoot.NodosHijos = nodosDeRoot;
            fPrincipalLoading.Show();

            //Se pobla recursivamente todos los nodos
            await NodosServicioStatic.PoblarNodosHijos(nodosDeRoot);


            //Se agrega los nodos al treeview                
            treeViewSync.Nodes.Add(nodoRoot.Id, "Mis archivos");
            treeViewSync.Nodes[nodoRoot.Id].Tag = nodoRoot;
            AñadirNodosTV(nodosDeRoot, treeViewSync.Nodes[nodoRoot.Id]);

            treeViewSync.Refresh();
            fPrincipalLoading.Close();
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

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            Nodo nodoSeleccionado = (Nodo)treeViewSync.SelectedNode.Tag;
            string pathRepositorio = Environment.CurrentDirectory + "\\" + nodoSeleccionado.Name;
            
            bool exists = Directory.Exists(pathRepositorio);
            if (!exists && !Properties.Settings.Default.RepositoriosPath.Contains(pathRepositorio))
            {
                NodoLocal nodoLocal = new NodoLocal(pathRepositorio, false, false);
                await nodoLocal.Crear(nodoSeleccionado.Id);
                PoblarCarpeta(nodoSeleccionado, pathRepositorio);
                Properties.Settings.Default.RepositoriosPath.Add(pathRepositorio);
                Properties.Settings.Default.RepositoriosId.Add(nodoSeleccionado.Id);
                Properties.Settings.Default.Save();
                PoblarListBoxRepositoriosGuardados();
            }
            else
            {
                MessageBox.Show("El directorio ya se está sincronizando");
            }
        }

        private void PoblarListBoxRepositoriosGuardados()
        {
            foreach (var item in Properties.Settings.Default.RepositoriosPath)
            {
                lstboxRepositoriosGuardados.Items.Add(item);                
            }
            lstboxRepositoriosGuardados.Refresh();
        }

        private async void PoblarCarpeta(Nodo carpetaSeleccionada, string pathLocal)
        {
            foreach (var nodoHijo in carpetaSeleccionada.NodosHijos)
            {
                if (nodoHijo.IsFolder)
                {
                    //Se crea el directorio y se le agrega el id
                    NodoLocal nodoLocal = new NodoLocal(pathLocal + "/" + nodoHijo.Name, false, false);
                    await nodoLocal.Crear(nodoHijo.Id);
                    if (nodoHijo.NodosHijos.Count != 0)
                    {
                        PoblarCarpeta(nodoHijo, pathLocal + "/" + nodoHijo.Name);
                    }
                }
                else if (nodoHijo.IsFile)
                {
                    NodoLocal nodoLocal = new NodoLocal(pathLocal + "/" + nodoHijo.Name, true , false);
                    await nodoLocal.Crear(nodoHijo.Id);
                }

            }
        }

        private async void btnSincronizar_Click(object sender, EventArgs e)
        {
            //Se obtiene repositorio seleccionado y se descarga estructura árbol del repositorio seleccionado
            string repositorioSeleccionado = lstboxRepositoriosGuardados.SelectedItem.ToString();
            int indexOfRepositorio = Properties.Settings.Default.RepositoriosPath.IndexOf(repositorioSeleccionado);
            string idRepositorioSeleccionado = Properties.Settings.Default.RepositoriosId[indexOfRepositorio];
            Nodo NodoSync = await NodosServicioStatic.ObtenerNodo(idRepositorioSeleccionado);
            await SincronizacionPcAlfresco(repositorioSeleccionado, NodoSync, true);

        }

        private async Task SincronizacionPcAlfresco(string repositorioSeleccionado, Nodo NodoSync, bool creado)
        {
            List<Nodo> nodosHijosSync = await NodosServicioStatic.ObtenerListaNodosHijos(NodoSync.Id);
            NodoSync.NodosHijos = nodosHijosSync;
            await NodosServicioStatic.PoblarNodosHijos(nodosHijosSync);

            //Se mapea nodosLocales según repositorio local (PC)
            List<NodoLocal> nodosLocales = new List<NodoLocal>();
            foreach (var path in Directory.GetFiles(repositorioSeleccionado))
            {
                nodosLocales.Add(new NodoLocal(path, true, creado));
            }
            foreach (var path in Directory.GetDirectories(repositorioSeleccionado))
            {
                nodosLocales.Add(new NodoLocal(path, false, creado));
            }

            //En caso de encontrar ids duplicados (Por copia):
            LimpiarIdNodosCopiados(nodosLocales);

            //*********Comparación desde PC hacia Alfresco*********
            foreach (var nodoLocal in nodosLocales)
            {
                Nodo archivoAlfresco = NodoSync.NodosHijos.Find(x => x.Id == nodoLocal.IdAlfresco);

                //Si se ha eliminado en Alfresco, entonces:
                if (archivoAlfresco is null && (!nodoLocal.IdAlfresco.Equals("")))
                {
                    nodoLocal.Eliminar();
                }
                //Si se ha creado en PC, entonces:
                else if ((archivoAlfresco is null && nodoLocal.IdAlfresco.Equals("")))
                {
                    nodoLocal.CrearNodoRemoto(NodoSync.Id);
                }
                //Modificación:
                else if (archivoAlfresco.ModifiedAt == nodoLocal.FechaModificacion)
                {
                    continue;
                }
                //Si el archivo se ha modificado en Alfresco entonces descargar el nuevo Archivo
                else if (archivoAlfresco.ModifiedAt > nodoLocal.FechaModificacion)
                {
                    if (nodoLocal.EsArchivo) await nodoLocal.Modificar(archivoAlfresco.Id);
                    else await SincronizacionPcAlfresco(nodoLocal.PathLocal, archivoAlfresco, false);
                }

                //Si el archivo se ha modificado en la PC entonces actualizar el archivo en Alfresco
                else if (archivoAlfresco.ModifiedAt < nodoLocal.FechaModificacion)
                {
                    if (nodoLocal.EsArchivo) await nodoLocal.ModificarRemoto(archivoAlfresco.Id, archivoAlfresco);
                    else await SincronizacionPcAlfresco(nodoLocal.PathLocal, archivoAlfresco, false);
                }
            }
        }

        private void LimpiarIdNodosCopiados(List<NodoLocal>nodosLocales)
        {
            
            var gruposNodosDuplicados = (nodosLocales
                                  .GroupBy(x => x.IdAlfresco)
                                  .Select(grupo => grupo.ToList())).ToList();


            foreach (var grupoNodoDuplicado in gruposNodosDuplicados)
            {
                LimpiarIdNodosDuplicados(grupoNodoDuplicado);
            }
        }

        private void LimpiarIdNodosDuplicados(List<NodoLocal> nodosDuplicados)
        {
            nodosDuplicados.OrderBy(x => x.FechaCreacion).ToList();
            for (int i = 0; i < nodosDuplicados.Count - 1; i++)
            {
                nodosDuplicados[i].IdAlfresco = "";
            }
        }

        private static string conseguirIdArchivoLocal(string pathArchivo)
        {
            OleDocumentProperties archivo = new OleDocumentProperties();
            archivo.Open(pathArchivo, false, dsoFileOpenOptions.dsoOptionDefault);
            string idAlfrescoArchivo = "";
            if (!(archivo.CustomProperties.Count == 0))
            {
                idAlfrescoArchivo = archivo.CustomProperties[0].get_Value();
            }
            archivo.Close(true);
            return idAlfrescoArchivo;


        }
    }
}