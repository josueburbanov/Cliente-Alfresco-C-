using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Modelos.Utils
{
    class ModelException : Exception
    {
        public ModelException(string message)
            : base(message)
        { }

    }
}
