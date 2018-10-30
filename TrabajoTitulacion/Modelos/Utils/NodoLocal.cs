using DSOFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos.Core;
using TrabajoTitulacion.Servicios.Core.Nodos;
using DocumentFormat.OpenXml.CustomProperties;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Packaging;

namespace TrabajoTitulacion.Modelos.Utils
{
    class NodoLocal
    {
        public string PathLocal { get; set; }
        public string IdAlfresco { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool EsArchivo { get; set; }
        public string Nombre { get; set; }

        public NodoLocal(string pathLocal, bool esArchivo)
        {
            PathLocal = pathLocal;
            EsArchivo = esArchivo;
            IdAlfresco = "";
            AñadirPropiedades();
        }

        private void AñadirPropiedades()
        {
            if (Directory.Exists(PathLocal) || File.Exists(PathLocal))
            {
                RevisarProcesoLibre();
                Nombre = Path.GetFileName(PathLocal);
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
                IdAlfresco = ConseguirIdArchivoLocal();
            }
        }

        public void RegresarFechaModificacionOriginal()
        {
            //Rollback... porque obtener las propiedades modifica la fecha de modificación
            if (EsArchivo)
            {
                File.SetLastWriteTime(PathLocal, FechaModificacion);
            }
            else
            {
                Directory.SetLastWriteTime(PathLocal, FechaModificacion);
            }
        }

        private string ConseguirIdArchivoLocal()
        {
            try
            {
                string idAlfrescoArchivo = "";

                List<string> extOfficeDocs = new List<string> { ".docx", ".docm", ".dotx", ".dotm", ".docb", ".xlsx", ".xlsm", ".xltx", ".xltm", ".pptx", "pptm", ".potx", "potm", ".ppam", ".ppsm", ".sldx", ".sldm" };
                if (extOfficeDocs.Contains(Path.GetExtension(PathLocal)))
                {
                    idAlfrescoArchivo = GetCustomProperty(PathLocal, "IdAlfresco", Path.GetExtension(PathLocal)) ?? "";
                }
                else
                {
                    OleDocumentProperties archivo = new OleDocumentProperties();
                    archivo.Open(PathLocal);
                    if ((!(archivo.CustomProperties.Count == 0)))
                    {
                        foreach (CustomProperty property in archivo.CustomProperties)
                        {                            
                            if (property.Name == "IdAlfresco")
                            {
                                idAlfrescoArchivo =property.get_Value();                                
                            }
                        }
                    }
                    archivo.Close(true);                    
                }
                return idAlfrescoArchivo;
            }
            catch (System.Runtime.InteropServices.COMException)
            {

            }
            return "";
        }

        public void Eliminar()
        {
            if (EsArchivo)
            {
                File.Delete(PathLocal);
            }
            else
            {
                Directory.Delete(PathLocal,true);
            }
        }

        public async Task CrearNodoRemoto(string nodoPadre)
        {
            Nodo nodo = new Nodo();
            nodo.Name = Path.GetFileName(PathLocal) ?? Path.GetDirectoryName(PathLocal);
            if (EsArchivo)
            {
                nodo.NodeType = "cm:content";
                Nodo nodoCreado = await NodosStatic.CrearNodoContenido(nodoPadre, nodo, File.ReadAllBytes(PathLocal));
                await NodosStatic.ObtenerContenido(nodoCreado.Id, PathLocal);
                AñadirIdAlfrescoLocal(nodoCreado.Id);
                await AñadirIdRemoto(nodoCreado);
                await ActualizarFechasLocales(nodoCreado);
            }
            else
            {
                nodo.NodeType = "cm:folder";
                Nodo nodoCreado = await NodosStatic.CrearNodo(nodoPadre, nodo);
                Directory.CreateDirectory(PathLocal);
                AñadirIdAlfrescoLocal(nodoCreado.Id);
                await AñadirIdRemoto(nodoCreado);
                await ActualizarFechasLocales(nodoCreado);
            }


        }

        public async Task Crear(string idRemoto)
        {
            if (EsArchivo)
            {
                await NodosStatic.ObtenerContenido(idRemoto, PathLocal);
                AñadirIdAlfrescoLocal(idRemoto);
                Nodo nodoCreado = await NodosStatic.ObtenerNodo(idRemoto);
                await AñadirIdRemoto(nodoCreado);
                await ActualizarFechasLocales(nodoCreado);
            }
            else
            {
                Directory.CreateDirectory(PathLocal);
                AñadirIdAlfrescoLocal(idRemoto);
                Nodo nodoCreado = await NodosStatic.ObtenerNodo(idRemoto);
                await AñadirIdRemoto(nodoCreado);
                await ActualizarFechasLocales(nodoCreado);
            }
        }

        /// <summary>
        /// Se añade un aspecto con una propiedad para identificar remotamente qué archivos están siendo Sincronizados
        /// </summary>
        /// <param name="nodoCreado"></param>
        /// <returns></returns>
        private static async Task AñadirIdRemoto(Nodo nodoCreado)
        {
            nodoCreado.AspectNames.Add("sync:Sincronizable");
            await NodosStatic.ActualizarAspectosNodo(nodoCreado);
        }

        public async Task Modificar(string idRemoto)
        {
            if (EsArchivo)
            {
                await NodosStatic.ObtenerContenido(idRemoto, PathLocal);
                AñadirIdAlfrescoLocal(idRemoto);
                Nodo nodoCreado = await NodosStatic.ObtenerNodo(idRemoto);
                await ActualizarFechasLocales(nodoCreado);
            }
        }

        public async Task ActualizarFechasLocales(Nodo nodoCreado)
        {
            nodoCreado = await NodosStatic.ObtenerNodo(nodoCreado.Id);
            if (EsArchivo)
            {
                File.SetLastWriteTime(PathLocal, nodoCreado.ModifiedAt);
                FechaModificacion = nodoCreado.ModifiedAt;
                File.SetCreationTime(PathLocal, nodoCreado.CreatedAt);
                FechaCreacion = nodoCreado.CreatedAt;
            }
            else
            {
                Directory.SetLastWriteTime(PathLocal, nodoCreado.ModifiedAt);
                FechaModificacion = nodoCreado.ModifiedAt;
                Directory.SetCreationTime(PathLocal, nodoCreado.CreatedAt);
                FechaCreacion = nodoCreado.CreatedAt;
            }
        }

        public void AñadirIdAlfrescoLocal(string idRemoto)
        {
            try
            {
                List<string> extOfficeDocs = new List<string> { ".docx", ".docm", ".dotx", ".dotm", ".docb", ".xlsx", ".xlsm", ".xltx", ".xltm", ".pptx", "pptm", ".potx", "potm", ".ppam", ".ppsm", ".sldx", ".sldm" };
                if (extOfficeDocs.Contains(Path.GetExtension(PathLocal)))
                {
                    SetCustomProperty(PathLocal, "IdAlfresco", idRemoto, PropertyTypes.Text, Path.GetExtension(PathLocal));
                }
                else
                {
                    OleDocumentProperties archivo = new OleDocumentProperties();
                    archivo.Open(PathLocal);
                    archivo.CustomProperties.Add("IdAlfresco", idRemoto);
                    archivo.Save();
                    archivo.Close(true);
                }
            }
            catch (System.Runtime.InteropServices.COMException)
            {

            }
        }

        public async Task ModificarRemoto(string idRemoto, Nodo archivoAlfresco)
        {
            archivoAlfresco.Name = Nombre;
            if (EsArchivo)
            {
                await NodosStatic.ActualizarContenido(archivoAlfresco, false, "Modificado desde Pc", File.ReadAllBytes(PathLocal));
                await NodosStatic.ObtenerContenido(idRemoto, PathLocal);
                AñadirIdAlfrescoLocal(idRemoto);

                Nodo nodoCreado = await NodosStatic.ObtenerNodo(idRemoto);
                await ActualizarFechasLocales(nodoCreado);
            }
            else
            {
                await NodosStatic.ActualizarNodo(archivoAlfresco);                                
                Nodo nodoCreado = await NodosStatic.ObtenerNodo(idRemoto);
                await ActualizarFechasLocales(nodoCreado);
            }

        }

        public async static Task EliminarRemoto(string idRemoto)
        {
            await NodosStatic.EliminarNodo(idRemoto);
        }

        public void RevisarProcesoLibre()
        {
            if (EsArchivo)
            {
                File.SetCreationTime(PathLocal, File.GetCreationTime(PathLocal));
            }
            else
            {
                Directory.SetCreationTime(PathLocal, Directory.GetCreationTime(PathLocal));
            }
            
        }

        public enum PropertyTypes : int
        {
            YesNo,
            Text,
            DateTime,
            NumberInteger,
            NumberDouble
        }

        public static string SetCustomProperty(
            string fileName,
            string propertyName,
            object propertyValue,
            PropertyTypes propertyType,
            string extension)
        {
            // Given a document name, a property name/value, and the property type, 
            // add a custom property to a document. The method returns the original
            // value, if it existed.

            string returnValue = null;

            var newProp = new CustomDocumentProperty();
            bool propSet = false;

            // Calculate the correct type.
            switch (propertyType)
            {
                case PropertyTypes.DateTime:

                    // Be sure you were passed a real date, 
                    // and if so, format in the correct way. 
                    // The date/time value passed in should 
                    // represent a UTC date/time.
                    if ((propertyValue) is DateTime)
                    {
                        newProp.VTFileTime =
                            new VTFileTime(string.Format("{0:s}Z",
                                Convert.ToDateTime(propertyValue)));
                        propSet = true;
                    }

                    break;

                case PropertyTypes.NumberInteger:
                    if ((propertyValue) is int)
                    {
                        newProp.VTInt32 = new VTInt32(propertyValue.ToString());
                        propSet = true;
                    }

                    break;

                case PropertyTypes.NumberDouble:
                    if (propertyValue is double)
                    {
                        newProp.VTFloat = new VTFloat(propertyValue.ToString());
                        propSet = true;
                    }

                    break;

                case PropertyTypes.Text:
                    newProp.VTLPWSTR = new VTLPWSTR(propertyValue.ToString());
                    propSet = true;

                    break;

                case PropertyTypes.YesNo:
                    if (propertyValue is bool)
                    {
                        // Must be lowercase.
                        newProp.VTBool = new VTBool(
                          Convert.ToBoolean(propertyValue).ToString().ToLower());
                        propSet = true;
                    }
                    break;
            }

            if (!propSet)
            {
                // If the code was not able to convert the 
                // property to a valid value, throw an exception.
                throw new InvalidDataException("propertyValue");
            }

            // Now that you have handled the parameters, start
            // working on the document.
            newProp.FormatId = "{D5CDD505-2E9C-101B-9397-08002B2CF9AE}";
            newProp.Name = propertyName;

            if (extension == ".docx" || extension == ".docm" || extension == ".dotx" || extension == ".dotm" || extension == ".docb")
            {
                using (var document = WordprocessingDocument.Open(fileName, true))
                {
                    var customProps = document.CustomFilePropertiesPart;
                    if (customProps == null)
                    {
                        // No custom properties? Add the part, and the
                        // collection of properties now.
                        customProps = document.AddCustomFilePropertiesPart();
                        customProps.Properties =
                            new DocumentFormat.OpenXml.CustomProperties.Properties();
                    }

                    var props = customProps.Properties;
                    if (props != null)
                    {
                        // This will trigger an exception if the property's Name 
                        // property is null, but if that happens, the property is damaged, 
                        // and probably should raise an exception.
                        var prop =
                            props.Where(
                            p => ((CustomDocumentProperty)p).Name.Value
                                == propertyName).FirstOrDefault();

                        // Does the property exist? If so, get the return value, 
                        // and then delete the property.
                        if (prop != null)
                        {
                            returnValue = prop.InnerText;
                            prop.Remove();
                        }

                        // Append the new property, and 
                        // fix up all the property ID values. 
                        // The PropertyId value must start at 2.
                        props.AppendChild(newProp);
                        int pid = 2;
                        foreach (CustomDocumentProperty item in props)
                        {
                            item.PropertyId = pid++;
                        }
                        props.Save();
                    }
                }
                return returnValue;
            }
            else if (extension == ".xlsx" || extension == ".xlsm" || extension == ".xltx" || extension == ".xltm")
            {
                using (var document = SpreadsheetDocument.Open(fileName, true))
                {
                    var customProps = document.CustomFilePropertiesPart;
                    if (customProps == null)
                    {
                        // No custom properties? Add the part, and the
                        // collection of properties now.
                        customProps = document.AddCustomFilePropertiesPart();
                        customProps.Properties =
                            new DocumentFormat.OpenXml.CustomProperties.Properties();
                    }

                    var props = customProps.Properties;
                    if (props != null)
                    {
                        // This will trigger an exception if the property's Name 
                        // property is null, but if that happens, the property is damaged, 
                        // and probably should raise an exception.
                        var prop =
                            props.Where(
                            p => ((CustomDocumentProperty)p).Name.Value
                                == propertyName).FirstOrDefault();

                        // Does the property exist? If so, get the return value, 
                        // and then delete the property.
                        if (prop != null)
                        {
                            returnValue = prop.InnerText;
                            prop.Remove();
                        }

                        // Append the new property, and 
                        // fix up all the property ID values. 
                        // The PropertyId value must start at 2.
                        props.AppendChild(newProp);
                        int pid = 2;
                        foreach (CustomDocumentProperty item in props)
                        {
                            item.PropertyId = pid++;
                        }
                        props.Save();
                    }
                }
                return returnValue;

            }
            else if (extension == ".pptx" || extension == "pptm" || extension == ".potx" || extension == "potm" || extension == ".ppam" || extension == ".ppsm" || extension == ".sldx" || extension == ".sldm")
            {
                using (var document = PresentationDocument.Open(fileName, true))
                {
                    var customProps = document.CustomFilePropertiesPart;
                    if (customProps == null)
                    {
                        // No custom properties? Add the part, and the
                        // collection of properties now.
                        customProps = document.AddCustomFilePropertiesPart();
                        customProps.Properties =
                            new DocumentFormat.OpenXml.CustomProperties.Properties();
                    }

                    var props = customProps.Properties;
                    if (props != null)
                    {
                        // This will trigger an exception if the property's Name 
                        // property is null, but if that happens, the property is damaged, 
                        // and probably should raise an exception.
                        var prop =
                            props.Where(
                            p => ((CustomDocumentProperty)p).Name.Value
                                == propertyName).FirstOrDefault();

                        // Does the property exist? If so, get the return value, 
                        // and then delete the property.
                        if (prop != null)
                        {
                            returnValue = prop.InnerText;
                            prop.Remove();
                        }

                        // Append the new property, and 
                        // fix up all the property ID values. 
                        // The PropertyId value must start at 2.
                        props.AppendChild(newProp);
                        int pid = 2;
                        foreach (CustomDocumentProperty item in props)
                        {
                            item.PropertyId = pid++;
                        }
                        props.Save();
                    }
                }
                return returnValue;
            }
            return null;
        }

        public static string GetCustomProperty(string fileName, string propertyName, string extension)
        {
            string returnValue = null;
            if (extension == ".docx" || extension == ".docm" || extension == ".dotx" || extension == ".dotm" || extension == ".docb")
            {
                using (var document = WordprocessingDocument.Open(fileName, true))
                {
                    var customProps = document.CustomFilePropertiesPart;
                    if (customProps == null)
                    {
                        // No custom properties? Add the part, and the
                        // collection of properties now.
                        customProps = document.AddCustomFilePropertiesPart();
                        customProps.Properties =
                            new DocumentFormat.OpenXml.CustomProperties.Properties();
                    }

                    var props = customProps.Properties;
                    if (props != null)
                    {
                        // This will trigger an exception if the property's Name 
                        // property is null, but if that happens, the property is damaged, 
                        // and probably should raise an exception.
                        var prop =
                            props.Where(
                            p => ((CustomDocumentProperty)p).Name.Value
                                == propertyName).FirstOrDefault();

                        // Does the property exist? If so, get the return value, 
                        // and then delete the property.
                        if (prop != null)
                        {
                            returnValue = prop.InnerText;
                        }
                    }
                }
                return returnValue;
            }
            else if (extension == ".xlsx" || extension == ".xlsm" || extension == ".xltx" || extension == ".xltm")
            {
                using (var document = SpreadsheetDocument.Open(fileName, true))
                {
                    var customProps = document.CustomFilePropertiesPart;
                    if (customProps == null)
                    {
                        // No custom properties? Add the part, and the
                        // collection of properties now.
                        customProps = document.AddCustomFilePropertiesPart();
                        customProps.Properties =
                            new DocumentFormat.OpenXml.CustomProperties.Properties();
                    }

                    var props = customProps.Properties;
                    if (props != null)
                    {
                        // This will trigger an exception if the property's Name 
                        // property is null, but if that happens, the property is damaged, 
                        // and probably should raise an exception.
                        var prop =
                            props.Where(
                            p => ((CustomDocumentProperty)p).Name.Value
                                == propertyName).FirstOrDefault();

                        // Does the property exist? If so, get the return value, 
                        // and then delete the property.
                        if (prop != null)
                        {
                            returnValue = prop.InnerText;
                        }

                    }
                }
                return returnValue;

            }
            else if (extension == ".pptx" || extension == "pptm" || extension == ".potx" || extension == "potm" || extension == ".ppam" || extension == ".ppsm" || extension == ".sldx" || extension == ".sldm")
            {
                using (var document = PresentationDocument.Open(fileName, true))
                {
                    var customProps = document.CustomFilePropertiesPart;
                    if (customProps == null)
                    {
                        // No custom properties? Add the part, and the
                        // collection of properties now.
                        customProps = document.AddCustomFilePropertiesPart();
                        customProps.Properties =
                            new DocumentFormat.OpenXml.CustomProperties.Properties();
                    }

                    var props = customProps.Properties;
                    if (props != null)
                    {
                        // This will trigger an exception if the property's Name 
                        // property is null, but if that happens, the property is damaged, 
                        // and probably should raise an exception.
                        var prop =
                            props.Where(
                            p => ((CustomDocumentProperty)p).Name.Value
                                == propertyName).FirstOrDefault();

                        // Does the property exist? If so, get the return value, 
                        // and then delete the property.
                        if (prop != null)
                        {
                            returnValue = prop.InnerText;
                        }
                    }
                }
                return returnValue;
            }
            return null;
        }

    }
}
