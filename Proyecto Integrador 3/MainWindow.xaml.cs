using MahApps.Metro.Controls;
using Proyecto_Integrador_3.Reportes;
using Proyecto_Integrador_3.TiposDato;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using UsuarioDBManager = Proyecto_Integrador_3.DBManagers.UsuarioDBManager;
using UsuariosPopulator = Proyecto_Integrador_3.DBManagers.UsuariosPopulator;

namespace Proyecto_Integrador_3
{
    /// <summary>
    /// Logica de interacción para la ventana principal
    /// </summary>
    ///


    public partial class MainWindow : MetroWindow
    {
        //private static SqlCeConnection conn = new SqlCeConnection(@ConfigurationManager.ConnectionStrings["Proyecto_Integrador_3.Properties.Settings.ProyectoIntegradorConnectionString"].ConnectionString);

        private static DBManagers mDBManagers = new DBManagers();

        private static UsuarioDBManager mUsuarioDBManager = new UsuarioDBManager(mDBManagers);

        private static UsuariosPopulator mUsuariosPopulator;

        private List<Usuario> Usuarios;

        private Usuario currentUsuario;

        private List<Usuario> UsuariosBusqueda;

        private BackgroundWorker mBackgroundGenerarLista = new BackgroundWorker();
        private BackgroundWorker mBackgroundRegistrar = new BackgroundWorker();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MainWindow"/>.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            /*Carga los datos de la DB*/

            //mDBManagers.Fill();
            mDBManagers.FillUsuarios();
            /*Se establecen los valores predeterminados*/
            cmbTipos.ItemsSource = (from values in Tipos.Usuarios.Values select values).ToList();
            cmbSangre.ItemsSource = Tipos.Sangre;
            cmbSangre.SelectedItem = Tipos.Sangre.Last();
            cmbMunicipio.ItemsSource = Tipos.Municipios;
            cmbMunicipio.SelectedItem = Tipos.Municipios.Last();
            dtpFechaNacimiento.SelectedDate = DateTime.Today.AddYears(-18);
            
            /*Se genera la primera tarjeta del día*/
            txtNumeroTarjeta.Text = Generadores.CardGenerator.Next().ToString();
            /*Se genera la estructura del Datagrid*/
            mUsuariosPopulator = new UsuariosPopulator(ref mDBManagers, false);
            dgtcNombre.Binding = new Binding("sNombre");
            dgtcNumeroTarjeta.Binding = new Binding("TarjetaAsignada");
            dgtcSaldo.Binding = new Binding("Saldo");
            dgtcSexo.Binding = new Binding("Sexo");
            Binding bTipoUsuario = new Binding("sTipoUsuario");
            dgtcTipoUsuario.Binding = bTipoUsuario;
            /*Se asignan los hilos de fondo*/
            SetBackgroundWorkers();
            SetEventHandlers();
            Helpers.SetValidationTextBoxes(grdRegistro);
            
            /*Se genera la primera lista*/
            generarLista();
        }

        /// <summary>
        /// Evento que se dispara al cerrar la ventana de reporte.
        /// </summary>
        /// <param name="sender">Quien dispara el evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> conteniendo la información del evento.</param>
        private void alCerrarReporte(object sender, EventArgs e)
        {
            mReporteFrecuencia = null;
            System.GC.Collect();
        }

        /// <summary>
        /// Asigna el trabajo, a los BackgroundWorkers.
        /// </summary>
        private void SetBackgroundWorkers()
        {
            mBackgroundGenerarLista.DoWork += _backgroundWorker_DoWork_GenerarLista;
            mBackgroundGenerarLista.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted_GenerarLista; // Run the Background Worker
            mBackgroundRegistrar.DoWork += _backgroundWorker_DoWork_Registro;
            mBackgroundRegistrar.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted_Registro;
        }



        /// <summary>
        /// Genera la lista de usuarios. Para esto deshabilida los controles de Recarga, para evitar errores de referencia nula.
        /// </summary>
        private void generarLista()
        {
            Helpers.DisableControls((Panel)tbRecarga.Content);
            mBackgroundGenerarLista.RunWorkerAsync();
        }

