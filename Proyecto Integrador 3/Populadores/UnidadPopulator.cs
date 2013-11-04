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
            
            //public void cuentaModificada(object sender, PropertyChangedEventArgs e)
            //{
            //    mUsuarioDBManager.itemModified(sender, e);
            //}

            public UnidadPopulator(DBManagers sender)
            {
                Parent = sender;
                
                generarLista();
            }





            public void generarLista()
            {
                _unidades.Clear();
                foreach (UnidadRow Row in Parent.mdsUnidades.Unidad.Rows)
                {
                    Unidad actual = new Unidad
                    {
                        Uid = Row.Uid,
                        NoUnidad= Row.NoUnidad ,
                        
                    };
                    //actual.PropertyChanged += cuentaModificada;
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
