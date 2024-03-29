﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Modelos.Utils
{
    class ModelException : Exception
    {
        public int Codigo { get; set; }
        public ModelException(string message)
            : base(message)
        { }
        public ModelException(int codigo)
        {
            Codigo = codigo;
        }
    }
}
