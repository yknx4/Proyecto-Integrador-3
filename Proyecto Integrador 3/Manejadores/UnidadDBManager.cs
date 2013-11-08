using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.SqlServerCe;
using System.Windows;
using Proyecto_Integrador_3.TiposDato;
using Proyecto_Integrador_3.Generadores;
using System.Data.SqlClient;
using Proyecto_Integrador_3.dsUnidadTableAdapters;

namespace Proyecto_Integrador_3
{
   public partial class DBManagers
    {

        protected dsUnidad mdsUnidades = new dsUnidad();
        protected UnidadTableAdapter mUnidadlesTableAdapter = new UnidadTableAdapter();

        public class UnidadDBManager : DBManager<Unidad>
        {

            public UnidadDBManager(DBManagers sender)
                : base(sender)
            {

            }

            private void notifyError()
            {
                throw new Exception(Error);
            }

            private Random mRandom = new Random();

            public override void AddToDB()
            {
                AddToDataset();
                UpdateDBFromDataset();
            }

            public override void AddToDataset(){
                if (!Active())
                {
                    Error = "No hay ningun usuario seleccionado";
                    notifyError();
                }
                if (heldItem.isAdded())
                {
                    Error = "La unidad con GUID " + heldItem.Uid.ToString() + " ya está en la base de datos";
                    notifyError();
                }
                heldItem.NoUnidad = RandChar.Next() + mRandom.Next(100,999).ToString();
                heldItem.Uid = Guid.NewGuid();
                dsUnidad.UnidadRow nuevoUsuario = getUnidadRow();
                Parent.mdsUnidades.Unidad.AddUnidadRow(nuevoUsuario);
            }

            public override void UpdateDBFromDataset()
            {
                Parent.LastMessage = Parent.mUnidadlesTableAdapter.Update(Parent.mdsUnidades).ToString();
            }

            private dsUnidad.UnidadRow getUnidadRow()
            {
                dsUnidad.UnidadRow nuevoUsuario = Parent.mdsUnidades.Unidad.NewUnidadRow();
                nuevoUsuario.Uid = heldItem.Uid;
                nuevoUsuario.NoUnidad = heldItem.NoUnidad;
                return nuevoUsuario;
            }


            public override void itemModified(object sender, PropertyChangedEventArgs e)
            {
                //base.itemModified(sender, e);
                //switch (e.PropertyName)
                //{
                //    case "Nombre":
                //        modificarDato(ModifiableValues.Name);
                //        break;

                //    case "Cuenta":
                //        modificarDato(ModifiableValues.ID);
                //        break;

                //    case "Plantel":
                //        modificarDato(ModifiableValues.Campus);
                //        break;
                //    default:
                //        throw new NotImplementedException(e.PropertyName+" hasn't been implemented.");
                //}
                //Clear();
            }

            public override void modificarDato()
            {
                if (!Active())
                {
                    Error = "No hay ningun usuario seleccionado";
                    notifyError();
                }
                int index = Parent.mdsUsuarios.Usuarios.Rows.IndexOf(Parent.mdsUnidades.Unidad.FindByUid(heldItem.Uid));
                Parent.mdsUnidades.Unidad.Rows[index].ItemArray = getUnidadRow().ItemArray;
                Parent.LastMessage = Parent.mUnidadlesTableAdapter.Update(Parent.mdsUnidades).ToString();
                Parent.Refresh();
            }
        }
    }
}
