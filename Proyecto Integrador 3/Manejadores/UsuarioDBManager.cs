using Proyecto_Integrador_3.dsUsuariosTableAdapters;
using Proyecto_Integrador_3.TiposDato;
using System;
using System.ComponentModel;

namespace Proyecto_Integrador_3
{
    public partial class DBManagers
    {
        protected dsUsuarios mdsUsuarios = new dsUsuarios();
        protected UsuariosTableAdapter mUsuariosTableAdapter = new UsuariosTableAdapter();

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
                if (heldItem.isAdded())
                {
                    Error = "El usuario con ID " + heldItem.Uid.ToString() + " ya está en la base de datos";
                    notifyError();
                }
                AddToDB(Guid.NewGuid());
            }

            public void AddToDB(Guid Uid)
            {
                
                
                
                AddToDataset(Uid);
                UpdateDBFromDataset();
                
            }
            public Usuario getItem() {
                return heldItem;
            }

            public void AddToDataset(Guid Uid) {
                if (!Active())
                {
                    Error = "No hay ningun usuario seleccionado";
                    notifyError();
                }
                heldItem.Saldo = 0;
                heldItem.Uid = Uid;
                dsUsuarios.UsuariosRow nuevoUsuario = getUsuarioRow();
                Parent.mdsUsuarios.Usuarios.AddUsuariosRow(nuevoUsuario);

            }

            public void UpdateDBFromDataset() {
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