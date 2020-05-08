using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Objects
{
    public class SucursalProducto :  Sucursal, IComparable 
    {
        public int CantidadInventario { get; set; }
        public int CompareTo(object obj)
        {
            return this.ID_Producto.CompareTo(((SucursalProducto)obj).ID_Sucursal);
        }
    }
}
