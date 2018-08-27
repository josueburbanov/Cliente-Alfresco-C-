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
        Task<string> ActualizarTipoPersonalizado(string nombreModelo, string nombreTipo, string tipo);
        Task<string> CrearTipoPersonalizado(string nombreModelo, string tipo);
        Task<string> EliminarTipoPersonalizado(string nombreModelo, string nombreTipo);
    }
}