        private void _backgroundWorker_DoWork_GenerarLista(object sender, DoWorkEventArgs e)
        {
            mUsuariosPopulator.generarLista();
        }

        /// <summary>
        /// Realiza las funciones cuando se termina de ejecutar la accion de fondo GeneralLista.
        /// </summary>
        /// <param name="sender">El origen del evento.</param> 
        /// <param name="e">La instancia <see cref="RunWorkerCompletedEventArgs"/> con los datos del evento.</param>
        private void _backgroundWorker_RunWorkerCompleted_GenerarLista(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                lblEstadoPrincipal.Content = "Evento Cancelado";

               /* //statusText.Text = "Cancelled";*/
            }
            else if (e.Error != null)
            {
                throw e.Error;
            }
            else
            {
                Usuarios = mUsuariosPopulator.Usuarios;
              /*  //.Text = "Completed";*/
            }
            Helpers.EnableControls((Panel)tbRecarga.Content);
        }



        /// <summary>
        /// Metodo asíncrono para procesar texto de la búsqueda.
        /// </summary>
        /// <param name="sender">El origen.</param>
        private void cambiaTextoBusquedaAsync(object sender)
        {
            dtgrdBusqueda.Visibility = Visibility.Hidden;
            TextBox origen = sender as TextBox;
            if (origen.Text != "")
            {
                UsuariosBusqueda = (from usuarios in Usuarios where usuarios.sNombre.ToLower().Contains(origen.Text.ToLower()) select usuarios).ToList();
                dtgrdBusqueda.ItemsSource = UsuariosBusqueda;
                if (UsuariosBusqueda.Count > 0) dtgrdBusqueda.Visibility = Visibility.Visible;

                /*//txtNumeroTarjetaRecarga.IsReadOnly = true;*/
            }
            else
            {
                /*//txtNumeroTarjetaRecarga.IsReadOnly = false;*/
            }
        }




        /// <summary>
        /// Genera un usuario en base a los datos de la ventana de registro
        /// </summary>
        /// <returns>Una instancia de Usuario con los datos de la ventana de registro</returns>
        private Usuario generarUsuario()
        {
            Usuario.Contacto tmpContacto = new Usuario.Contacto
            {
                Nombre = txtNombreDeContacto.Text,
                Telefono = txtTelefonoDeContacto.Text
            };
            Usuario.Domicilio tmpDomicilio = new Usuario.Domicilio
            {
                Calle = txtCalle.Text,
                Colonia = txtColonia.Text,
                Municipio = (short)cmbMunicipio.SelectedIndex,
                Numero = int.Parse(txtNumero.Text)
            };

            return new Usuario
            {
                Uid = Guid.Empty,
                Sexo = rdbHombre.IsChecked.ToString(),
                Alergias = txtAlergias.Text,
                Celular = txtCelular.Text,
                FechaNacimiento = (DateTime)dtpFechaNacimiento.SelectedDate,
                Nombre = txtNombre.Text + Constantes.SeparadorNombre + txtApellidoPaterno.Text + Constantes.SeparadorNombre + txtApellidoMaterno.Text,
                TarjetaAsignada = txtNumeroTarjeta.Text,
                TipoSangre = (short)(cmbSangre.SelectedIndex),
                TipoUsuario = (byte)(cmbTipos.SelectedIndex + 1),
                Telefono = txtTelefono.Text,
                mContacto = tmpContacto,
                mDomicilio = tmpDomicilio
            };
        }



        /// <summary>
        /// Limpiar la ventana registro.
        /// </summary>
        private void limpiarVentanaRegistro()
        {
            cmbSangre.SelectedItem = Tipos.Sangre.Last();
            cmbMunicipio.SelectedItem = Tipos.Municipios.Last();
            Helpers.ClearTextBoxes((Panel)grdRegistro);
        }

        /// <summary>
        /// Realiza lo necesario al realizar click en el botón de registrar.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onClickRegistrar(object sender, RoutedEventArgs e)
        {
            if (!validarRegistro())
            {
                return;
            }
            ((Control)sender).IsEnabled = false;
            Helpers.DisableControls((Panel)grdRegistro);
            pgrRegistrar.IsEnabled = true;
            pgrRegistrar.IsActive = true;
            Usuario usuarioNuevo = generarUsuario();
            mBackgroundRegistrar.RunWorkerAsync(usuarioNuevo);
        }

        /// <summary>
        /// Realiza de manera asíncrona lo necesario para realizar un registro en la base de datos.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        private void _backgroundWorker_DoWork_Registro(object sender, DoWorkEventArgs e)
        {
            mUsuarioDBManager.setItem((Usuario)e.Argument);
            mUsuarioDBManager.AddToDB();
        }

        /// <summary>
        /// Realiza lo necesario cuando se realiza un registro de manera exitosa.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void _backgroundWorker_RunWorkerCompleted_Registro(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                lblEstadoPrincipal.Content = "Evento Cancelado";

             /*   //statusText.Text = "Cancelled";*/
            }
            else if (e.Error != null)
            {
                lblEstadoPrincipal.Content = "Error al registrar. " + e.Error.ToString();
                
                /*//statusText.Text = "Exception Thrown";*/
            }
            else
            {
                lblEstadoPrincipal.Content = mUsuarioDBManager.getItem().sNombre + " registrado correctamente.";
                lblEstadoSecundaria.Content = mDBManagers.LastMessage + " filas han sido actualizadas.";
                limpiarVentanaRegistro();
                txtNumeroTarjeta.Text = Generadores.CardGenerator.Next().ToString();
                btnRegistrar.IsEnabled = true;
                mUsuarioDBManager.Refresh();
                generarLista();

               /* //.Text = "Completed";*/
            }
            Helpers.EnableControls((Panel)grdRegistro);
            pgrRegistrar.IsActive = false;
        }



        /// <summary>
        /// Método asíncrono para manejar el cambio de datos en la tarjeta de recarga.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void cambiaTarjetaRecargaAsync(object sender)
        {
            TextBox origen = sender as TextBox;
            if (origen.Text != "")
            {
                //txtNombreBusqueda.IsReadOnly = true;
                UsuariosBusqueda = (from usuarios in Usuarios where usuarios.TarjetaAsignada == origen.Text select usuarios).ToList();

                if (UsuariosBusqueda.Count > 0)
                {
                    currentUsuario = UsuariosBusqueda.First();
                    txtNombreBusqueda.Text = currentUsuario.sNombre;
                }
            }
            else
            {
                //txtNombreBusqueda.IsReadOnly = false;
            }
        }

        /// <summary>
        /// Maneja el evento de cambiar el texto de la tarjeta a recargar.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void cambiaTarjetaRecarga(object sender, TextChangedEventArgs e)
        {
            Action<object> del = (object s) => cambiaTarjetaRecargaAsync(s);
            ThreadStart start = delegate()
            {
                Dispatcher.Invoke(
                    DispatcherPriority.ApplicationIdle,
                   del, sender
                    );
            };

            new Thread(start).Start();
            
        }

        /// <summary>
        /// Realiza lo necesario para realizar una recarga.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void recargaClick(object sender, RoutedEventArgs e)
        {
            txtNumeroTarjetaRecarga.Background = Brushes.White;
            if (!Helpers.validarCuenta(txtNumeroTarjetaRecarga.Text) || currentUsuario==null)
            {
                txtNumeroTarjetaRecarga.Focus();
                txtNumeroTarjetaRecarga.Background = Constantes.ErrorBrush;
                return;
            }
            txtSaldoRecarga.Background = Brushes.White;
            if (!Helpers.validarDinero(txtSaldoRecarga.Text))
            {
                txtSaldoRecarga.Background = Constantes.ErrorBrush;
                txtSaldoRecarga.Focus();
                return;
            }
            lblEstadoPrincipal.Content = "";
            lblEstadoSecundaria.Content = "";
            mDBManagers.LastMessage = "";
            mUsuarioDBManager.setItem(currentUsuario);
            currentUsuario.Saldo += Decimal.Parse(txtSaldoRecarga.Text);
            lblEstadoPrincipal.Content = currentUsuario.Saldo.ToString() + " saldoTotal.";
            mUsuarioDBManager.modificarDato();
            PerformBusquedaEnter();

            //generarLista();
        }

        /// <summary>
        /// Simula el presionar 'Enter' al cuadro de texto de búsqueda.
        /// </summary>
        private void PerformBusquedaEnter()
        {
            var key = Key.Enter;
            System.Windows.Media.Visual target = txtNombreBusqueda;
            var routedEvent = Keyboard.KeyUpEvent;
            txtNombreBusqueda.RaiseEvent(
                new KeyEventArgs(
                    Keyboard.PrimaryDevice,
                    PresentationSource.FromVisual(target),
                    0,
                    key
                    )
                    {
                        RoutedEvent = routedEvent
                    }

                );
        }

        /// <summary>
        /// Maneja el Enter en el cuadro de texto de búsqueda.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void alPresionarEnterBusqueda(object sender, KeyEventArgs e)
        {
            /*http://msdn.microsoft.com/en-us/magazine/cc163328.aspx*/
            if (e.Key != Key.Enter)
            {
                return;
            }
            Action<object> del = (object s) => cambiaTextoBusquedaAsync(s);
            ThreadStart start = delegate()
            {
                Dispatcher.Invoke(
                    DispatcherPriority.ApplicationIdle,
                   del, txtNombreBusqueda
                    );
            };

            new Thread(start).Start();
        }
        ReporteFrecuenciaDeUso mReporteFrecuencia;
        /// <summary>
        /// Muesta el reporte seleccionado.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnMostrarReporte_Click(object sender, RoutedEventArgs e)
        {
            switch (cmbTipoReporte.SelectedIndex)
            {
                default:
                    break;
                case 0:
                    ReportePorUnidad mReporteUnidad = new ReportePorUnidad(ref mDBManagers);
                    mReporteUnidad.ShowDialog();
                    break;
                case 1:
                    if (mReporteFrecuencia != null)
                    {
                        
                        mReporteFrecuencia.Focus();
                    }
                    else
                    {
                        mReporteFrecuencia = new ReporteFrecuenciaDeUso(ref mDBManagers);

                        /*Se asignan Manejadores de eventos*/
                        mReporteFrecuencia.Closed += alCerrarReporte;
                        mReporteFrecuencia.Show();
                    }
                    break;

            }
            
            
            System.GC.Collect();
        }



        /// <summary>
        /// Maneja el evento de doble click en el DataGrid de la búsqueda.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void alDobleClickBusqueda(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Doble Click"+dtgrdBusqueda.SelectedCells.Count.ToString());
            if (dtgrdBusqueda.SelectedItems.Count == 1)
            {
                currentUsuario = (Usuario)dtgrdBusqueda.SelectedItems[0];
                txtNumeroTarjetaRecarga.Text = currentUsuario.TarjetaAsignada;
            }
        }

        /// <summary>
        /// Realiza las validaciones necesarias para saber si los datos introducidos en el formulario de registro son correctos.
        /// </summary>
        /// <returns>Verdadero si todos son válidos, Falso si al menos uno es inválido.</returns>
        private bool validarRegistro()
        {
            bool estado = true;
            TextBox[] validarTexto = { txtNombre, txtApellidoMaterno, txtApellidoPaterno, txtNombreDeContacto, txtCalle, txtColonia, txtApellidoMaternoDeContacto, txtApellidoPaternoDeContacto };
            TextBox[] validarNumero = {txtNumero };
            TextBox[] validarTelefono = {txtTelefono,txtTelefonoDeContacto,txtCelular };
            foreach (TextBox txt in validarTexto)
            {
                estado &= Helpers.validarTextBoxEstandar(txt);
            }
            foreach (TextBox txt in validarNumero)
            {
                estado &= Helpers.validarTextBoxNumero(txt);
            }
            foreach (TextBox txt in validarTelefono)
            {
                estado &= Helpers.validarTextBoxTelefono(txt);
            }
            
            return estado;
        }

        

       
    }
}