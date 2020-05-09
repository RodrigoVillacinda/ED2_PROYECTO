using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Interfaces
{
    public interface IData<T>
    {
        IFormFile Archivo { get; set; }
        T Nombre { get; set; }
        T Direccion { get; set; }
        T Producto { get; set; }
        T Precio { get; set; }
        T Cantidad { get; set; }
        T Ruta { get; set; }
        T Comprimir { get; set; }
        


    }
}
