using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.Group
{
    interface IGrupos
    {
        Task<string> ObtenerMiembrosGrupo(string idGrupo);
        Task<string> AñadirMiembrosGrupo(string idGrupo, string groupMembershipBodyCreate);
    }
}
