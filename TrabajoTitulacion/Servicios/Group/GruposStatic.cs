using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos.CoreAPI;
using Newtonsoft.Json;

namespace TrabajoTitulacion.Servicios.Group
{
    class GruposStatic
    {
        static GruposServicio gruposServicio = new GruposServicio();
        public async static Task<GroupMember> AñadirMiembrosGrupo(string idGrupo, GroupMembershipBodyCreate groupMembershipBodyCreate)
        {
            string groupMembershipBodyCreateJson = JsonConvert.SerializeObject(groupMembershipBodyCreate);
            string respuestaJson = await gruposServicio.AñadirMiembrosGrupo(idGrupo, groupMembershipBodyCreateJson);

            //Se deserializa y luego serializa para obtener una lista de nodos (Elimina metadatos de descarga)
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string groupMemberJson = JsonConvert.SerializeObject(respuestaDeserializada.entry);
            GroupMember groupMemberListo = JsonConvert.DeserializeObject<GroupMember>(groupMemberJson);
            return groupMemberListo;
        }

        public async static Task<List<GroupMember>> ObtenerMiembrosGrupoAdministradorModelos()
        {
            string respuestaJson = await gruposServicio.ObtenerMiembrosGrupo("GROUP_ALFRESCO_MODEL_ADMINISTRATORS");

            //Se deserializa y luego serializa para obtener una lista de nodos (Elimina metadatos de descarga)
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string miembrosGrupoJson = JsonConvert.SerializeObject(respuestaDeserializada.list.entries);
            dynamic miembrosNoMapeados = JsonConvert.DeserializeObject(miembrosGrupoJson);

            List<GroupMember> miembrosGrupo = new List<GroupMember>();

            //Se itera para mapear cada nodo de la lista de nodos y añadirlos como hijos de cada nodo
            foreach (var miembro in miembrosNoMapeados)
            {
                string nodoJson = JsonConvert.SerializeObject(miembro.entry);
                GroupMember miembroGrupoLimpio = JsonConvert.DeserializeObject<GroupMember>(nodoJson);
                miembrosGrupo.Add(miembroGrupoLimpio);
            }

            return miembrosGrupo;
        }
    }
}
