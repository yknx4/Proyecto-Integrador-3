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
using UnidadRow = Proyecto_Integrador_3.dsUnidad.UnidadRow;
using System.Windows;

namespace Proyecto_Integrador_3
{
    partial class DBManagers
    {
        public class UnidadPopulator
        {
            private List<Unidad> _unidades = new List<Unidad>();
            private DBManagers Parent;
            private static ServiciosPopulator mServiciosPopulator;

            public UnidadPopulator(DBManagers sender)
            {
                Parent = sender;
                mServiciosPopulator = new ServiciosPopulator(Parent);
                mServiciosPopulator.generarLista();
                generarLista();
            }

            public void clear()
            {
                _unidades.Clear();
                System.GC.Collect();

            }

            public void generarLista()
            {
                _unidades.Clear();
                List<Servicio> tmpServicios;
                foreach (UnidadRow Row in Parent.mdsUnidades.Unidad.Rows)
                {
                    Unidad actual = new Unidad
                    {
                        Uid = Row.Uid,
                        NoUnidad= Row.NoUnidad ,
                        
                    };
                    //actual.PropertyChanged += cuentaModificada;
                    
                     tmpServicios = (from servicio in mServiciosPopulator.Servicios where servicio.Unidad == actual.Uid select servicio).ToList();
                    //MessageBox.Show(tmpServicios.Count.ToString());
                    actual.Servicios = tmpServicios;
                    _unidades.Add(actual);
                }

            }

            public List<Unidad> Unidades
            {
                set { throw new NotImplementedException(); }
                get { return _unidades; }
            }
            
        }
    }
}
