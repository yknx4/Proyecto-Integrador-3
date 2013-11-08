using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.SqlServerCe;
using System.Windows;
using Proyecto_Integrador_3.TiposDato;
using System.Data.SqlClient;
using Proyecto_Integrador_3.dsUsuariosTableAdapters;
using Proyecto_Integrador_3.dsServiciosTableAdapters;

namespace Proyecto_Integrador_3
{
   public partial class DBManagers
    {

        
        protected dsServicios mdsServicios = new dsServicios();
        protected ServiciosTableAdapter mServiciosTableAdapter = new ServiciosTableAdapter();

        public class ServicioDBManager : DBManager<Servicio>
        {

            public ServicioDBManager(DBManagers sender)
                : base(sender)
            {

            }

            private void notifyError()
            {
                throw new Exception(Error);
            }

            public override void AddToDB()
            {

                AddToDataset();
                UpdateDBFromDataset();
                
                
                
                
            }

            public override void AddToDataset() {
                if (!Active())
                {
                    Error = "No hay ningun servicio seleccionado";
                    notifyError();
                }
                if (heldItem.isAdded())
                {
                    Error = "El servicio con ID " + heldItem.Id.ToString() + " ya está en la base de datos";
                    notifyError();
                }
                dsServicios.ServiciosRow nuevoServicio = getServicioRow();
                Parent.mdsServicios.Servicios.AddServiciosRow(nuevoServicio);

            }
            public override void UpdateDBFromDataset() {
                Parent.LastMessage = Parent.mServiciosTableAdapter.Update(Parent.mdsServicios).ToString();
                Parent.Refresh();
            }



            private dsServicios.ServiciosRow getServicioRow()
            {
                dsServicios.ServiciosRow nuevoServicio = Parent.mdsServicios.Servicios.NewServiciosRow();
                nuevoServicio.fecha = heldItem.Fecha;
                nuevoServicio.tipoUsuario = heldItem.TipoUsuario.ToString();
                nuevoServicio.unidad = heldItem.Unidad;
                nuevoServicio.usuario = heldItem.Usuario;
                return nuevoServicio;
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
                    Error = "No hay ningun servicio seleccionado";
                    notifyError();
                }
               /* dsServicios.ServiciosRow servicioAModificar = Parent.mdsServicios.Servicios.FindByUid(heldItem.Uid);
                servicioAModificar = getServicioRow();*/
                int index = Parent.mdsServicios.Servicios.Rows.IndexOf(Parent.mdsServicios.Servicios.FindByid(heldItem.Id));
                Parent.mdsServicios.Servicios.Rows[index].ItemArray = getServicioRow().ItemArray;
                Parent.LastMessage = Parent.mServiciosTableAdapter.Update(Parent.mdsServicios).ToString();
                Parent.Refresh();
            }
        }
    }
}
