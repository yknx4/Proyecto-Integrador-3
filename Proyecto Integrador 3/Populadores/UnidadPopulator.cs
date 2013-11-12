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

            public UnidadPopulator(ref DBManagers sender, bool Servicios)
            {
                this.Servicios = Servicios;
                Parent = sender;
                
            }

            public void clear()
            {
                _unidades.Clear();
                System.GC.Collect();

            }


            bool Servicios;

            void generarServicios()
            {
                Parent.FillServicios();
                mServiciosPopulator = new ServiciosPopulator(ref Parent);
                mServiciosPopulator.generarLista();
                //generarLista();

            }


            public void generarLista()
            {
                _unidades.Clear();
                
                //mServiciosPopulator = new ServiciosPopulator(Parent);
                //mServiciosPopulator.generarLista();
                if (Servicios) generarServicios();
                List<Servicio> tmpServicios ;
                foreach (UnidadRow Row in Parent.mdsUnidades.Unidad.Rows)
                {
                    Unidad actual = new Unidad
                    {
                        Uid = Row.Uid,
                        NoUnidad= Row.NoUnidad ,
                        
                    };
                    //actual.PropertyChanged += cuentaModificada;

                    if (Servicios)
                    {
                        tmpServicios = (from servicio in mServiciosPopulator.Servicios where servicio.Unidad == actual.Uid select servicio).ToList();
                        
                        actual.Servicios = tmpServicios;
                    }
                    _unidades.Add(actual);
                }
                //MessageBox.Show(mServiciosPopulator.Servicios.Count.ToString());
                if (Servicios) mServiciosPopulator.clear();
                if (Servicios) Parent.ClearServicios();
                //MessageBox.Show(mServiciosPopulator.Servicios.Count.ToString());
                //MessageBox.Show(_unidades.Count.ToString());
            }

            public List<Unidad> Unidades
            {
                get { return _unidades; }
            }
            
        }
    }
}
