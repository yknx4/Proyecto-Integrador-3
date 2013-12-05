using System;
using System.Collections.Generic;

namespace Proyecto_Integrador_3.TiposDato
{
    /// <summary>
    /// Modelo del usuario
    /// </summary>
    public partial class Usuario : DBItem
    {
        public struct Domicilio
        {
            public string Calle { get; set; }

            public int Numero { get; set; }

            public string Colonia { get; set; }

            public short Municipio { get; set; }
        }

        public struct Contacto
        {
            public string Nombre { get; set; }

            public string Telefono { get; set; }
        }

        public Usuario()
        {
            this.TipoUsuario = 1;
            this.Saldo = 0;
            this.Servicios = new HashSet<Servicio>();
        }

        public DateTime FechaNacimiento { get; set; }

        public System.Guid Uid { get; set; }

        public string sNombre
        {
            get
            {
                return Nombre.Replace('&', ' ');
            }
        }

        public string Nombre { get; set; }

        public Domicilio mDomicilio { get; set; }

        public string Telefono { get; set; }

        public string Celular { get; set; }

        public int TipoSangre { get; set; }

        public string Alergias { get; set; }

        public Contacto mContacto { get; set; }

        public byte TipoUsuario { get; set; }

        public string sTipoUsuario
        {
            get
            {
                return Tipos.Usuarios[TipoUsuario];
            }
        }

        public decimal Saldo { get; set; }

        public string TarjetaAsignada { get; set; }

        public bool _sexo;

        public string Sexo { get { return _sexo ? "Hombre" : "Mujer"; } set { if (value.ToLower() == "hombre" || value == "True") { _sexo = true; } else { _sexo = false; } } }

        public virtual ICollection<Servicio> Servicios { get; set; }

        public virtual int ServiciosCount
        {
            get
            {
                if (Servicios == null)
                {
                    return 0;
                }
                else
                {
                    return Servicios.Count;
                }
            }
        }

        public virtual Decimal Consumo
        {
            get
            {
                if (Servicios == null)
                {
                    return 0;
                }
                else
                {
                    decimal consumo = 0;
                    foreach (Servicio serv in Servicios)
                    {
                        if (serv.TipoUsuario == 1)
                        {
                            consumo += Constantes.PrecioNormal;
                        }
                        else
                        {
                            consumo += Constantes.PrecioEspecial;
                        }
                    }
                    return consumo;
                }
            }
        }

        public override bool isAdded()
        {
            return !(Uid == Guid.Empty);
        }

        public override string ToString()
        {
            return sNombre;
        }
    }
}