using Proyecto_Integrador_3.TiposDato;
using System;
using System.Collections.Generic;
using ServiciosRow = Proyecto_Integrador_3.dsServicios.ServiciosRow;

namespace Proyecto_Integrador_3
{
    partial class DBManagers
    {
        public class ServiciosPopulator
        {
            private List<Servicio> _servicios = new List<Servicio>();
            private DBManagers Parent;

            public ServiciosPopulator(ref DBManagers sender)
            {
                Parent = sender;

                generarLista();
            }

            public void clear()
            {
                _servicios = new List<Servicio>();
                System.GC.Collect();
            }

            public void generarLista()
            {
                _servicios.Clear();
                foreach (ServiciosRow Row in Parent.mdsServicios.Servicios.Rows)
                {
                    Servicio actual = new Servicio
                    {
                        //Id=Row.id,
                        TipoUsuario = short.Parse(Row.tipoUsuario),
                        Unidad = Row.unidad,
                        Usuario = Row.usuario,
                        Fecha = Row.fecha
                    };

                    //actual.PropertyChanged += cuentaModificada;
                    _servicios.Add(actual);
                }
            }

            public List<Servicio> Servicios
            {
                set { throw new NotImplementedException(); }
                get { return _servicios; }
            }
        }
    }
}