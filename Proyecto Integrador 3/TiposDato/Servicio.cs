using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Integrador_3.TiposDato
{
    public partial class Servicio
    {
        private int _id;
        public int Id { get; set; }
        private short _tipoUsuario;
        public short TipoUsuario { get; set; }
        private System.Guid _unidad;
        public System.Guid Unidad { get; set; }
        private System.Guid _usuario;
        public System.Guid Usuario { get; set; }
        private DateTime _fecha;
        public System.DateTime Fecha { get; set; }

        public virtual Unidad Unidad1 { get; set; }
        public virtual Usuario Usuarios { get; set; }
    }
}
