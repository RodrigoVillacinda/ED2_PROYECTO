using ED2_PROYECTO.Models.Estruct;
using ED2_PROYECTO.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Functioning
{
    public class UpdateDatosSucursal
    {
        public static BStarTree<Sucursal> arbol = new BStarTree<Sucursal>(7);
        public static SDES sdes = new SDES();
        public static int id = 0;
        public static void AgregarProucto(string path, Sucursal sucursal)
        {
            id = sucursal.ID_Sucursal;
            //sucursal.Nombre = sdes.Encriptar(id, sucursal.Nombre);
            //sucursal.Direccion = sdes.Encriptar(id, sucursal.Direccion);
            arbol.ruta = path;
            arbol.insertElement(sucursal);

        }

    }
}
