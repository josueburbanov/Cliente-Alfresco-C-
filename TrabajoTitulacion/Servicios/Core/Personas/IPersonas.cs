﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Servicios.Core.Personas
{
    interface IPersonas
    {
        Task<string> ObtenerPersona(string idUsuario);
    }
}
