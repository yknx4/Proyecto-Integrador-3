using MahApps.Metro.Controls;
using Proyecto_Integrador_3.TiposDato;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlServerCe;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
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
        private static SqlCeConnection conn = new SqlCeConnection(@ConfigurationManager.ConnectionStrings["Proyecto_Integrador_3.Properties.Settings.ProyectoIntegradorConnectionString"].ConnectionString);

        private static DBManagers mDBManagers = new DBManagers(conn);

        private static UsuarioDBManager mUsuarioDBManager = new UsuarioDBManager(mDBManagers);

        private static UsuariosPopulator mUsuariosPopulator;

        private List<Usuario> Usuarios;

        private List<Usuario> UsuariosBusqueda;

        private Usuario currentUsuario;

        public MainWindow()
        {
            InitializeComponent();
            cmbTipos.ItemsSource = (from values in Tipos.Usuarios.Values select values).ToList();
            cmbSangre.ItemsSource = Tipos.Sangre;
            cmbSangre.SelectedItem = Tipos.Sangre.Last();
            cmbMunicipio.ItemsSource = Tipos.Municipios;
            cmbMunicipio.SelectedItem = Tipos.Municipios.Last();
            dtpFechaNacimiento.SelectedDate = DateTime.Today.AddYears(-18);
            
            dtpFechaReporteInicial.SelectedDate = DateTime.Today.AddDays(-1);
            
            txtNumeroTarjeta.Text = Generadores.CardGenerator.Next().ToString();
            mUsuariosPopulator = new UsuariosPopulator(mDBManagers);
            generarLista();
            dgtcNombre.Binding = new Binding("sNombre");
            dgtcNumeroTarjeta.Binding = new Binding("TarjetaAsignada");
            dgtcSaldo.Binding = new Binding("Saldo");
            dgtcSexo.Binding = new Binding("Sexo");
            Binding bTipoUsuario = new Binding("sTipoUsuario");
            dgtcTipoUsuario.Binding = bTipoUsuario;
            /*Código Placeholder*/
           /* dtgrdBusqueda.Items.Add(new Usuario() {Nombre="Prueba",TarjetaAsignada=12345678,Saldo=519,Sexo="Hombre" });
            dtgrdBusqueda.Items.Add(new Usuario() { Nombre = "Prueba1", TarjetaAsignada = 12345678, Saldo = 519, Sexo = "Hombre" });
            dtgrdBusqueda.Items.Add(new Usuario() { Nombre = "Prueba2", TarjetaAsignada = 12345678, Saldo = 519, Sexo = "Hombre" });
            dtgrdBusqueda.Items.Add(new Usuario() { Nombre = "Prueba3", TarjetaAsignada = 12345678, Saldo = 519, Sexo = "Hombre" });
            dtgrdBusqueda.Items.Add(new Usuario() { Nombre = "Prueba4", TarjetaAsignada = 12345678, Saldo = 519, Sexo = "Hombre" });*/
            /*##END##*/
            
           
        }

        void generarLista()
        {

            mUsuariosPopulator.generarLista();
            Usuarios = mUsuariosPopulator.Usuarios;
        }

        public void ClearTextBoxes(Panel panel)
        {

            foreach (Control c in panel.Children.OfType<Control>())
            {

                if (c is TextBox)
                {

                    ((TextBox)c).Clear();

                }

            }
            foreach (Panel p in panel.Children.OfType<Panel>())
            {

                ClearTextBoxes(p);
            }

        }

        private void cambiaTextoBusquedaAsync(object sender, TextChangedEventArgs e)
        {
            dcpnlBusqueda.Visibility = Visibility.Hidden;
            TextBox origen = e.Source as TextBox;
            if (origen.Text != "" ) {
                
                UsuariosBusqueda = (from usuarios in Usuarios where usuarios.Nombre.ToLower().Contains(origen.Text.ToLower()) select usuarios ).ToList();
                dtgrdBusqueda.ItemsSource = UsuariosBusqueda;
                if(UsuariosBusqueda.Count>0)dcpnlBusqueda.Visibility = Visibility.Visible;
                
                //txtNumeroTarjetaRecarga.IsReadOnly = true;
                
            }
            else
            {
                
                //txtNumeroTarjetaRecarga.IsReadOnly = false;
            }
        }

        
        private void cambiaTextoBusqueda(object sender, TextChangedEventArgs e)
        {
            Action<object, TextChangedEventArgs> del = (object s, TextChangedEventArgs t) => cambiaTextoBusquedaAsync(s, t);
            ThreadStart start = delegate() { 
                Dispatcher.Invoke(
                    DispatcherPriority.ApplicationIdle, 
                   del,sender,e
                    ); 
            };

            new Thread(start).Start();
            
        }

        private void clickCheckboxRangoFecha(object sender, RoutedEventArgs e)
        {
            if (((sender as CheckBox).IsChecked).Value)
            {
                dtpFechaReporteFinal.IsEnabled = true;
            }
            else
            {
                dtpFechaReporteFinal.IsEnabled = false;
            }
        }

        private void entraTexto(object sender, TextCompositionEventArgs e)
        {

        }

        Usuario generarUsuario()
        {
            Usuario.Contacto tmpContacto = new Usuario.Contacto { 
                Nombre = txtNombreDeContacto.Text,
                Telefono = txtTelefonoDeContacto.Text
            };
            Usuario.Domicilio tmpDomicilio = new Usuario.Domicilio { 
                Calle = txtCalle.Text,
                Colonia = txtColonia.Text,
                Municipio = (short)cmbMunicipio.SelectedIndex,
                Numero = int.Parse(txtNumero.Text)
            };

            return new Usuario { 
                Sexo = rdbHombre.IsChecked.ToString(),
                Alergias = txtAlergias.Text,
                Celular = txtCelular.Text,
                FechaNacimiento = (DateTime)dtpFechaNacimiento.SelectedDate,
                Nombre = txtNombre.Text+Constantes.SeparadorNombre+txtApellidoPaterno.Text+Constantes.SeparadorNombre+txtApellidoMaterno.Text,
                TarjetaAsignada = txtNumeroTarjeta.Text,
                TipoSangre = (short)(cmbSangre.SelectedIndex),
                TipoUsuario = (byte)(cmbTipos.SelectedIndex+1),               
                Telefono = txtTelefono.Text,
                mContacto = tmpContacto,
                mDomicilio = tmpDomicilio
            };
        }
        Usuario generarUsuario(int i)
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
        void limpiarVentanaRegistro()
        {
            cmbSangre.SelectedItem = Tipos.Sangre.Last();
            cmbMunicipio.SelectedItem = Tipos.Municipios.Last();
            ClearTextBoxes((Panel)grdRegistro);
            
        }

        private void onClickRegistrar(object sender, RoutedEventArgs e)
        {

            Usuario usuarioNuevo = generarUsuario();
            mUsuarioDBManager.setItem(usuarioNuevo);
            try 
            {
                mUsuarioDBManager.AddToDB();
                lblEstadoPrincipal.Content = usuarioNuevo.sNombre + " registrado correctamente.";
                lblEstadoSecundaria.Content = mDBManagers.LastMessage+" filas han sido actualizadas.";
                limpiarVentanaRegistro();
                txtNumeroTarjeta.Text = Generadores.CardGenerator.Next().ToString();
                generarLista();
            }
            catch(Exception ex)
            {
                lblEstadoPrincipal.Content = ex.ToString();
            }

        }

        private void PresionarTecla_BusqNom(object sender, KeyEventArgs e)
        {

        }

        private void ventanaCambiaTamaño(object sender, SizeChangedEventArgs e)
        {
            lblEstadoPrincipal.Content = e.NewSize.ToString();
        }

        private void cambiaTarjetaRecargaAsync(object sender, TextChangedEventArgs e)
        {
            TextBox origen = e.Source as TextBox;
            if (origen.Text != "")
            {
                
                //txtNombreBusqueda.IsReadOnly = true;
                UsuariosBusqueda = (from usuarios in Usuarios where usuarios.TarjetaAsignada == origen.Text select usuarios).ToList();
                
                if (UsuariosBusqueda.Count>0)
                {
                    currentUsuario = UsuariosBusqueda.First();
                    txtNombreBusqueda.Text =currentUsuario.sNombre;
                }
                //if (UsuariosBusqueda.Count==1)
                //{
                //    dtgrdBusqueda.Visibility = Visibility.Hidden;
                //}
                
            }
            else
            {
                
                //txtNombreBusqueda.IsReadOnly = false;
            }
        }
        private void cambiaTarjetaRecarga(object sender, TextChangedEventArgs e)
        {
            Action<object, TextChangedEventArgs> del = (object s, TextChangedEventArgs t) => cambiaTarjetaRecargaAsync(s, t);
            ThreadStart start = delegate()
            {
                Dispatcher.Invoke(
                    DispatcherPriority.ApplicationIdle,
                   del, sender, e
                    );
            };

            new Thread(start).Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lblEstadoPrincipal.Content = "";
            lblEstadoSecundaria.Content = "";
            mDBManagers.LastMessage = "";
            mUsuarioDBManager.setItem(currentUsuario);
            currentUsuario.Saldo += Decimal.Parse(txtSaldoRecarga.Text);
            lblEstadoPrincipal.Content = currentUsuario.Saldo.ToString() + " saldoTotal.";
            mUsuarioDBManager.modificarDato();
            generarLista();
            txtNombreBusqueda.Text = "";
            txtNombreBusqueda.Text = currentUsuario.Nombre;
        }
    }
}


