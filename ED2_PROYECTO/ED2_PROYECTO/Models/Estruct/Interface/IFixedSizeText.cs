using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ED2_PROYECTO.Models.Estruct.Interface
{
    public interface IFixedSizeText
    {
        int FixedSizeText { get; set; }
        string ToFixedSizeString();
        string ToNullFormat();
    }
}
