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
    }
}
