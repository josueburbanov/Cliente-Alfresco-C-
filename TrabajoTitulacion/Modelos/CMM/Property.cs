using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Modelos.CMM
{
    public class Property
    {
        public string Name { get; set; }
        public string Title { get; set; }//Etiqueta de presentación
        public string Datatype { get; set; }
        public object Indexed { get; set; }
        public bool Protected1 {get; set; }
        public bool Mandatory { get; set; }
        public string DefaultValue { get; set; }
        public bool Multiple { get; set; }
        public List<Constraint> Constraints { get; set; }
        public string Description { get; set; }
        public dynamic Value { get; set; }
        public string PrefixedName { get; set; }
        public string Facetable { get; set; }
        public string indexTokenisationMode { get; set; }
        public string MultiValued { get; set; }
        public string MandatoryEnforced { get; set; }

        public Property() { }

        public Property(string name, string title, string datatype)
        {
            Name = name;
            PrefixedName = name;
            Title = title;
            Datatype = datatype;
        }

        public Property(string name, string title, string description,string datatype)
        {
            Name = name;
            PrefixedName = name;
            Title = title;
            Description = description;
            Datatype = datatype;
        }
    }
}
