using MahApps.Metro.Controls;
using Proyecto_Integrador_3.TiposDato;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using UnidadPopulator = Proyecto_Integrador_3.DBManagers.UnidadPopulator;
using Estadistica;


namespace Proyecto_Integrador_3.Reportes
{
    /// <summary>
    /// Interaction logic for ReportePorUnidad.xaml
    /// </summary>
    public partial class ReportePorUnidad : MetroWindow
    {
        private UnidadPopulator mUnidadPopulator;

        //private List<ReporteDeUnidad> reportesIndividuales = new List<ReporteDeUnidad>();
        private BindingList<ReporteDeUnidad> reportesIndividuales = new BindingList<ReporteDeUnidad>();

        public DateTime? inicial;
        public DateTime? final;
        private DBManagers dbMgr;
        private BackgroundWorker mBackgroundCargarDataset = new BackgroundWorker();
        private BackgroundWorker mBackgroundGenerarLista = new BackgroundWorker();

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
            SetBackgroundWorkers();
            dtpFechaReporteInicial.SelectedDate = DateTime.Today.AddDays(-1);
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

        private void SetBackgroundWorkers()
        {
            mBackgroundCargarDataset.DoWork += _backgroundWorker_DoWork_CargarDataset;
            mBackgroundCargarDataset.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted_CargarDataset;
            mBackgroundGenerarLista.DoWork += _backgroundWorker_DoWork_GenerarLista;
            mBackgroundGenerarLista.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted_GenerarLista; // Run the Background Worker
        }

        private void _backgroundWorker_DoWork_CargarDataset(object sender, DoWorkEventArgs e)
        {
            dbMgr.FillUnidades();
        }

        private void _backgroundWorker_RunWorkerCompleted_CargarDataset(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                //statusText.Text = "Cancelled";
            }
            else if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString());
            }
            else
            {
                pgrEstado.IsActive = true;
                txtbEstado.Text = "Cargando servicios y unidades.";
                mBackgroundGenerarLista.RunWorkerAsync();

                //.Text = "Completed";
            }
        }

        private void _backgroundWorker_DoWork_GenerarLista(object sender, DoWorkEventArgs e)
        {
            mUnidadPopulator = new UnidadPopulator(ref dbMgr, true);
            mUnidadPopulator.generarLista();
        }

        private void _backgroundWorker_RunWorkerCompleted_GenerarLista(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                //statusText.Text = "Cancelled";
            }
            else if (e.Error != null)
            {
                //statusText.Text = "Exception Thrown";
            }
            else
            {
                pgrEstado.IsActive = false;
                txtbEstado.Text = mUnidadPopulator.Unidades.Count.ToString() + " unidades cargadas.";
                btnMostrarReporte.IsEnabled = true;
                btnMostrarEstadisticas.IsEnabled = true;
                if (tmpFuncion != null)
                {
                    tmpFuncion();
                }

                //.Text = "Completed";
            }
        }

        private class ReporteDeUnidad
        {
            private Unidad _unidad;
            private int general = 0;
            private int estudiante = 0;
            private int terceraedad = 0;
            private int discapacitado = 0;
            private List<Servicio> servicios;

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
                servicios = (from servicio in _unidad.Servicios where servicio.Fecha.Date >= inicial.Date && servicio.Fecha.Date <= final select servicio).ToList();

                //servicios = _unidad.Servicios.ToList();
                //MessageBox.Show(servicios.Count.ToString());
                Contar();
            }

            public ReporteDeUnidad(Unidad unidad, DateTime inicial)
                : this(unidad, inicial, inicial)
            {
            }

            public ReporteDeUnidad(Unidad unidad)
                : this(unidad, DateTime.Now)
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

        public void Generar()
        {
            if (reportesIndividuales != null) reportesIndividuales.Clear();

            if (!inicial.HasValue && !final.HasValue)
            {
            }
            else if (!final.HasValue)
            {
                this.Title = "Reporte por Autobuses (" + inicial.Value.ToShortDateString() + ")";
            }
            else
                this.Title = "Reporte por Autobuses (" + inicial.Value.ToShortDateString() + " - " + final.Value.ToShortDateString() + ")";

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
                realizarBusqueda(txtBusqueda.Text);
            }
            if (((TextBox)sender).Text == "")
            {
                dtgrReportes.ItemsSource = reportesIndividuales;
            }
        }

        public void realizarBusqueda(string busqueda)
        {
            txtBusqueda.Text = txtBusqueda.Text.ToUpper().Trim();
            dtgrReportes.ItemsSource = (from reporte in reportesIndividuales where reporte.Unidad.Contains(busqueda) select reporte).ToList();
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
            dtgrReportes.Visibility = Visibility.Hidden;
            this.inicial = null;
            this.final = null;
            this.inicial = dtpFechaReporteInicial.SelectedDate;
            if (dtpFechaReporteFinal.IsEnabled) this.final = dtpFechaReporteFinal.SelectedDate;
            generarReporte();
        }

        public void generarReporte()
        {
            Generar();
            dtgrReportes.Visibility = Visibility.Visible;
        }

        private void reportesCargan(object sender, RoutedEventArgs e)
        {
            txtbEstado.Text = "Cargando Base de Datos";
            pgrEstado.IsActive = true;
            mBackgroundCargarDataset.RunWorkerAsync();
        }

        public delegate void funcionOpcional();

        public funcionOpcional tmpFuncion;

        private void btnMostrarEstadistica_Click(object sender, RoutedEventArgs e)
        {
            //int mes = inicial.Value.Month;
            List<Double> frecuencia = new List<Double>();
            foreach (Unidad uni in mUnidadPopulator.Unidades) {
                frecuencia.Add(uni.Servicios.Count);
            }
            frmEstadistica estad = new frmEstadistica(frecuencia);
            estad.Text = "Numero de abordajes por unidad durante 3 meses";
            estad.Show();
            estad.rehacer();
        }
    }
}