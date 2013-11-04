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

            public UsuarioDBManager()
                : base()
            {

            }

            public override bool AddToDB()
            {
                bool finalizo = true;
                if (!Active())
                {
                    Error = "No hay ningun usuario seleccionado";
                    return false;
                }
                if (heldItem.isAdded())
                {
                    Error = "El usuario con ID " + heldItem.Uid.ToString() + " ya está en la base de datos";
                    return false;
                }
                //connection.Open();
                // Build the query statement using parameterized query.
                //string sql = "INSERT INTO Usuarios(Nombre, FechaNacimiento, sexo, TipoUsuario, TipoSangre, Calle, NumeroCalle, Colonia, Municipio, Telefono, Celular, Alergias, NombreContacto, TelefonoContacto, TarjetaAsignada, Uid, Saldo) VALUES        (N@Nombre, @FechaNacimiento, @Sexo, @TipoUsuario, @TipoSangre, N@Calle, @NumeroCalle, N@Colonia, @Municipio, N@Telefono, N@Celular, N@Alergias, N@NombreContacto, N@TelefonoContacto, N@Tarjeta, @GUID, 0)";
                string sql = "INSERT INTO Usuarios(Nombre, FechaNacimiento, sexo, TipoUsuario, TipoSangre, Calle, NumeroCalle, Colonia, Municipio, Telefono, Celular, Alergias, NombreContacto, TelefonoContacto, TarjetaAsignada, Uid, Saldo) VALUES (@Nombre, @FechaNacimiento, @Sexo, @TipoUsuario, @TipoSangre, @Calle, @NumeroCalle, @Colonia, @Municipio, @Telefono, @Celular, @Alergias, @NombreContacto, @TelefonoContacto, @Tarjeta, @GUID, 0)";

                using (SqlCeCommand cmd = new SqlCeCommand(sql, connection))
                {
                    // Create the parameter objects as specific as possible.
                    cmd.Parameters.Add("@GUID", System.Data.SqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@Calle", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@Telefono", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@TipoSangre", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@Alergias", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@NombreContacto", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@TelefonoContacto", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@TipoUsuario", System.Data.SqlDbType.TinyInt);
                    cmd.Parameters.Add("@Saldo", System.Data.SqlDbType.Money);
                    cmd.Parameters.Add("@Tarjeta", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@Sexo", System.Data.SqlDbType.Bit);
                    cmd.Parameters.Add("@FechaNacimiento", System.Data.SqlDbType.DateTime);
                    cmd.Parameters.Add("@NumeroCalle", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@Colonia", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@Municipio", System.Data.SqlDbType.SmallInt);
                    cmd.Parameters.Add("@Celular", System.Data.SqlDbType.NVarChar, 100);

                    heldItem.Saldo = 0;
                    heldItem.Uid = Guid.NewGuid();

                    // Add the parameter values.  Validation should have already happened.
                    cmd.Parameters["@GUID"].Value = heldItem.Uid;
                    cmd.Parameters["@Nombre"].Value = heldItem.Nombre;
                    cmd.Parameters["@Calle"].Value = heldItem.mDomicilio.Calle;
                    cmd.Parameters["@Telefono"].Value = heldItem.Telefono;
                    cmd.Parameters["@TipoSangre"].Value = heldItem.TipoSangre;
                    cmd.Parameters["@Alergias"].Value = heldItem.Alergias;
                    cmd.Parameters["@NombreContacto"].Value = heldItem.mContacto.Nombre;
                    cmd.Parameters["@TelefonoContacto"].Value = heldItem.mContacto.Telefono;
                    cmd.Parameters["@TipoUsuario"].Value = heldItem.TipoUsuario;
                    cmd.Parameters["@Saldo"].Value = heldItem.Saldo;
                    cmd.Parameters["@Tarjeta"].Value = heldItem.TarjetaAsignada;
                    cmd.Parameters["@Sexo"].Value = heldItem._sexo;
                    cmd.Parameters["@FechaNacimiento"].Value = heldItem.FechaNacimiento;
                    cmd.Parameters["@NumeroCalle"].Value = heldItem.mDomicilio.Numero;
                    cmd.Parameters["@Colonia"].Value = heldItem.mDomicilio.Colonia;
                    cmd.Parameters["@Municipio"].Value = heldItem.mDomicilio.Municipio;
                    cmd.Parameters["@Celular"].Value = heldItem.Celular;


                    try
                    {
                        connection.Open();
                        //var userId = cmd.ExecuteScalar();
                        // MessageBox.Show(cmd.CommandText);

                        cmd.ExecuteNonQuery();

                    }
                    catch (SqlCeException sx)
                    {
                        Error = sx.ToString();
                        // Handle exceptions before moving on.
                        finalizo = false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                return finalizo;
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

            public override bool modificarDato(UInt32 modificar)
            {
                return false;
                //UPDATE Alumnos SET Nombre = N'Jorge Figueroa Perez' WHERE (Alumnos.NumeroCuenta = 20094894)//
                //if (!Active()) return false;
                //int numeroCuentaQuery = 0;
                //string valorAModificarQuery = "";
                //string valorNuevoQuery = "";
                //switch (modificar)
                //{
                //    case ModifiableValues.Name:
                //        numeroCuentaQuery = heldItem.numeroCuenta;
                //        valorAModificarQuery = "Nombre";
                //        valorNuevoQuery = heldItem.Nombre;
                //        break;

                //    case ModifiableValues.ID:
                //        numeroCuentaQuery = heldItem.CuentaOriginal();
                //        valorAModificarQuery = "NumeroCuenta";
                //        valorNuevoQuery = heldItem.numeroCuenta.ToString();
                //        break;

                //    case ModifiableValues.Campus:
                //        numeroCuentaQuery = heldItem.numeroCuenta;
                //        valorAModificarQuery = "Plantel";
                //        valorNuevoQuery = heldItem.plantel;
                //        break;

                //    default:
                //        throw new NotImplementedException();
                //}
                //connection.Open();

                //SqlCeCommand cmd = new SqlCeCommand("UPDATE Alumnos SET " + valorAModificarQuery + " = N'" + valorNuevoQuery + "' WHERE (Alumnos.NumeroCuenta = " + numeroCuentaQuery.ToString() + ")", connection);

                //try
                //{
                //    cmd.ExecuteNonQuery();
                //    //MessageBox.Show(ModifiableValues.Value[modificar] + " modificado por " + valorNuevoQuery + ".");
                //}
                //catch (System.InvalidOperationException ex)
                //{
                //    //MessageBoxResult mes = MessageBox.Show("Query: " + cmd.CommandText + " - " + ex.ToString());
                //    connection.Close();
                //    return false;
                //}
                //catch (SqlCeException ex)
                //{
                //    //MessageBoxResult mes = MessageBox.Show("Query: " + cmd.CommandText + " - " + ex.ToString());
                //    connection.Close();
                //    return false;
                //}
                //connection.Close();
                //return true;
            }
        }
    }
}
