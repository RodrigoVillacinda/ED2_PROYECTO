using ED2_PROYECTO.Models.Estruct;
using ED2_PROYECTO.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Functioning
{
    public class AddProductoSucursal
    {
        public static BStarTree<SucursalProducto> arbol = new BStarTree<SucursalProducto>(7);
        public static SDES sdes = new SDES();
        public static int id = 0;
        public static void AgregarProuctoSucursal(string path, SucursalProducto sucursalproducto)
        {
            id = sucursalproducto.ID_Sucursal;
            id = sucursalproducto.ID_Producto;
            sucursalproducto.CantidadInventario = Cifrar(  id, sucursalproducto.CantidadInventario.ToString() ).ToString();

            arbol.ruta = path + "ArbolProductoScucursal.txt";
            arbol.insertElement(sucursalproducto);
            arbol.InsertarEnDisco(sucursalproducto);

        }
        public static string Cifrar(int key, string text)
        {
            SDES sdes = new SDES(key);
            List<char> lista = new List<char>();
            string cifrado = "";
            char[] array = new char[text.Length];
            array = text.ToCharArray();
            for (int i = 0; i < text.Length; i++)
            {
                lista.Add(sdes.Cifrado(array[i]));
            }
            cifrado = string.Join(" ", lista);
            cifrado = cifrado.Replace(" ", "");
            return cifrado;
        }
    }
}
