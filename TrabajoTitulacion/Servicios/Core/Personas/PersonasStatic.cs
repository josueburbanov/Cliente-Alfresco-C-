using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoTitulacion.Modelos.CoreAPI;

namespace TrabajoTitulacion.Servicios.Core.Personas
{
    class PersonasStatic
    {
        public static PersonasServicio servicioPersonas = new PersonasServicio();
        public static Person PersonaAutenticada { get; set; }
        public async static Task<Person> ObtenerPersona(string idUsuario)
        {
            //Se serializa y deserializa con el fin de mapear correctamente los campos de Persona
            string respuestaJson = await servicioPersonas.ObtenerPersona(idUsuario);
            dynamic respuestaDeserializada = JsonConvert.DeserializeObject(respuestaJson);
            string personaJson = JsonConvert.SerializeObject(respuestaDeserializada.entry);
            PersonaAutenticada = JsonConvert.DeserializeObject<Person>(personaJson);
            return PersonaAutenticada;
            
        }
    }
}
