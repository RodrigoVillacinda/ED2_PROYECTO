
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace ED2_PROYECTO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ProyectoController : ControllerBase
    {
        public static IWebHostEnvironment _environment;

        public ProyectoController(IWebHostEnvironment environment)
        {
            _environment = environment;

        }
        public class FileUploadAPI
        {
            public IFormFile files { get; set; }
        }

        [HttpPost("BstarTree/insert/Producto")]
        public IEnumerable<Producto> PostDrinks([FromBody] Producto bebida)
        {
            ED2_PROYECTO.Models.Data.Data.Instance.arbolP.Add(Producto);
            D2_LABFINAL.Models.Btree.Data.Instance.arbol.Insertar(D2_LABFINAL.Models.Btree.Data.Instance.bebidas);
            return D2_LABFINAL.Models.Btree.Data.Instance.bebidas;
        }



    }
}