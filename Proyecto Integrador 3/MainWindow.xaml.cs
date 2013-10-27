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
using System.Drawing;
using MahApps.Metro.Controls;


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
        public MainWindow()
        {
            InitializeComponent();
            cmbTipos.ItemsSource = (from values in Tipos.Usuarios.Values select values).ToList();
            cmbSangre.ItemsSource = Tipos.Sangre;
            cmbSangre.SelectedItem = Tipos.Sangre.Last();
            cmbMunicipio.ItemsSource = Tipos.Municipios;
            cmbMunicipio.SelectedItem = Tipos.Municipios.Last();
            dtpFechaNacimiento.SelectedDate = DateTime.Today.AddYears(-18);
            dgtcNombre.Binding = new Binding("Nombre");
            dgtcNumeroTarjeta.Binding = new Binding("TarjetaAsignada");
            dgtcSaldo.Binding = new Binding("Saldo");
            dgtcSexo.Binding = new Binding("Sexo");
            dgtcTipoUsuario.Binding = new Binding("TipoUsuario");
            dtgrdBusqueda.Items.Add(new Usuario() {Nombre="Prueba",TarjetaAsignada=12345678,Saldo=519,Sexo=true });
            dtgrdBusqueda.Items.Add(new Usuario() { Nombre = "Prueba1", TarjetaAsignada = 12345678, Saldo = 519, Sexo = true });
            dtgrdBusqueda.Items.Add(new Usuario() { Nombre = "Prueba2", TarjetaAsignada = 12345678, Saldo = 519, Sexo = true });
            dtgrdBusqueda.Items.Add(new Usuario() { Nombre = "Prueba3", TarjetaAsignada = 12345678, Saldo = 519, Sexo = true });
            dtgrdBusqueda.Items.Add(new Usuario() { Nombre = "Prueba4", TarjetaAsignada = 12345678, Saldo = 519, Sexo = true });
            dtpFechaReporteInicial.SelectedDate = DateTime.Today.AddDays(-1);
            
           
        }

        private void ventanaCambiaTamaño(object sender, SizeChangedEventArgs e)
        {
            lblEstadoPrincipal.Content = e.NewSize.ToString();
        }

        private void entraTexto(object sender, TextCompositionEventArgs e)
        {
            
        }

        private void PresionarTecla_BusqNom(object sender, KeyEventArgs e)
        {
            if ((e.Source as TextBox).Text != "") { dcpnlBusqueda.Visibility = Visibility.Visible; }
            else dcpnlBusqueda.Visibility = Visibility.Hidden;
        }

        private void clickCheckboxRangoFecha(object sender, RoutedEventArgs e)
        {
            if (((sender as CheckBox).IsChecked).Value)
            {
                dtpFechaReporteFinal.IsEnabled = true;
            }
            else {
                dtpFechaReporteFinal.IsEnabled = false;
            }
        }

        private void onClickRegistrar(object sender, RoutedEventArgs e)
        {

        }
    }
}


