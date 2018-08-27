using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.Core.Nodos
{
    interface INodosServicio
    {
        Task<string> ObtenerListaNodosHijos(string nodoPadre);
        Task<string> ObtenerNodo(string idNodo);
        void ObtenerContenido(string idNodo, string path);
        Task<string> ActualizarNodo(string idNodo, string nodoJson);
    }
}
