using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.CMM.TiposPersonalizados
{
    interface ITiposPersonalizadosServicio
    {
        Task<string> ObtenerTiposPersonalizados(string nombreModelo);
        Task<string> ObtenerTipoPersonalizado(string nombreModelo, string nombreTipo);
    }
}
