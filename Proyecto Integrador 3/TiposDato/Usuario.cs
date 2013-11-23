<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Integrador_3.dsUsuariosTableAdapters;

namespace Proyecto_Integrador_3.TiposDato
{
    public partial class Usuario : DBItem
    {

        public dsUsuarios.UsuariosRow Datos;

       public class Domicilio{

           public Domicilio(ref Usuario parent) {
               this.parent = parent;
           }
           private Usuario parent;
            public string Calle{
                get {
                    return parent.Datos.Calle;
                }
                set {
                    parent.Datos.Calle = value;
                }
            }
            public int Numero
            {
                get
                {
                    return parent.Datos.NumeroCalle;
                }
                set
                {
                    parent.Datos.NumeroCalle = value;
                }
            }
            public string Colonia
            {
                get
                {
                    return parent.Datos.Colonia;
                }
                set
                {
                    parent.Datos.Colonia = value;
                }
            }
            public short Municipio
            {
                get
                {
                    return parent.Datos.Municipio;
                }
                set
                {
                    parent.Datos.Municipio = value;
                }
            }
        }
        public class Contacto{

            public Contacto(ref Usuario parent) {
               this.parent = parent;
           }
           private Usuario parent;

           public string Nombre
           {
               get
               {
                   return parent.Datos.NombreContacto;
               }
               set
               {
                   parent.Datos.NombreContacto = value;
               }
           }

           public string Telefono
           {
               get
               {
                   return parent.Datos.TelefonoContacto;
               }
               set
               {
                   parent.Datos.TelefonoContacto = value;
               }
           }

        }
        public Usuario(dsUsuarios.UsuariosRow data)
        {
            Datos = data;
            this.TipoUsuario = 1;
            this.Saldo = 0;
            this.Servicios = new HashSet<Servicio>();
        }
        public DateTime FechaNacimiento {
            get { return Datos.FechaNacimiento; }
            set { Datos.FechaNacimiento = value; }
        }
        public System.Guid Uid
        {
            get { return Datos.Uid; }
            set { Datos.Uid = value; }
        }
        public string sNombre
        {
            get
            {
                return Nombre.Replace('&', ' ');
            }
        }
        public string Nombre
        {
            get { return Datos.Nombre; }
            set { Datos.Nombre = value; }
        }
        public Domicilio mDomicilio { get; set; }
        public string Telefono
        {
            get { return Datos.Telefono; }
            set { Datos.Telefono = value; }
        }
        public string Celular
        {
            get { return Datos.Celular; }
            set { Datos.Celular = value; }
        }
        public int TipoSangre
        {
            get { return Datos.TipoSangre; }
            set { Datos.TipoSangre = value; }
        }
        public string Alergias
        {
            get { return Datos.Alergias; }
            set { Datos.Alergias = value; }
        }
        public Contacto mContacto { get; set; }
        public byte TipoUsuario
        {
            get { return Datos.TipoUsuario; }
            set { Datos.TipoUsuario = value; }
        }
        public string sTipoUsuario
        {
            get
            {
                return Tipos.Usuarios[TipoUsuario];
            }
        }
        public decimal Saldo
        {
            get { return Datos.Saldo; }
            set { Datos.Saldo = value; }
        }
        public string TarjetaAsignada
        {
            get { return Datos.TarjetaAsignada; }
            set { Datos.TarjetaAsignada = value; }
        }
        public bool bSexo {

            get { return Datos.sexo; }
            set { Datos.sexo = value; }
        }

        public string Sexo { get { return bSexo ? "Hombre" : "Mujer"; } set { if (value.ToLower() == "hombre" || value == "True") { bSexo = true; } else { bSexo = false; } } }

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
                    decimal consumo=0;
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
        public override bool isAdded() {
            return !(Uid == Guid.Empty);
        }
    }

}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Integrador_3.TiposDato
{
    public partial class Usuario : DBItem
    {
       public struct Domicilio{

            public string Calle{get;set;}
            public int Numero{get;set;}
            public string Colonia{get;set;}
            public short Municipio{get;set;}
        }
        public struct Contacto{
            public string Nombre{get;set;}
            
            public string Telefono{get;set;}

        }
        public Usuario()
        {
            this.TipoUsuario = 1;
            this.Saldo = 0;
            this.Servicios = new HashSet<Servicio>();
        }
        public DateTime FechaNacimiento {get;set;}
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
                    decimal consumo=0;
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
        public override bool isAdded() {
            return !(Uid == Guid.Empty);
        }
        public override string ToString()
        {
            return sNombre;
        }
    }

}
>>>>>>> 39c85c4... agregue busqueda
