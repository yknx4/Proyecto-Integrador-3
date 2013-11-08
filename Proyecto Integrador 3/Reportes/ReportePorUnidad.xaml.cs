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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Proyecto_Integrador_3.TiposDato;
using UnidadPopulator = Proyecto_Integrador_3.DBManagers.UnidadPopulator;

namespace Proyecto_Integrador_3.Reportes
{
    /// <summary>
    /// Interaction logic for ReportePorUnidad.xaml
    /// </summary>
    public partial class ReportePorUnidad : MetroWindow
    {
        private UnidadPopulator mUnidadPopulator;
        private List<ReporteDeUnidad> reportesIndividuales = new List<ReporteDeUnidad>();
        public DateTime? inicial;
        public DateTime? final;
        private DBManagers dbMgr;

        public ReportePorUnidad(ref DBManagers dbMgr)
        {
            InitializeComponent();
            this.dbMgr = dbMgr;
            string defaultStringFormat = "${0}";
            dtgrReportes.AutoGenerateColumns = false;
            dgtcUnidad.Binding = new Binding("Unidad");
            dgtcTotal.Binding = new Binding("Total");
            dgtcTotal.Binding.StringFormat = defaultStringFormat;
            dgtcGeneral.Binding = new Binding("UsuarioGeneral");
            dgtcGeneral.Binding.StringFormat = defaultStringFormat;
            dgtcEstudiante.Binding = new Binding("UsuarioEstudiante");
            dgtcEstudiante.Binding.StringFormat = defaultStringFormat;
            dgtcTerceraEdad.Binding = new Binding("UsuarioTerceraEdad");
            dgtcTerceraEdad.Binding.StringFormat = defaultStringFormat;
            dgtcDiscapacitado.Binding = new Binding("UsuarioDiscapacitado");
            dgtcDiscapacitado.Binding.StringFormat = defaultStringFormat;
            
        }

        private class ReporteDeUnidad
        {
            Unidad _unidad;
            int general=0;
            int estudiante=0;
            int terceraedad=0;
            int discapacitado=0;
            List<Servicio> servicios;

            private void Contar()
            {
                
                foreach (Servicio servicio in servicios)
                {
                    
                    switch (servicio.TipoUsuario)
                    {
                        case 1:
                            general++;
                            break;
                        case 2:
                            estudiante++;
                            break;
                        case 3:
                            terceraedad++;
                            break;
                        case 4:
                            discapacitado++;
                            break;
                        default:
                            break;
                    }
                }

            }

            public ReporteDeUnidad(Unidad unidad, DateTime inicial, DateTime final)
            {
                _unidad = unidad;
                //MessageBox.Show(final.ToShortDateString() +" + "+inicial.ToShortDateString());
                servicios = (from servicio in _unidad.Servicios where servicio.Fecha >= inicial && servicio.Fecha <= final select servicio).ToList();
                //servicios = _unidad.Servicios.ToList();
                //MessageBox.Show(servicios.Count.ToString());
                Contar();

            }
            public ReporteDeUnidad(Unidad unidad, DateTime inicial) : this(unidad, inicial, inicial)
            {
                
            }

            public ReporteDeUnidad(Unidad unidad):this(unidad,DateTime.Now)
            {
                
            }

           public String Unidad
            {
                get
                {
                    return _unidad.NoUnidad.ToString();
                }
            }
           public Decimal Total
            {
                get
                {
                    return UsuarioGeneral + UsuarioEstudiante + UsuarioDiscapacitado + UsuarioTerceraEdad;
                }
            }
           public Decimal UsuarioGeneral
            {
                get
                {
                    return (general * Constantes.PrecioNormal);
                }
            }
           public Decimal UsuarioEstudiante
            {
                get
                {
                    return (estudiante * Constantes.PrecioEspecial);
                }
            }
           public Decimal UsuarioTerceraEdad
            {
                get
                {
                    return (terceraedad * Constantes.PrecioEspecial);
                }
            }
           public Decimal UsuarioDiscapacitado
            {
                get
                {
                    return (discapacitado * Constantes.PrecioEspecial);
                }
            }
        }


        private void Generar() {
            dtgrReportes.ItemsSource = new List<ReportePorUnidad>();
            if (!inicial.HasValue && !final.HasValue)
            {

            }
            else if (!final.HasValue)
            {
                this.Title += " (" + inicial.Value.ToShortDateString() + ")";
            }
            else
                this.Title += " (" + inicial.Value.ToShortDateString() + " - " + final.Value.ToShortDateString() + ")";

            dbMgr.FillUnidades();
            mUnidadPopulator = new UnidadPopulator(dbMgr);
            mUnidadPopulator.generarLista();
            foreach (Unidad unidad in mUnidadPopulator.Unidades)
            {

                if (!inicial.HasValue && !final.HasValue)
                {
                    reportesIndividuales.Add(new ReporteDeUnidad(unidad));
                }
                else if (!final.HasValue)
                {
                    reportesIndividuales.Add(new ReporteDeUnidad(unidad, inicial.Value));
                }
                else
                    reportesIndividuales.Add(new ReporteDeUnidad(unidad, inicial.Value, final.Value));

            }
            dtgrReportes.ItemsSource = reportesIndividuales;
        }

        private void alCargar(object sender, RoutedEventArgs e)
        {

            

            
            
        }

        private void entradaBusqueda(object sender, TextCompositionEventArgs e)
        {
            
        }

        private void enterEnBusqueda(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                dtgrReportes.ItemsSource = (from reporte in reportesIndividuales where reporte.Unidad.Contains(txtBusqueda.Text) select reporte).ToList();
            }
            if (((TextBox)sender).Text=="")
            {
                dtgrReportes.ItemsSource = reportesIndividuales;
            }
        }

        private void alCargarFilas(object sender, DataGridRowDetailsEventArgs e)
        {
        }

        private void cuantoVentanaCierra(object sender, EventArgs e)
        {
            dbMgr.ClearUnidades();
            //mUnidadPopulator.clear();
            
        }

        private void btnMostrarReporte_Click(object sender, RoutedEventArgs e)
        {
            this.inicial = dtpFechaReporteInicial.SelectedDate;
            this.final = dtpFechaReporteFinal.SelectedDate;
            Generar();
        }
    }
}
