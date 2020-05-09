using ED2_PROYECTO.Models.Estruct;
using ED2_PROYECTO.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Data
{
    public class ArbolSucursalProducto
    {

        public static BStarTree<SucursalProducto> arbol = new BStarTree<SucursalProducto>(7);
        public static SDES sdes = new SDES();
        public static int id = 0;
        public static void AgregarProucto(string path, SucursalProducto sucursalproducto)
        {
            id = sucursalproducto.ID_Sucursal;
            id = sucursalproducto.ID_Producto;
            sucursalproducto.CantidadInventario = sdes.Encriptar(id, sucursalproducto.CantidadInventario.ToString();
            
            arbol.ruta = path;
            arbol.insertElement(sucursalproducto);

        }


    }
}
