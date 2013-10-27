using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Integrador_3.TiposDato
{
    public partial class Unidad
    {
        public Unidad()
        {
            this.Servicios = new HashSet<Servicio>();
        }

        public System.Guid Uid { get; set; }
        public string NoUnidad { get; set; }

        public virtual ICollection<Servicio> Servicios { get; set; }
    }
}
