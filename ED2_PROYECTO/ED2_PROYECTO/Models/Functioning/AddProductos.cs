using ED2_PROYECTO.Models.Estruct;
using ED2_PROYECTO.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Functioning
{
    public static class AddProductos
    {
        public static int id = 0;
        //public static SDES sdes = new SDES(2);
        public static BStarTree<Producto> arbol = new BStarTree<Producto>(7);

        public static void AgregarProucto(string path, Producto producto)
        {
            id = producto.ID_Producto;
            producto.Nombre = Cifrar(id, producto.Nombre.ToString()); ;
            producto.Precio = Cifrar(id, producto.Precio.ToString());
            arbol.ruta = path + "ArbolProducto.txt";
            arbol.insertElement(producto);
            arbol.InsertarEnDisco(producto);
        }

        public static string Cifrar(int key, string text) {
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
