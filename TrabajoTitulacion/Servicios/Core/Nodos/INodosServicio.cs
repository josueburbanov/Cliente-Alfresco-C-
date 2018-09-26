using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.Core.Nodos
{
    interface INodosServicio
    {
        Task<string> ObtenerListaNodosHijos(string nodoPadre);
        Task<string> ObtenerNodo(string idNodo);
        Task<byte[]> ObtenerContenido(string idNodo);
        Task<string> ActualizarNodo(string idNodo, string nodoJson);
        Task<string> CrearNodo(string idNodoPadre, string nodoCreate);
        Task<string> ActualizarContenido(string idNodo, bool majorVersion, string comment, string name, string[] include, string[] fields, byte[] contentBodyUpdate);
        Task<string> EliminarNodo(string idNodo);
    }
}
