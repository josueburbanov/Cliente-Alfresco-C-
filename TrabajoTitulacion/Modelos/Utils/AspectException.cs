using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Modelos.Utils
{
    class AspectException:Exception
    {
        public int Codigo { get; set; }
        public AspectException(string message)
            : base(message)
        { }
        public AspectException(int codigo)
        {
            Codigo = codigo;
        }
    }
}