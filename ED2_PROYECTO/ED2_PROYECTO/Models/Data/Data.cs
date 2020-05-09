using ED2_PROYECTO.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Data
{
    public class Data
    {
        private static Data _instance = null;
        public static Data Instance
        {
            get
            {
                if (_instance == null) _instance = new Data();
                return _instance;
            }
        }

        internal ArbolProducto<Producto> arbolP = new ArbolProducto<Producto>(7);
        internal ArbolSucursal<Sucursal> arbolS = new ArbolSucursal<Sucursal>(7);
        internal ArbolSucursalProducto<SucursalProducto> arbolSP = new ArbolSucursalProducto<SucursalProducto>(7); 
    }
}
