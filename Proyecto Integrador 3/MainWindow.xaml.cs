﻿using System;
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
using Proyecto_Integrador_3.TiposDato;



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
            dtgrdBusqueda.Items.Add(new Usuario() {Nombre="Prueba",TarjetaAsignada=12345678,Saldo=519,Sexo="Hombre" });
            dtgrdBusqueda.Items.Add(new Usuario() { Nombre = "Prueba1", TarjetaAsignada = 12345678, Saldo = 519, Sexo = "Hombre" });
            dtgrdBusqueda.Items.Add(new Usuario() { Nombre = "Prueba2", TarjetaAsignada = 12345678, Saldo = 519, Sexo = "Hombre" });
            dtgrdBusqueda.Items.Add(new Usuario() { Nombre = "Prueba3", TarjetaAsignada = 12345678, Saldo = 519, Sexo = "Hombre" });
            dtgrdBusqueda.Items.Add(new Usuario() { Nombre = "Prueba4", TarjetaAsignada = 12345678, Saldo = 519, Sexo = "Hombre" });
            dtpFechaReporteInicial.SelectedDate = DateTime.Today.AddDays(-1);
            
           
        }

        Usuario generarUsuario() {
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
                TarjetaAsignada = long.Parse(txtNumeroTarjeta.Text),
                TipoSangre = (short)(cmbSangre.SelectedIndex),
                TipoUsuario = (short)(cmbTipos.SelectedIndex+1),               
                Telefono = txtTelefono.Text,
                mContacto = tmpContacto,
                mDomicilio = tmpDomicilio
            };
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

            txtNumeroTarjeta.Text = Generadores.CardGenerator.Next().ToString();
        }

        private void cambiaTextoBusqueda(object sender, TextChangedEventArgs e)
        {
            if ((e.Source as TextBox).Text != "") { dcpnlBusqueda.Visibility = Visibility.Visible; txtNumeroTarjetaRecarga.IsReadOnly = true; }
            else { dcpnlBusqueda.Visibility = Visibility.Hidden;
            txtNumeroTarjetaRecarga.IsReadOnly = false;
            }
        }
    }
}


