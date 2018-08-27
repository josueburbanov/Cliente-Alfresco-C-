
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.CMM.ModelosPersonalizados
{
    interface IModeloPersonalizadoServicio
    {
        Task<string> ObtenerModelosPersonalizados();
        Task<string> ObtenerModeloPersonalizado(string nombreModelo);
        Task<string> CrearModeloPersonalizado(string modelo);
        Task<string> ActualizarModeloPersonalizado(string nombreModelo, string modelo);
        Task<string> ActivarModeloPersonalizado(string nombreModelo);
        Task<string> DesactivarModeloPersonalizado(string nombreModelo);
        Task<string> EliminarModeloPersonalizado(string nombreModelo);

    }
}
