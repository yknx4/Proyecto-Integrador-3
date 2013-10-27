using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Integrador_3.TiposDato
{
    public partial class Tarjeta
    {
        public long Uid { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
