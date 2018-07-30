using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos;

namespace TrabajoTitulacion.Servicios.Core.Personas
{
    class PersonasStatic
    {        

        public async static Task<Person> ObtenerPersona(string idPersona)
        {
            Personas servicioPersonas = new Personas();
            //Se serializa y deserializa con el fin de mapear correctamente los campos de Persona
            string respuestaJson = await servicioPersonas.ObtenerPersona(idPersona);
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string personaJson = JsonConvert.SerializeObject(respuestaDeserializada.entry);
            return JsonConvert.DeserializeObject<Person>(personaJson);
        }
    }
}
