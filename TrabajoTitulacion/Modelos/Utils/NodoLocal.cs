using DSOFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos.Core;
using TrabajoTitulacion.Servicios.Core.Nodos;

namespace TrabajoTitulacion.Modelos.Utils
{
    class NodoLocal
    {
        public string PathLocal { get; set; }
        public string IdAlfresco { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool EsArchivo { get; set; }

        public NodoLocal(string pathLocal, bool esArchivo, bool creado)
        {
            PathLocal = pathLocal;
            EsArchivo = esArchivo;
            IdAlfresco = "";
            if (creado)
            {
                AñadirPropiedades();
            }
        }

        private void AñadirPropiedades()
        {
            IdAlfresco = conseguirIdArchivoLocal(PathLocal);
            if (EsArchivo)
            {
                FechaModificacion = File.GetLastWriteTime(PathLocal);
                FechaCreacion = File.GetCreationTime(PathLocal);
            }
            else
            {
                FechaModificacion = Directory.GetLastWriteTime(PathLocal);
                FechaCreacion = Directory.GetCreationTime(PathLocal);
            }
            
        }
        private string conseguirIdArchivoLocal(string pathArchivo)
        {
            string idAlfrescoArchivo = "";
            if (Directory.Exists(pathArchivo) || File.Exists(pathArchivo)){
                OleDocumentProperties archivo = new OleDocumentProperties();
                archivo.Open(pathArchivo, false, dsoFileOpenOptions.dsoOptionDefault);
                if (!(archivo.CustomProperties.Count == 0))
                {
                    idAlfrescoArchivo = archivo.CustomProperties[0].get_Value();
                }
                archivo.Close(true);
            }
            return idAlfrescoArchivo;
        }

        public void Eliminar()
        {
            if (EsArchivo)
            {
                File.Delete(PathLocal);
            }
            else
            {
                Directory.Delete(PathLocal);
            }
        }

        private void CrearNodoLocal(string pathLocal)
        {
            if (EsArchivo)
            {
            }
        }

        public async void CrearNodoRemoto(string nodoPadre)
        {
            Nodo nodo = new Nodo();
            nodo.Name = Path.GetFileName(PathLocal) ?? Path.GetDirectoryName(PathLocal);
            if (EsArchivo)
            {
                nodo.NodeType = "cm:content";
                Nodo nodoCreado = await NodosServicioStatic.CrearNodoContenido(nodoPadre, nodo, File.ReadAllBytes(PathLocal));
                await NodosServicioStatic.ObtenerContenido(nodoCreado.Id, PathLocal);
                //Se cambia la fecha de modificación del archivo por la del archivo en el servidor
                AñadirIdAlfresco(nodoCreado.Id);
                File.SetLastWriteTime(PathLocal, nodoCreado.ModifiedAt);
                FechaModificacion = nodoCreado.ModifiedAt;
                File.SetCreationTime(PathLocal, nodoCreado.CreatedAt);
                FechaCreacion = nodoCreado.CreatedAt;
            }
            else
            {
                nodo.NodeType = "cm:folder";
                Nodo nodoCreado = await NodosServicioStatic.CrearNodo(nodoPadre, nodo);
                Directory.CreateDirectory(PathLocal);
                AñadirIdAlfresco(nodoCreado.Id);
                Directory.SetLastWriteTime(PathLocal, nodoCreado.ModifiedAt);
                FechaModificacion = nodoCreado.ModifiedAt;
                Directory.SetCreationTime(PathLocal, nodoCreado.CreatedAt);
                FechaCreacion = nodoCreado.CreatedAt;
            }


        }

        public async Task Crear(string idRemoto)
        {
            if (EsArchivo)
            {
                await NodosServicioStatic.ObtenerContenido(idRemoto, PathLocal);
                AñadirIdAlfresco(idRemoto);
                Nodo nodoCreado = await NodosServicioStatic.ObtenerNodo(idRemoto);
                File.SetLastWriteTime(PathLocal, nodoCreado.ModifiedAt);
                FechaModificacion = nodoCreado.ModifiedAt;
                File.SetCreationTime(PathLocal, nodoCreado.CreatedAt);
                FechaCreacion = nodoCreado.CreatedAt;
            }
            else
            {
                Directory.CreateDirectory(PathLocal);
                AñadirIdAlfresco(idRemoto);
                Nodo nodoCreado = await NodosServicioStatic.ObtenerNodo(idRemoto);
                Directory.SetLastWriteTime(PathLocal, nodoCreado.ModifiedAt);
                FechaModificacion = nodoCreado.ModifiedAt;
                Directory.SetCreationTime(PathLocal, nodoCreado.CreatedAt);
                FechaCreacion = nodoCreado.CreatedAt;
            }
        }

        public async Task Modificar(string idRemoto)
        {
            if (EsArchivo)
            {
                await NodosServicioStatic.ObtenerContenido(idRemoto, PathLocal);
                AñadirIdAlfresco(idRemoto);
                Nodo nodoCreado = await NodosServicioStatic.ObtenerNodo(idRemoto);
                File.SetLastWriteTime(PathLocal, nodoCreado.ModifiedAt);
                FechaModificacion = nodoCreado.ModifiedAt;
                File.SetCreationTime(PathLocal, nodoCreado.CreatedAt);
                FechaCreacion = nodoCreado.CreatedAt;
            }
        }

        private void AñadirIdAlfresco(string idRemoto)
        {
            OleDocumentProperties archivo = new OleDocumentProperties();
            archivo.Open(PathLocal, false, dsoFileOpenOptions.dsoOptionOpenReadOnlyIfNoWriteAccess);
            archivo.CustomProperties.Add("IdAlfresco", idRemoto);
            archivo.Save();
            archivo.Close(true);
        }

        public async Task ModificarRemoto(string idRemoto, Nodo archivoAlfresco)
        {
            if (EsArchivo)
            {
                await NodosServicioStatic.ActualizarContenido(archivoAlfresco, false, "Modificado desde Pc", File.ReadAllBytes(PathLocal));
                await NodosServicioStatic.ObtenerContenido(idRemoto, PathLocal);
                AñadirIdAlfresco(idRemoto);
                Nodo nodoCreado = await NodosServicioStatic.ObtenerNodo(idRemoto);
                File.SetLastWriteTime(PathLocal, nodoCreado.ModifiedAt);
                FechaModificacion = nodoCreado.ModifiedAt;
                File.SetCreationTime(PathLocal, nodoCreado.CreatedAt);
                FechaCreacion = nodoCreado.CreatedAt;
            }
        }
    }
}
