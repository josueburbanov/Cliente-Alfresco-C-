using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Modelos.Utils
{
    class TypeException : Exception
    {
        public int Codigo { get; set; }
        public TypeException(string message)
            : base(message)
        { }

        public TypeException(int codigo)
        {
            Codigo = codigo;
        }
    }
}
