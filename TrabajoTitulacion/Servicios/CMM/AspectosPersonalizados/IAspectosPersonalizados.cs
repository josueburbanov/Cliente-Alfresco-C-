using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.CMM.AspectosPersonalizados
{
    interface IAspectosPersonalizados
    {
        Task<string> ObtenerAspectos(string nombreModelo);
        Task<string> CrearAspecto(string nombreModelo, string aspecto);
        Task<string> ObtenerAspecto(string nombreModelo, string nombreAspecto);
        Task<string> ActualizarAspecto(string nombreModelo, string nombreAspecto, string aspecto);
        Task<string> EliminarAspecto(string nombreModelo, string nombreAspecto);
        Task<string> AñadirPropiedadAspecto(string nombreModelo, string nombreAspecto, string propertiesBodyUpdate);
        Task<string> ActualizarPropiedadAspecto(string nombreModelo, string nombreAspecto, string nombrePropiedad, string propiedadActualizar);
        Task<string> EliminarPropiedadAspecto(string nombreModelo, string nombreAspecto, string nombrePropiedad);
    }
}
