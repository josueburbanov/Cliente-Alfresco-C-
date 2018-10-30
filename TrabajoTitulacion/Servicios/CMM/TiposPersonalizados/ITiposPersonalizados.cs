using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.CMM.TiposPersonalizados
{
    interface ITiposPersonalizados
    {
        Task<string> ObtenerTipos(string nombreModelo);
        Task<string> ObtenerTipo(string nombreModelo, string nombreTipo);
        Task<string> ActualizarTipo(string nombreModelo, string nombreTipo, string tipo);
        Task<string> CrearTipo(string nombreModelo, string tipo);
        Task<string> EliminarTipo(string nombreModelo, string nombreTipo);
    }
}
