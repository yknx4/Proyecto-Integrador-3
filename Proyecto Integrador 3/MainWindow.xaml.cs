﻿using MahApps.Metro.Controls;
using Proyecto_Integrador_3.Reportes;
using Proyecto_Integrador_3.TiposDato;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    /*public System.Guid Uid { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string TipoSangre { get; set; }
        public string Alergias { get; set; }
        public string Contacto { get; set; }
        public string TelefonoContacto { get; set; }
        public short TipoUsuario { get; set; }
        public int Saldo { get; set; }*/

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

        private void SetBackgroundWorkers()
        {
            mBackgroundGenerarLista.DoWork += _backgroundWorker_DoWork_GenerarLista;
            mBackgroundGenerarLista.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted_GenerarLista; // Run the Background Worker
            mBackgroundRegistrar.DoWork += _backgroundWorker_DoWork_Registro;
            mBackgroundRegistrar.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted_Registro;
        }

        private void SetEventHandlers()
        {
            //txtNumeroTarjetaRecarga.TextChanged+=Helpers.validarTarjeta;
        }

        private void generarLista()
        {
            Helpers.DisableControls((Panel)tbRecarga.Content);
            mBackgroundGenerarLista.RunWorkerAsync();
        }

        private void _backgroundWorker_DoWork_GenerarLista(object sender, DoWorkEventArgs e)
        {
            mUsuariosPopulator.generarLista();
        }

        private void _backgroundWorker_RunWorkerCompleted_GenerarLista(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                lblEstadoPrincipal.Content = "Evento Cancelado";

                //statusText.Text = "Cancelled";
            }
            else if (e.Error != null)
            {
                //statusText.Text = "Exception Thrown";
            }
            else
            {
                Usuarios = mUsuariosPopulator.Usuarios;

                //.Text = "Completed";
            }
            Helpers.EnableControls((Panel)tbRecarga.Content);
        }

        

        private void cambiaTextoBusquedaAsync(object sender)
        {
            dtgrdBusqueda.Visibility = Visibility.Hidden;
            TextBox origen = sender as TextBox;
            if (origen.Text != "")
            {
                UsuariosBusqueda = (from usuarios in Usuarios where usuarios.sNombre.ToLower().Contains(origen.Text.ToLower()) select usuarios).ToList();
                dtgrdBusqueda.ItemsSource = UsuariosBusqueda;
                if (UsuariosBusqueda.Count > 0) dtgrdBusqueda.Visibility = Visibility.Visible;

                //txtNumeroTarjetaRecarga.IsReadOnly = true;
            }
            else
            {
                //txtNumeroTarjetaRecarga.IsReadOnly = false;
            }
        }

        private void cambiaTextoBusqueda(object sender, TextChangedEventArgs e)
        {
        }

        //private void clickCheckboxRangoFecha(object sender, RoutedEventArgs e)
        //{
        //    if (((sender as CheckBox).IsChecked).Value)
        //    {
        //        dtpFechaReporteFinal.IsEnabled = true;
        //    }
        //    else
        //    {
        //        dtpFechaReporteFinal.IsEnabled = false;
        //    }
        //}

        private void entraTexto(object sender, TextCompositionEventArgs e)
        {
        }

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

        private Usuario generarUsuario(int i)
        {
            Usuario.Contacto tmpContacto = new Usuario.Contacto
            {
                Nombre = DateTime.Now.ToShortDateString(),
                Telefono = "312123456"
            };
            Usuario.Domicilio tmpDomicilio = new Usuario.Domicilio
            {
                Calle = "Calle",
                Colonia = "Colonia",
                Municipio = 1,
                Numero = 123
            };

            return new Usuario
            {
                Uid = Guid.Empty,
                Sexo = "Hombre",
                Alergias = "Alergias",
                Celular = "312123",
                FechaNacimiento = DateTime.Now,
                Nombre = DateTime.Now.ToShortDateString(),
                TarjetaAsignada = txtNumeroTarjeta.Text,
                TipoSangre = (short)(cmbSangre.SelectedIndex),
                TipoUsuario = (byte)(cmbTipos.SelectedIndex + 1),
                Telefono = "3123123",
                mContacto = tmpContacto,
                mDomicilio = tmpDomicilio
            };
        }

        private void limpiarVentanaRegistro()
        {
            cmbSangre.SelectedItem = Tipos.Sangre.Last();
            cmbMunicipio.SelectedItem = Tipos.Municipios.Last();
            Helpers.ClearTextBoxes((Panel)grdRegistro);
        }

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

        private void _backgroundWorker_DoWork_Registro(object sender, DoWorkEventArgs e)
        {
            mUsuarioDBManager.setItem((Usuario)e.Argument);
            mUsuarioDBManager.AddToDB();
        }

        private void _backgroundWorker_RunWorkerCompleted_Registro(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                lblEstadoPrincipal.Content = "Evento Cancelado";

                //statusText.Text = "Cancelled";
            }
            else if (e.Error != null)
            {
                lblEstadoPrincipal.Content = "Error al registrar. " + e.Error.ToString();

                //statusText.Text = "Exception Thrown";
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

                //.Text = "Completed";
            }
            Helpers.EnableControls((Panel)grdRegistro);
            pgrRegistrar.IsActive = false;
        }

        private void PresionarTecla_BusqNom(object sender, KeyEventArgs e)
        {
        }

        private void ventanaCambiaTamaño(object sender, SizeChangedEventArgs e)
        {
            lblEstadoPrincipal.Content = e.NewSize.ToString();
        }

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
                    ReporteFrecuenciaDeUso mReporteFrecuencia = new ReporteFrecuenciaDeUso(ref mDBManagers);
                    mReporteFrecuencia.ShowDialog();
                    break;

            }
            
            
            System.GC.Collect();
        }

        private void alDobleClickBusqueda(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Doble Click"+dtgrdBusqueda.SelectedCells.Count.ToString());
            if (dtgrdBusqueda.SelectedItems.Count == 1)
            {
                currentUsuario = (Usuario)dtgrdBusqueda.SelectedItems[0];
                txtNumeroTarjetaRecarga.Text = currentUsuario.TarjetaAsignada;
            }
        }

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