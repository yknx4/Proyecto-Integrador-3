using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Integrador_3.TiposDato
{
    public partial class Usuario
    {
        public Usuario()
        {
            this.TipoUsuario = 1;
            this.Saldo = 0;
            this.Servicios = new HashSet<Servicio>();
        }

        public System.Guid Uid { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string TipoSangre { get; set; }
        public string Alergias { get; set; }
        public string Contacto { get; set; }
        public string TelefonoContacto { get; set; }
        public short TipoUsuario { get; set; }
        public int Saldo { get; set; }
        public long TarjetaAsignada { get; set; }
        private bool _sexo;
        public string Sexo { get { return _sexo ? "Hombre" : "Mujer"; } set { if (value.ToLower() == "hombre" || value == "True") { _sexo = true; } else { _sexo = false; } } }

        public virtual ICollection<Servicio> Servicios { get; set; }
        public virtual Tarjeta Tarjetas { get; set; }
    }

}
