
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using ED2_PROYECTO.Models.Objects;
using ED2_PROYECTO.Models.Data;
using ED2_PROYECTO.Models.Interfaces;
using System.Data;
using Microsoft.AspNetCore.Authentication;
using ED2_PROYECTO.Models.Functioning;

namespace ED2_PROYECTO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ProyectoController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        public static int id = 10;
        public ProyectoController(IWebHostEnvironment environment)
        {
            _environment = environment;

        }
        public class FileUploadAPI
        {
            public IFormFile files { get; set; }
        }

        [HttpPost("BstarTree/insert/producto")]
        public IEnumerable<Producto> PostProduct([FromForm]Data data)
        {
            
            AddProductos.AgregarProucto(data.Ruta, new Producto { ID_Producto = id ,Nombre =  data.Nombre, Precio =  data.Precio});
            id++;
            string nombre = "CompresoProducto";
            if (data.Comprimir.ToUpper() == "SI")
            {
                ComprimirArbol.Compirimir(data.Ruta + "ArbolProducto.txt", ref nombre);
            }

            return new List<Producto>();
            
        }

        [HttpPost("BstarTree/insert/productosucursal")]
        public IEnumerable<Producto> PostProductSucursal([FromForm]Data data)
        {

            AddProductoSucursal.AgregarProuctoSucursal(data.Ruta, new SucursalProducto { ID_Producto = id, ID_Sucursal = id, CantidadInventario = data.Cantidad.ToString() });
            id++;
            string nombre = "CompresoPS";
            if (data.Comprimir.ToUpper() == "SI")
            {
                ComprimirArbol.Compirimir(data.Ruta + "ArbolProductoScucursal.txt", ref nombre);
            }

            return new List<Producto>();


        }
        [HttpPost("BstarTree/insert/sucursal")]
        public IEnumerable<Producto> PostSucursal([FromForm]Data data)
        {

            AddSucursal.AgregarSucursal(data.Ruta, new Sucursal { ID_Sucursal = id, Nombre = data.Nombre, Direccion = data.Direccion });
            id++;
            string nombre = "CompresoSucursal";
            if (data.Comprimir.ToUpper() == "SI")
            {
                ComprimirArbol.Compirimir(data.Ruta + "ArbolSucursal.txt", ref nombre);
            }
            return new List<Producto>();

        }
        [HttpPost("BstarTree/insert/productFile")]
        public IEnumerable<Producto> PostProductFile([FromForm]Data data)
        {
           
            if (data.Archivo.Length > 0)
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                }
                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + data.Archivo.FileName))
                {
                    data.Archivo.CopyTo(fileStream);
                    fileStream.Flush();
                    fileStream.Close();
                    string s = @_environment.WebRootPath;
                 
                    AddProductoArchivo.AgregarProducto(fileStream.Name, data.Ruta);
                    id++;
                    string texto = System.IO.File.ReadAllText(fileStream.Name);
                    return new List<Producto>();
                }

            }
            else
            {
                return new List<Producto>();
            }
          

        }

    }
}