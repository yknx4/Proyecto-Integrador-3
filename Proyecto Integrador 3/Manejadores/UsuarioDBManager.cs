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
    partial class DBManagers
    {
        public class UsuarioDBManager : DBManager<Usuario>
        {

            public UsuarioDBManager(DBManagers sender)
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
                    Error = "No hay ningun usuario seleccionado";
                    notifyError();
                }
                if (heldItem.isAdded())
                {
                    Error = "El usuario con ID " + heldItem.Uid.ToString() + " ya está en la base de datos";
                    notifyError();
                }
                heldItem.Saldo = 0;
                heldItem.Uid = Guid.NewGuid();
                dsUsuarios.UsuariosRow nuevoUsuario = getUsuarioRow();
                Parent.mdsUsuarios.Usuarios.AddUsuariosRow(nuevoUsuario);
                Parent.LastMessage = Parent.mUsuariosTableAdapter.Update(Parent.mdsUsuarios).ToString();
                Parent.Refresh();
                
                

                
            }

            private dsUsuarios.UsuariosRow getUsuarioRow()
            {
                dsUsuarios.UsuariosRow nuevoUsuario = Parent.mdsUsuarios.Usuarios.NewUsuariosRow();
                nuevoUsuario.Uid = heldItem.Uid;
                nuevoUsuario.Nombre = heldItem.Nombre;
                nuevoUsuario.Calle = heldItem.mDomicilio.Calle;
                nuevoUsuario.Telefono = heldItem.Telefono;
                nuevoUsuario.TipoSangre = heldItem.TipoSangre;
                nuevoUsuario.Alergias = heldItem.Alergias;
                nuevoUsuario.NombreContacto = heldItem.mContacto.Nombre;
                nuevoUsuario.TelefonoContacto = heldItem.mContacto.Telefono;
                nuevoUsuario.TipoUsuario = heldItem.TipoUsuario;
                nuevoUsuario.Saldo = heldItem.Saldo;
                nuevoUsuario.TarjetaAsignada = heldItem.TarjetaAsignada;
                nuevoUsuario.sexo = heldItem._sexo;
                nuevoUsuario.FechaNacimiento = heldItem.FechaNacimiento;
                nuevoUsuario.NumeroCalle = heldItem.mDomicilio.Numero;
                nuevoUsuario.Colonia = heldItem.mDomicilio.Colonia;
                nuevoUsuario.Municipio = heldItem.mDomicilio.Municipio;
                nuevoUsuario.Celular = heldItem.Celular;

                return nuevoUsuario;
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
                    Error = "No hay ningun usuario seleccionado";
                    notifyError();
                }
               /* dsUsuarios.UsuariosRow usuarioAModificar = Parent.mdsUsuarios.Usuarios.FindByUid(heldItem.Uid);
                usuarioAModificar = getUsuarioRow();*/
                int index = Parent.mdsUsuarios.Usuarios.Rows.IndexOf(Parent.mdsUsuarios.Usuarios.FindByUid(heldItem.Uid));
                Parent.mdsUsuarios.Usuarios.Rows[index].ItemArray = getUsuarioRow().ItemArray;
                Parent.LastMessage = Parent.mUsuariosTableAdapter.Update(Parent.mdsUsuarios).ToString();
                Parent.Refresh();
            }
        }
    }
}
