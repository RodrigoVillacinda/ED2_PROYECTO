using ED2_PROYECTO.Models.Estruct;
using ED2_PROYECTO.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Data
{
    public static class ArbolProducto
    {
        public static int id = 0;
        public static SDES sdes = new SDES();
        public static BStarTree<Producto> arbol = new BStarTree<Producto>(7);

        public static void AgregarProucto(string path, Producto producto)
        {
            id = producto.ID_Producto;
            producto.Nombre = sdes.Encriptar(id, producto.Nombre);
            producto.Precio = Double.Parse( sdes.Encriptar(id, producto.Precio.ToString()));
            arbol.ruta = path;
            arbol.insertElement(producto);
        
        }

    }
}
