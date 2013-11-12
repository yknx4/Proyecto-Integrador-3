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
                    Usuario.Contacto tmpContacto = new Usuario.Contacto
                    {
                        Nombre = Row.NombreContacto,
                        Telefono = Row.TelefonoContacto
                    };
                    Usuario.Domicilio tmpDomicilio = new Usuario.Domicilio
                    {
                        Calle = Row.Calle,
                        Colonia = Row.Colonia,
                        Municipio = Row.Municipio,
                        Numero = Row.NumeroCalle
                    };

                    Usuario actual = new Usuario
                    {
                        Uid = Row.Uid,
                        Saldo = Row.Saldo,
                        Sexo = Row.sexo.ToString(),
                        Alergias = Row.Alergias,
                        Celular = Row.Celular,
                        FechaNacimiento = Row.FechaNacimiento,
                        Nombre = Row.Nombre,
                        TarjetaAsignada = Row.TarjetaAsignada,
                        TipoSangre = Row.TipoSangre,
                        TipoUsuario = Row.TipoUsuario,
                        Telefono = Row.Telefono,
                        mContacto = tmpContacto,
                        mDomicilio = tmpDomicilio
                    };

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
            //public void generarLista(int i)
            //{

            //    UsuariosQueryResult.Clear();
            //    _usuarios = new List<Usuario>();
            //    SqlCeDataAdapter adap = new SqlCeDataAdapter("SELECT Usuarios.* FROM Usuarios", conn);
            //    adap.Fill(UsuariosQueryResult);

            //    /*Inicial variables temporales para cada Usuario en la DB*/

            //    Guid Uid;
            //    String Nombre;
            //    String Calle;
            //    String Telefono;
            //    short TipoSangre;
            //    String Alergias;
            //    String NombreContacto;
            //    String TelefonoContacto;
            //    short TipoUsuario;
            //    decimal Saldo;
            //    string TarjetaAsignada;
            //    bool Sexo;
            //    DateTime FechaNacimiento;
            //    int NumeroCalle;
            //    string Colonia;
            //    short Municipio;
            //    String Celular;

            //    /* ##END## */
            //    foreach (DataRow Row in UsuariosQueryResult.Rows)
            //    {
            //        // int accountValue = Convert.ToInt32(Row["NumeroCuenta"].ToString());
            //        // string nmb = Row["Nombre"].ToString();
            //        //string ptl = Row["Plantel"].ToString();
            //        Uid = (Guid)Row["Uid"];
            //        Nombre = (String)Row["Nombre"];
            //        Calle = (String)Row["Calle"];
            //        Telefono = (String)Row["Telefono"];
            //        TipoSangre = (short)Row["TipoSangre"];
            //        Alergias = (String)Row["Alergias"];
            //        NombreContacto = (String)Row["NombreContacto"];
            //        TelefonoContacto = (String)Row["TelefonoContacto"];
            //        TipoUsuario = (short)Row["TipoUsuario"];
            //        Saldo = (Decimal)Row["Saldo"];
            //        TarjetaAsignada = (string)Row["TarjetaAsignada"];
            //        Sexo = (bool)Row["sexo"];
            //        FechaNacimiento = (DateTime)Row["FechaNacimiento"];
            //        NumeroCalle = (int)Row["NumeroCalle"];
            //        Colonia = (String)Row["Colonia"];
            //        Municipio = (short)Row["Municipio"];
            //        Celular = (String)Row["Celular"];

            //        //DataTable Asistencias = new DataTable();
            //        //SqlCeDataAdapter asist = new SqlCeDataAdapter("SELECT Asistencias.* FROM Asistencias WHERE (idClub = " + clubSeleccionado.Id + ") AND (idAlumno = " + accountValue + ") AND (parcial = " + parcial + ")", conn);
            //        //asist.Fill(Asistencias);
            //        //List<Asistencia> Asistencia = new List<Asistencia>();
            //        //foreach (DataRow aRow in Asistencias.Rows)
            //        //{
            //        //    DateTime tmpDate;
            //        //    try
            //        //    {
            //        //        tmpDate = Convert.ToDateTime(aRow["date"]);
            //        //    }
            //        //    catch (System.InvalidCastException)
            //        //    {
            //        //        tmpDate = new DateTime(0);
            //        //    }
            //        //    Asistencia tmpAsistencia = new Asistencia()
            //        //    {
            //        //        //ID = Convert.ToInt32(aRow["id"].ToString()),
            //        //        Date = tmpDate,
            //        //        Parcial = parcial,
            //        //    };
            //        //    Asistencia.Add(tmpAsistencia);
            //        //}

            //        Usuario.Contacto tmpContacto = new Usuario.Contacto
            //        {
            //            Nombre = NombreContacto,
            //            Telefono = TelefonoContacto
            //        };
            //        Usuario.Domicilio tmpDomicilio = new Usuario.Domicilio
            //        {
            //            Calle = Calle,
            //            Colonia = Colonia,
            //            Municipio = Municipio,
            //            Numero = NumeroCalle
            //        };

            //        Usuario actual = new Usuario
            //        {
            //            Sexo = Sexo.ToString(),
            //            Alergias = Alergias,
            //            Celular = Celular,
            //            FechaNacimiento = FechaNacimiento,
            //            Nombre = Nombre,
            //            TarjetaAsignada = TarjetaAsignada,
            //            TipoSangre = TipoSangre,
            //            TipoUsuario = TipoUsuario,
            //            Telefono = Telefono,
            //            mContacto = tmpContacto,
            //            mDomicilio = tmpDomicilio
            //        };
            //        //actual.PropertyChanged += cuentaModificada;
            //        _usuarios.Add(actual);
            //    }
            //}
        }
    }
}
