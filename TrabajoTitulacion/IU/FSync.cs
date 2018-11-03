using DSOFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos.Core;
using TrabajoTitulacion.Modelos.CoreAPI;
using TrabajoTitulacion.Modelos.Sync;
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

        private void FSync_Load(object sender, EventArgs e)
        {
            
        }

        private async Task CargarRepositorio()
        {
            FLoading fPrincipalLoading = new FLoading();

            //Se obtiene la lista de los nodos hijos de root (1er nivel) y el nodo root
            List<Nodo> nodosDeRoot;
            nodosDeRoot = await NodosStatic.ObtenerListaNodosHijos("-root-");
            nodoRoot = await NodosStatic.ObtenerNodo("-root-");
            nodoRoot.NodosHijos = nodosDeRoot;
            fPrincipalLoading.Show();

            //Se pobla recursivamente todos los nodos
            await NodosStatic.PoblarNodosHijos(nodosDeRoot);


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
            btnAceptar.Visible = false;
            treeViewSync.Visible = false;
            Nodo nodoSeleccionado = (Nodo)treeViewSync.SelectedNode.Tag;
            string pathRepositorio = Environment.CurrentDirectory + "\\" + nodoSeleccionado.Name;

            bool exists = Directory.Exists(pathRepositorio);
            if (!exists && !Properties.Settings.Default.RepositoriosPath.Contains(pathRepositorio))
            {
                NodoLocal nodoLocal = new NodoLocal(pathRepositorio, false);
                await nodoLocal.Crear(nodoSeleccionado.Id);
                await PoblarCarpeta(nodoSeleccionado, pathRepositorio);
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

        private async Task PoblarCarpeta(Nodo carpetaSeleccionada, string pathLocal)
        {
            foreach (var nodoHijo in carpetaSeleccionada.NodosHijos)
            {
                if (nodoHijo.IsFolder)
                {
                    //Se crea el directorio y se le agrega el id
                    NodoLocal nodoLocal = new NodoLocal(pathLocal + "\\" + nodoHijo.Name, false);
                    await nodoLocal.Crear(nodoHijo.Id);
                    if (nodoHijo.NodosHijos.Count != 0)
                    {
                        await PoblarCarpeta(nodoHijo, pathLocal + "\\" + nodoHijo.Name);
                        await nodoLocal.ActualizarFechasLocales(nodoHijo);
                    }
                }
                else if (nodoHijo.IsFile)
                {
                    NodoLocal nodoLocal = new NodoLocal(pathLocal + "\\" + nodoHijo.Name, true);
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
            Nodo nodoSync = await NodosStatic.ObtenerNodo(idRepositorioSeleccionado);


            try
            {
                await SincronizacionPcAlfresco(repositorioSeleccionado, nodoSync);

            }
            catch (IOException exc)
            {
                MessageBox.Show("No se ha podido sincronizar. " + exc.Message +
                    " Cierre ese proceso y vuelva a sincronizar.");
            }catch (FileFormatException)
            {
                MessageBox.Show("No se ha podido sincronizar. Hay un archivo Office de tamaño 0" );
            }


        }

        private List<NodoLocal> MapearNodosLocales(string repositorioSeleccionado)
        {
            //Se mapea nodosLocales según repositorio local (PC)
            List<NodoLocal> nodosLocales = new List<NodoLocal>();
            foreach (var path in Directory.GetFiles(repositorioSeleccionado))
            {
                nodosLocales.Add(new NodoLocal(path, true));
            }
            foreach (var path in Directory.GetDirectories(repositorioSeleccionado))
            {
                nodosLocales.Add(new NodoLocal(path, false));
            }
            return nodosLocales;
        }

        private void RevisarProcesoLibre(List<NodoLocal> nodosLocales)
        {
            foreach (var nodoLocal in nodosLocales)
            {
                nodoLocal.RegresarFechaModifOriginal();
            }
        }

        private async Task SincronizacionPcAlfresco(string repositorioSeleccionado, Nodo nodoSync)
        {
            List<Nodo> nodosHijosSync = await NodosStatic.ObtenerListaNodosHijos(nodoSync.Id);
            nodoSync.NodosHijos = nodosHijosSync;
            await NodosStatic.PoblarNodosHijos(nodosHijosSync);
            await NodosStatic.CompletarNodos(nodoSync);

            List<NodoLocal> nodosLocales = MapearNodosLocales(repositorioSeleccionado);
            RevisarProcesoLibre(nodosLocales);

            //En caso de encontrar ids duplicados (Por copia):
            LimpiarIdNodosCopiados(nodosLocales);

            //*********Comparación desde PC hacia Alfresco*********
            foreach (var nodoLocal in nodosLocales)
            {
                Nodo archivoAlfresco = nodoSync.NodosHijos.Find(x => x.Id == nodoLocal.IdAlfresco);

                if (nodoLocal.IdAlfresco != "")
                {
                    if (!await VerificarPath(nodoLocal))
                    {
                        nodoLocal.IdAlfresco = "";
                        nodoLocal.AñadirIdAlfrescoLocal("");
                    }
                }

                //Si se ha eliminado en Alfresco, entonces:
                if (archivoAlfresco is null && (!nodoLocal.IdAlfresco.Equals("")))
                {
                    nodoLocal.Eliminar();
                    continue;
                }
                //Si se ha creado en PC, entonces:
                else if ((archivoAlfresco is null && nodoLocal.IdAlfresco.Equals("")))
                {
                    await nodoLocal.CrearNodoRemoto(nodoSync.Id);
                }
                //Modificación:
                else if (archivoAlfresco.ModifiedAt == nodoLocal.FechaModificacion)
                {
                    //En caso de cambio de nombre
                    if (archivoAlfresco.Name != nodoLocal.Nombre)
                    {
                        await nodoLocal.ModificarRemoto(archivoAlfresco.Id, archivoAlfresco);
                    }
                }
                //Si el archivo se ha modificado en Alfresco entonces descargar el nuevo Archivo
                else if (archivoAlfresco.ModifiedAt > nodoLocal.FechaModificacion)
                {
                    if (nodoLocal.EsArchivo) await nodoLocal.Modificar(archivoAlfresco.Id);                    
                }

                //Si el archivo se ha modificado en la PC entonces actualizar el archivo en Alfresco
                else if (archivoAlfresco.ModifiedAt < nodoLocal.FechaModificacion)
                {
                    if (nodoLocal.EsArchivo) await nodoLocal.ModificarRemoto(archivoAlfresco.Id, archivoAlfresco);                    
                }

                if (!nodoLocal.EsArchivo && !(archivoAlfresco is null))
                {
                    await SincronizacionPcAlfresco(nodoLocal.PathLocal, archivoAlfresco);
                    await nodoLocal.ActualizarFechasLocales(archivoAlfresco);
                }

            }
            await SincronizarAlfrescoPc(repositorioSeleccionado, nodoSync, nodosLocales);
        }

        private async Task<bool> VerificarPath(NodoLocal nodoLocal)
        {
            Nodo nodoRemoto = await NodosStatic.ObtenerNodo(nodoLocal.IdAlfresco);
            if (nodoRemoto is null) return true;
            string pathSinDisco = nodoLocal.PathLocal.Split(':')[1];
            string[] partesPath = pathSinDisco.Split('\\');
            partesPath = partesPath.Where(x => x != nodoLocal.Nombre).ToArray();
            foreach (var parte in partesPath)
            {

                if (!(nodoRemoto.Path.Elements.Where(x => x.Name == parte).FirstOrDefault() is null))
                {
                    List<PathElement> pathActualRemoto = nodoRemoto.Path.Elements.ToList();
                    int indexIni = nodoRemoto.Path.Elements.IndexOf(nodoRemoto.Path.Elements.Where(x => x.Name == parte).FirstOrDefault());
                    pathActualRemoto.RemoveRange(0, indexIni);
                    return CompararPath(partesPath, pathActualRemoto);
                }
                else
                {
                    int parteIndice = Array.IndexOf(partesPath, parte);
                    partesPath = partesPath.Where((val, idx) => idx != parteIndice).ToArray();
                }
            }
            return false;
        }

        private bool CompararPath(string[] partesPath, List<PathElement> pathActualRemoto)
        {
            if (partesPath.Count() == pathActualRemoto.Count)
            {
                for (int i = 0; i < partesPath.Length; i++)
                {
                    if (partesPath[i] == pathActualRemoto[i].Name) continue;
                    else return false;
                }
                return true;
            }
            return false;
        }

        private async Task SincronizarAlfrescoPc(string repositorioSeleccionado, Nodo nodoSync, List<NodoLocal> nodosLocales)
        {
            //*********Comparación desde Alfresco hacia PC*********
            foreach (var nodoRemoto in nodoSync.NodosHijos)
            {
                NodoLocal nodoLocal = nodosLocales.Find(x => x.IdAlfresco == nodoRemoto.Id);
                bool creadoRemotamente = false;
                List<string> aspectosNodo = (List<string>)nodoRemoto.AspectNames;
                if (!(aspectosNodo.Contains("sync:Sincronizable")))
                {
                    creadoRemotamente = true;
                }

                //Si se ha eliminado en PC, entonces:
                if (nodoLocal is null && !creadoRemotamente)
                {
                    await NodoLocal.EliminarRemoto(nodoRemoto.Id);
                }
                //Si se ha creado en Alfresco, entonces:
                else if (nodoLocal is null && creadoRemotamente)
                {
                    if (nodoRemoto.IsFile)
                    {
                        nodoLocal = new NodoLocal(repositorioSeleccionado + "\\" + nodoRemoto.Name, true);
                    }
                    else
                    {
                        nodoLocal = new NodoLocal(repositorioSeleccionado + "\\" + nodoRemoto.Name, false);
                    }
                    await nodoLocal.Crear(nodoRemoto.Id);
                }
            }
        }


        private void LimpiarIdNodosCopiados(List<NodoLocal> nodosLocales)
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
                nodosDuplicados[i].AñadirIdAlfrescoLocal("");
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

        private async void btnAgregarRepo_Click(object sender, EventArgs e)
        {
            try
            {
                btnAceptar.Visible = true;
                treeViewSync.Visible = true;
                await CargarRepositorio();
                PoblarListBoxRepositoriosGuardados();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Hubo un problema cargando sus archivos.");
            }
        }
    }
}