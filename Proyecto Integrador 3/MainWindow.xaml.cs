using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ResourcePool = Proyecto_Integrador_3.Properties.Resources;
using UsuarioDBManager = Proyecto_Integrador_3.DBManagers.UsuarioDBManager;
using System.Drawing;
using MahApps.Metro.Controls;
using Proyecto_Integrador_3.TiposDato;
using System.Configuration;
using System.Data.SqlServerCe;

using Proyecto_Integrador_3.Populadores;
using System.Timers;
using System.Windows.Threading;
using System.Threading;



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

        private static UsuarioDBManager mUsuarioDBManager = new UsuarioDBManager();

        private static UsuariosPopulator mUsuariosPopulator;

        private List<Usuario> Usuarios;

        private List<Usuario> UsuariosBusqueda;

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
            mUsuarioDBManager.setConnection(conn);
            txtNumeroTarjeta.Text = Generadores.CardGenerator.Next().ToString();
            mUsuariosPopulator = new UsuariosPopulator(conn);
            mUsuariosPopulator.generarLista();
            Usuarios = mUsuariosPopulator.Usuarios;
            


            dgtcNombre.Binding = new Binding("Nombre");
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
            
            TextBox origen = e.Source as TextBox;
            if (origen.Text != "" ) {
                dcpnlBusqueda.Visibility = Visibility.Hidden;
                UsuariosBusqueda = (from usuarios in Usuarios where usuarios.Nombre.Contains(origen.Text) select usuarios ).ToList();
                dtgrdBusqueda.ItemsSource = UsuariosBusqueda;
                dcpnlBusqueda.Visibility = Visibility.Visible; 
                txtNumeroTarjetaRecarga.IsReadOnly = true;
                
            }
            else
            {
                dcpnlBusqueda.Visibility = Visibility.Hidden;
                txtNumeroTarjetaRecarga.IsReadOnly = false;
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
                TipoUsuario = (short)(cmbTipos.SelectedIndex+1),               
                Telefono = txtTelefono.Text,
                mContacto = tmpContacto,
                mDomicilio = tmpDomicilio
            };
        }
        Usuario generarUsuario(int i)
        {
            Usuario.Contacto tmpContacto = new Usuario.Contacto
            {
                Nombre = "Contacto",
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
                Nombre = "Dalia&Nummy",
                TarjetaAsignada = txtNumeroTarjeta.Text,
                TipoSangre = (short)(cmbSangre.SelectedIndex),
                TipoUsuario = (short)(cmbTipos.SelectedIndex + 1),
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

            Usuario usuarioNuevo = generarUsuario(1);
            mUsuarioDBManager.setItem(usuarioNuevo);
            if (mUsuarioDBManager.AddToDB())
            {
                lblEstadoPrincipal.Content = usuarioNuevo.Nombre.Replace('&', ' ') + " registrado correctamente.";
                limpiarVentanaRegistro();
                txtNumeroTarjeta.Text = Generadores.CardGenerator.Next().ToString();
            }
            else
            {
                lblEstadoPrincipal.Content = mUsuarioDBManager.Error;
            }

        }

        private void PresionarTecla_BusqNom(object sender, KeyEventArgs e)
        {

        }

        private void ventanaCambiaTamaño(object sender, SizeChangedEventArgs e)
        {
            lblEstadoPrincipal.Content = e.NewSize.ToString();
        }
    }
}


