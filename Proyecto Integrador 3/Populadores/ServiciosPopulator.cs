using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using Proyecto_Integrador_3.TiposDato;

using Proyecto_Integrador_3.dsServiciosTableAdapters;
using ServiciosRow = Proyecto_Integrador_3.dsServicios.ServiciosRow;
using System.Windows;

namespace Proyecto_Integrador_3
{
    partial class DBManagers
    {
        public class ServiciosPopulator
        {
            private List<Servicio> _servicios = new List<Servicio>();
            private DBManagers Parent;
            
            

            public ServiciosPopulator(DBManagers sender)
            {
                Parent = sender;
                
                generarLista();
            }

            public void generarLista()
            {
                _servicios.Clear();
                foreach (ServiciosRow Row in Parent.mdsServicios.Servicios.Rows)
                {
                    Servicio actual = new Servicio
                    {
                        Uid = Row.Uid,
                        
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
