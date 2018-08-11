
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.CMM.ModelosPersonalizados
{
    interface IModeloPersonalizadoServicio
    {
        Task<string> ObtenerModelosPersonalizados();
        Task<string> ObtenerModeloPersonalizado(string nombreModelo);        

    }
}
