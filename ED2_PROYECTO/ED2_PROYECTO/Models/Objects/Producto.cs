using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Objects
{
    public class Producto : IComparable
    {
        public int ID_Producto { get; set; }
        public string Nombre { get; set; }
        public string Precio { get; set; }
        public int CompareTo(object obj)
        {
            return this.ID_Producto.CompareTo(((Producto)obj).ID_Producto);
        }
    }
}
