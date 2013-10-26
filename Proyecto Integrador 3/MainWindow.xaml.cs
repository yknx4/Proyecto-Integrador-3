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
        }
    }
}
