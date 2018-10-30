
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.CMM.ModelosPersonalizados
{
    interface IModelosPersonalizados
    {
        Task<string> ObtenerModelos();
        Task<string> ObtenerModelo(string nombreModelo);
        Task<string> CrearModelo(string modelo);
        Task<string> ActualizarModelo(string nombreModelo, string modelo);
        Task<string> ActivarModelo(string nombreModelo);
        Task<string> DesactivarModelo(string nombreModelo);
        Task<string> EliminarModelo(string nombreModelo);
    }
}
