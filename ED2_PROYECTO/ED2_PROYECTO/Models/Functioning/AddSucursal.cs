using ED2_PROYECTO.Models.Estruct;
using ED2_PROYECTO.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Functioning
{
    public class AddSucursal
    {
        public static BStarTree<Sucursal> arbol = new BStarTree<Sucursal>(7);
        public static SDES sdes = new SDES();
        public static int id = 0;
        public static void AgregarSucursal(string path, Sucursal sucursal)
        {
            id = sucursal.ID_Sucursal;
            sucursal.Nombre = Cifrar(id, sucursal.Nombre);
            sucursal.Direccion = Cifrar(id, sucursal.Direccion);
            arbol.ruta = path + "ArbolSucursal.txt";
            arbol.insertElement(sucursal);
            arbol.InsertarEnDisco(sucursal);

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
