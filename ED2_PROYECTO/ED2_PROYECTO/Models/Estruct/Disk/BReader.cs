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

        public static int LeerRaiz(string ruta)
        {
            var buffer = new byte[12];
            using (var fs = new FileStream(ruta, FileMode.OpenOrCreate))
            {
                fs.Read(buffer, 0, 12);
            }

            return int.Parse(ByteGenerator.ConvertToString(buffer));
        }
        public static int LeerPosicionDisponible(string ruta)
        {
            var buffer = new byte[12];
            using (var fs = new FileStream(ruta, FileMode.OpenOrCreate))
            {
                fs.Seek(12, SeekOrigin.Begin);
                fs.Read(buffer, 0, 12);
            }

            return int.Parse(ByteGenerator.ConvertToString(buffer));
        }
        //public static string LeerNodo(string ruta, int posicion)
        //{
        //    var buffer = new byte[BStarTreeNode<T>.FixedSize];
        //    using (var fs = new FileStream(ruta, FileMode.OpenOrCreate))
        //    {
        //        fs.Seek(24 + ((posicion - 1) * BStarTreeNode<T>.FixedSize), SeekOrigin.Begin);
        //        fs.Read(buffer, 0, BStarTreeNode<T>.FixedSize);
        //    }

        //    return ByteGenerator.ConvertToString(buffer);
        //}

    }
}
