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

namespace Proyecto_Integrador_3
{
   public partial class DBManagers
    {
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
                
                if (!Active())
                {
                    Error = "No hay ningun servicio seleccionado";
                    notifyError();
                }
                if (heldItem.isAdded())
                {
                    Error = "El servicio con ID " + heldItem.Uid.ToString() + " ya está en la base de datos";
                    notifyError();
                }
                heldItem.Saldo = 0;
                heldItem.Uid = Guid.NewGuid();
                dsServicios.ServiciosRow nuevoServicio = getServicioRow();
                Parent.mdsServicios.Servicios.AddServiciosRow(nuevoServicio);
                Parent.LastMessage = Parent.mServiciosTableAdapter.Update(Parent.mdsServicios).ToString();
                Parent.Refresh();
                
                

                
            }

            private dsServicios.ServiciosRow getServicioRow()
            {
                dsServicios.ServiciosRow nuevoServicio = Parent.mdsServicios.Servicios.NewServiciosRow();
                nuevoServicio.Uid = heldItem.Uid;
                nuevoServicio.Nombre = heldItem.Nombre;
                nuevoServicio.Calle = heldItem.mDomicilio.Calle;
                nuevoServicio.Telefono = heldItem.Telefono;
                nuevoServicio.TipoSangre = heldItem.TipoSangre;
                nuevoServicio.Alergias = heldItem.Alergias;
                nuevoServicio.NombreContacto = heldItem.mContacto.Nombre;
                nuevoServicio.TelefonoContacto = heldItem.mContacto.Telefono;
                nuevoServicio.TipoServicio = heldItem.TipoServicio;
                nuevoServicio.Saldo = heldItem.Saldo;
                nuevoServicio.TarjetaAsignada = heldItem.TarjetaAsignada;
                nuevoServicio.sexo = heldItem._sexo;
                nuevoServicio.FechaNacimiento = heldItem.FechaNacimiento;
                nuevoServicio.NumeroCalle = heldItem.mDomicilio.Numero;
                nuevoServicio.Colonia = heldItem.mDomicilio.Colonia;
                nuevoServicio.Municipio = heldItem.mDomicilio.Municipio;
                nuevoServicio.Celular = heldItem.Celular;

                return nuevoServicio;
            }

            public bool anadirServicio(Servicio servicio)
            {

                if (!Active()) return false;
                //connection.Open();
                //SqlCeCommand cmd = new SqlCeCommand("INSERT INTO Asistencias(idClub, idAlumno, parcial, date)VALUES(@club, @cuenta, @parcial, @date)", connection);
                //cmd.Parameters.AddWithValue("@club", club);
                //cmd.Parameters.AddWithValue("@cuenta", heldItem.numeroCuenta);
                //cmd.Parameters.AddWithValue("@parcial", parcial);
                //cmd.Parameters.AddWithValue("@date", DateTime.Now);

                ////MessageBox.Show(DateTime.Now.Ticks);
                //try
                //{
                //    cmd.ExecuteNonQuery();
                //}
                //catch (System.InvalidOperationException ex)
                //{
                //    //MessageBoxResult mes = MessageBox.Show(ex.ToString());
                //    connection.Close();
                //    return false;
                //}
                //catch (SqlCeException ex)
                //{
                //    //MessageBoxResult mes = MessageBox.Show(ex.ToString());
                //    connection.Close();
                //    return false;
                //}
                //connection.Close();

                //Holder.asistencias++;
                //heldItem.Asistencias.Add(new Asistencia()
                //{
                //    Date = DateTime.Now,
                //    Parcial = parcial,
                //});

                return true;
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
                int index = Parent.mdsServicios.Servicios.Rows.IndexOf(Parent.mdsServicios.Servicios.FindByUid(heldItem.Uid));
                Parent.mdsServicios.Servicios.Rows[index].ItemArray = getServicioRow().ItemArray;
                Parent.LastMessage = Parent.mServiciosTableAdapter.Update(Parent.mdsServicios).ToString();
                Parent.Refresh();
            }
        }
    }
}
