using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Interfaces
{
    public class Data : IData<string>
    {
        public IFormFile Archivo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string  Producto { get; set; }
        public string Precio { get; set; }
        public string Cantidad { get; set; }
        public string Ruta { get; set; }
        public string Comprimir { get; set; }

        string IData<string>.Precio { get; set; }
        string IData<string>.Cantidad { get; set; }
    }
}
