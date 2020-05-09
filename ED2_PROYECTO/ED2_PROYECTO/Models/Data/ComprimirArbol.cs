using ED2_PROYECTO.Models.Estruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Data
{
    public class ComprimirArbol
    {

        public static LZW lzw = new LZW();

        public static void Compirimir(string rutatexto, ref string nombre)
        {
            lzw.Comprimir(rutatexto, ref nombre);
        }



    }
}
