using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using Proyecto_Integrador_3.TiposDato;

using Proyecto_Integrador_3.dsUsuariosTableAdapters;
using UsuariosRow = Proyecto_Integrador_3.dsUsuarios.UsuariosRow;
using System.Windows;

namespace Proyecto_Integrador_3
{
    partial class DBManagers
    {
        public class UsuariosPopulator
        {
            private List<Usuario> _usuarios = new List<Usuario>();
            private DBManagers Parent;

            private static ServiciosPopulator mServiciosPopulator;
            
            public UsuariosPopulator(ref DBManagers sender, bool Servicios)
            {
                this.Servicios = Servicios;
                Parent = sender;
                //if (Servicios)
                //{
                //    generarServicios();
                //} 
                //else
                //    {
                //        generarLista();
                //}
                
            }

            bool Servicios;

            void generarServicios()
            {
                mServiciosPopulator = new ServiciosPopulator(ref Parent);
                mServiciosPopulator.generarLista();
                //generarLista();

            }

            public void generarLista()
            {
                if (Servicios) generarServicios();
                _usuarios.Clear();
                foreach (UsuariosRow Row in Parent.mdsUsuarios.Usuarios.Rows)
                {


                    Usuario actual = new Usuario(Row);
                    //MessageBox.Show(Row.TipoUsuario.ToString());
                    Usuario.Contacto tmpContacto = new Usuario.Contacto(ref actual);
                    //{
                    //    Nombre = Row.NombreContacto,
                    //    Telefono = Row.TelefonoContacto
                    //};
                    Usuario.Domicilio tmpDomicilio = new Usuario.Domicilio(ref actual);
                    //{
                    //    Calle = Row.Calle,
                    //    Colonia = Row.Colonia,
                    //    Municipio = Row.Municipio,
                    //    Numero = Row.NumeroCalle
                    //};

                    
                    actual.mContacto = tmpContacto;
                    actual.mDomicilio = tmpDomicilio;

                    if(Servicios)actual.Servicios = (from servicio in mServiciosPopulator.Servicios where servicio.Usuario == actual.Uid select servicio).ToList();
                    //actual.PropertyChanged += cuentaModificada;
                    _usuarios.Add(actual);
                }

            }

            public List<Usuario> Usuarios
            {
                set { throw new NotImplementedException(); }
                get { return _usuarios; }
            }
        }
    }
}
