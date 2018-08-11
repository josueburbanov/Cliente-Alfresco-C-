using System.Collections.Generic;

namespace TrabajoTitulacion.Modelos.CMM
{
    public class Type1
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; } //Etiqueta de presentación
        public string ParentName { get; set; }
        public string PrefixedName { get; set; }
        public string Datatype { get; set; }
        public List<Property> Properties { get; set; }
    }
}