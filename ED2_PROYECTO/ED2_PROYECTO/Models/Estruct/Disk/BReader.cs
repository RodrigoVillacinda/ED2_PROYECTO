using ED2_PROYECTO.Models.Estruct.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Estruct.Disk
{
    public class BReader<T> where T : IFixedSizeText
    {
        public static void LeerEncabezado(string ruta, ref int raiz, ref int nuevaPosicion)
        {
            var buffer = new byte[BStarTreeNode<T>.FixedSize];
            using (var fs = new FileStream(ruta, FileMode.OpenOrCreate))
            {
                fs.Read(buffer, 0, BStarTreeNode<T>.FixedSize);
            }

            raiz = int.Parse(ByteGenerator.ConvertToString(buffer));

            buffer = new byte[BStarTreeNode<T>.FixedSize];
            using (var fs = new FileStream(ruta, FileMode.OpenOrCreate))
            {
                fs.Seek(12, SeekOrigin.Begin);
                fs.Read(buffer, 0, BStarTreeNode<T>.FixedSize);
            }

            nuevaPosicion = int.Parse(ByteGenerator.ConvertToString(buffer));
        }
        public static void Leer(string ruta, int posicion)
        {

        }
    }
}
