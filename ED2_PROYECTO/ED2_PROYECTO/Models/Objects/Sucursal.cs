﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Objects
{
    public class Sucursal : Producto
    { 
        public int ID_Sucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string Direccion { get; set; }
    }
}
