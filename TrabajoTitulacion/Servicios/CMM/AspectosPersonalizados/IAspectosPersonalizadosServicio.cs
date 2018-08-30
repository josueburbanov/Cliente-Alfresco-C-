using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.CMM.AspectosPersonalizados
{
    interface IAspectosPersonalizadosServicio
    {
        Task<string> ObtenerAspectosPersonalizados(string nombreModelo);
        Task<string> CrearAspectoPersonalizado(string nombreModelo, string aspecto);
        Task<string> ObtenerAspectoPersonalizado(string nombreModelo, string nombreAspecto);
        Task<string> ActualizarAspectoPersonalizado(string nombreModelo, string nombreAspecto, string aspecto);
        Task<string> EliminarAspectoPersonalizado(string nombreModelo, string nombreAspecto);
        Task<string> AñadirPropiedadAspecto(string nombreModelo, string nombreAspecto, string propertiesBodyUpdate);
        Task<string> ActualizarPropiedadAspecto(string nombreModelo, string nombreAspecto, string nombrePropiedad, string propiedadActualizar);
        Task<string> EliminarPropiedadAspecto(string nombreModelo, string nombreAspecto, string nombrePropiedad);
    }
}
