using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Integrador_3.TiposDato
{
    public partial class Tarjeta
    {
        private long _uid;
        public long Uid { get { return _uid; } set { _uid = value; } }

        public virtual Usuario Usuario { get; set; }
    }
}
