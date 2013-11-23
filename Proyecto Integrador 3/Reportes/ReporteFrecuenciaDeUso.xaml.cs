using MahApps.Metro.Controls;
using Proyecto_Integrador_3.TiposDato;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using UnidadPopulator = Proyecto_Integrador_3.DBManagers.UnidadPopulator;
using UsuariosPopulator = Proyecto_Integrador_3.DBManagers.UsuariosPopulator;

namespace Proyecto_Integrador_3.Reportes
{
    /// <summary>
    /// Interaction logic for ReporteFrecuenciaDeUso.xaml
    /// </summary>
    public partial class ReporteFrecuenciaDeUso : MetroWindow
    {
        private DBManagers mDBManagers;

        private static UsuariosPopulator mUsuariosPopulator;

        private UnidadPopulator mUnidadPopulator;

        private Usuario currentUsuario;
        private Unidad currentUnidad;

        private BackgroundWorker mBackgroundGenerarLista = new BackgroundWorker();
        private BackgroundWorker mBackgroundBusquedaUsuario = new BackgroundWorker();
        private BackgroundWorker mBackgroundBusquedaUnidad = new BackgroundWorker();
        private BackgroundWorker mBackgroundBusquedaGeneral = new BackgroundWorker();
        private BackgroundWorker mBackgroundCargarDataset = new BackgroundWorker();
        private BackgroundWorker mBackgroundEstadisticasTipoUsuario = new BackgroundWorker();

        public ReporteFrecuenciaDeUso(ref DBManagers Dbmgr)
        {
            mDBManagers = Dbmgr;

            //mDBManagers.Fill();
            mUnidadPopulator = new UnidadPopulator(ref Dbmgr, true);
            mUsuariosPopulator = new UsuariosPopulator(ref Dbmgr, true);
            InitializeComponent();
            SetBackgroundWorkers();
            SetBindings();
            txtTarjetaUsuario.TextChanged += Helpers.validarTextBoxtColor;
        }

        private void SetBackgroundWorkers()
        {
            mBackgroundGenerarLista.DoWork += _backgroundWorker_DoWork_GenerarLista;
            mBackgroundGenerarLista.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted_GenerarLista; // Run the Background Worker
            mBackgroundBusquedaUsuario.DoWork += _backgroundWorker_DoWork_BuscarUsuario;
            mBackgroundBusquedaUsuario.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted_BuscarUsuario;
            mBackgroundCargarDataset.DoWork += _backgroundWorker_DoWork_CargarDataset;
            mBackgroundCargarDataset.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted_CargarDataset;
            mBackgroundBusquedaUnidad.DoWork += _backgroundWorker_DoWork_BuscarUnidad;
            mBackgroundBusquedaUnidad.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted_BuscarUnidad;
            mBackgroundEstadisticasTipoUsuario.DoWork += _backgroundWorker_DoWork_EstadisticasTipoUsuario;
            mBackgroundEstadisticasTipoUsuario.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted_EstadisticasTipoUsuario;
            mBackgroundBusquedaGeneral.DoWork += _backgroundWorker_DoWork_BuscarGeneral;
            mBackgroundBusquedaGeneral.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted_BuscarGeneral;
        }

        private void _backgroundWorker_DoWork_BuscarGeneral(object sender, DoWorkEventArgs e)
        { //MessageBox.Show("Ejecuto");
            string arg = e.Argument as String;
            arg=arg.ToLower();
            if (arg != "")
            {
                try
                {
                  e.Result = (from usuario in mUsuariosPopulator.Usuarios where usuario.Nombre.ToLower().Contains(arg) select usuario).ToList();
                }
                catch (System.Exception)
                {
                    e.Result = null;
                }
            }
            else
            {
                //txtNumeroTarjetaRecarga.IsReadOnly = false;
            }
        }

        private void _backgroundWorker_RunWorkerCompleted_BuscarGeneral(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                //lblEstadoPrincipal.Content = "Evento Cancelado";

                //statusText.Text = "Cancelled";
            }
            else if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString());
            }
            else
            {
                if (e.Result != null)
                {
                    List<Usuario> result =(List<Usuario>)e.Result;
                    if (result.Count > 0) {
                        grdBusqueda.Visibility = Visibility.Visible;
                        lstBusqueda.ItemsSource = result;
                    }
                }
                else
                {
                    txtbEstado.Text = "No se encontró nada.";
                }
            }
            pgrEstado.IsActive = false;
        }

        struct EstadisticaResult
        {
            public int General;
            public int Estudiante;
            public int TerceraEdad;
            public int Discapacidad;
            public Datos GData;
            public Datos EData;
            public Datos TData;
            public Datos CData;
           
            public struct Datos
            {
                //public string Unidades;
                public List<String> Unidades;
                public int Ucount;
                public List<String> Meses;
                //public string Meses;
                public int Mcount;
                public List<String> Dias;
                //public string Dias;
                public int Dcount;
            }
        }

        private EstadisticaResult.Datos parseServicios(List<Servicio> servicios)
        {
            EstadisticaResult.Datos resultado = new EstadisticaResult.Datos();
            List<DateTime> Fecha = (from servicio in servicios select servicio.Fecha.Date).ToList();
            List<int> Mes = (from servicio in servicios select servicio.Fecha.Month).ToList();
            List<Guid> UnidadesID = (from servicio in servicios select servicio.Unidad).ToList();
            HashSet<DateTime> Res;
            HashSet<int> ResM;
            HashSet<Guid> ResU;
            resultado.Dcount = Helpers.masRepetido<DateTime>(Fecha, out Res);
            resultado.Dias = new List<string>();
            foreach (DateTime valor in Res.Cast<DateTime>().ToList())
            {
                //resultado.Dias += valor.ToShortDateString() + " ";
                resultado.Dias.Add(valor.ToShortDateString());
            }
            resultado.Mcount = Helpers.masRepetido<int>(Mes, out ResM);
            resultado.Meses = new List<string>();
            foreach (int valor in ResM)
            {
                //resultado.Meses += Tipos.Meses[valor] + " ";
                resultado.Meses.Add(Tipos.Meses[valor]);
            }
            resultado.Ucount = Helpers.masRepetido<Guid>(UnidadesID, out ResU);
            Unidad res;
            resultado.Unidades = new List<string>();
            foreach (Guid valor in ResU)
            {
                res = mUnidadPopulator.Unidades.Find(Unidad => Unidad.Uid == valor);
                //resultado.Unidades+= res.NoUnidad + " ";
                resultado.Unidades.Add(res.NoUnidad);
            }
            //lstGUnidadM.ItemsSource = unidades;

            /*resultado.Mcount = (from unidades)*/



            return resultado;
        }

        private void _backgroundWorker_DoWork_EstadisticasTipoUsuario(object sender, DoWorkEventArgs e)
        {
            EstadisticaResult resultado = new EstadisticaResult();
            List<Servicio> General = new List<Servicio>();
            foreach (var serv in (from usuario in mUsuariosPopulator.Usuarios where usuario.TipoUsuario == 1 select usuario.Servicios))
            {
                General.AddRange(serv);
            }
            List<Servicio> Estudiante = new List<Servicio>();
            foreach (var serv in (from usuario in mUsuariosPopulator.Usuarios where usuario.TipoUsuario == 2 select usuario.Servicios))
            {
                Estudiante.AddRange(serv);
            }
            List<Servicio> TerceraEdad = new List<Servicio>();
            foreach (var serv in (from usuario in mUsuariosPopulator.Usuarios where usuario.TipoUsuario == 3 select usuario.Servicios))
            {
                TerceraEdad.AddRange(serv);
            }
            List<Servicio> Discapacidad = new List<Servicio>();
            foreach (var serv in (from usuario in mUsuariosPopulator.Usuarios where usuario.TipoUsuario == 4 select usuario.Servicios))
            {
                Discapacidad.AddRange(serv);
            }
            resultado.GData = parseServicios(General);
            resultado.EData = parseServicios(Estudiante);
            resultado.TData = parseServicios(TerceraEdad);
            resultado.CData = parseServicios(Discapacidad);
            resultado.General = General.Count;
            resultado.Estudiante = Estudiante.Count;
            resultado.TerceraEdad = TerceraEdad.Count;
            resultado.Discapacidad = Discapacidad.Count;
            e.Result = resultado;
        }

        private void _backgroundWorker_RunWorkerCompleted_EstadisticasTipoUsuario(object sender, RunWorkerCompletedEventArgs e)
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
                EstadisticaResult r = (EstadisticaResult)e.Result;
                txtGServicios.Text = r.General.ToString();
                txtEServicios.Text = r.Estudiante.ToString();
                txtTServicios.Text = r.TerceraEdad.ToString();
                txtCServicios.Text = r.Discapacidad.ToString();
                txtGTotal.Text = "$"+(r.General * Constantes.PrecioNormal).ToString();
                txtETotal.Text = "$" + (r.Estudiante * Constantes.PrecioEspecial).ToString();
                txtTTotal.Text = "$" + (r.TerceraEdad * Constantes.PrecioEspecial).ToString();
                txtCTotal.Text = "$" + (r.Discapacidad * Constantes.PrecioEspecial).ToString();
                //txtGUnidadM.Text = r.GData.Unidades;
                lstGUnidadM.ItemsSource = r.GData.Unidades;
                lstGDiasM.ItemsSource = r.GData.Dias;
                lstGMesesM.ItemsSource = r.GData.Meses;
                lstEUnidadM.ItemsSource = r.EData.Unidades;
                lstEDiasM.ItemsSource = r.EData.Dias;
                lstEMesesM.ItemsSource = r.EData.Meses;
                lstTUnidadM.ItemsSource = r.TData.Unidades;
                lstTDiasM.ItemsSource = r.TData.Dias;
                lstTMesesM.ItemsSource = r.TData.Meses;
                lstCUnidadM.ItemsSource = r.CData.Unidades;
                lstCDiasM.ItemsSource = r.CData.Dias;
                lstCMesesM.ItemsSource = r.CData.Meses;

                Helpers.EnableControls(grdTipoUsuarios);
                pgrTipoUsuario.IsActive = false;
            }
        }

        private void SetBindings()
        {
            dgtcNombre.Binding = new Binding("sNombre");
            dgtcNumeroTarjeta.Binding = new Binding("TarjetaAsignada");
            dgtcTipoUsuario.Binding = new Binding("TipoUsuario");
            dgtcServicios.Binding = new Binding("ServiciosCount");
            dgtcConsumo.Binding = new Binding("Consumo");
            dgtcConsumo.Binding.StringFormat = "${0}";

        }

        private void _backgroundWorker_DoWork_BuscarUnidad(object sender, DoWorkEventArgs e)
        {
            //MessageBox.Show("Ejecuto");
            if ((string)e.Argument != "")
            {
                try
                {
                    e.Result = (from unidades in mUnidadPopulator.Unidades where unidades.NoUnidad == (string)e.Argument select unidades).First();
                }
                catch (System.Exception)
                {
                    e.Result = null;
                }
            }
            else
            {
                //txtNumeroTarjetaRecarga.IsReadOnly = false;
            }
        }

        private void _backgroundWorker_RunWorkerCompleted_BuscarUnidad(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                //lblEstadoPrincipal.Content = "Evento Cancelado";

                //statusText.Text = "Cancelled";
            }
            else if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString());
            }
            else
            {
                if (e.Result != null)
                {
                    currentUnidad = (Unidad)e.Result;
                    txtbEstado.Text = "Unidad encontrada.";
                    loadUnidad(currentUnidad);
                }
                else
                {
                    txtbEstado.Text = "No se encontró la unidad";
                }
            }
            pgrEstado.IsActive = false;

            //MessageBox.Show("Termino");
            //EnableControls((Panel)tbRecarga.Content);
        }

        private void _backgroundWorker_DoWork_CargarDataset(object sender, DoWorkEventArgs e)
        {
            mDBManagers.Fill();
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
                txtbEstado.Text = "Cargando servicios, unidades y usuarios.";
                generarLista();

                //.Text = "Completed";
            }
        }

        private void generarLista()
        {
            //DisableControls((Panel)tbRecarga.Content);

            mBackgroundGenerarLista.RunWorkerAsync();
        }

        private void _backgroundWorker_DoWork_GenerarLista(object sender, DoWorkEventArgs e)
        {
            mUsuariosPopulator.generarLista();
            mUnidadPopulator.generarLista();

            //mUsuariosPopulator.generarLista();
            //mUnidadPopulator.generarLista();
            //Usuarios.Start();
            //Unidades.Start();
            //Unidades.Join();
            //Usuarios.Join();
        }

        private void _backgroundWorker_DoWork_BuscarUsuario(object sender, DoWorkEventArgs e)
        {
            //MessageBox.Show("Ejecuto");
            if ((string)e.Argument != "")
            {
                try
                {
                    e.Result = (from usuarios in mUsuariosPopulator.Usuarios where usuarios.TarjetaAsignada == (string)e.Argument select usuarios).First();
                }
                catch (System.Exception)
                {
                    e.Result = null;
                }
            }
            else
            {
                //txtNumeroTarjetaRecarga.IsReadOnly = false;
            }
        }

        private void _backgroundWorker_RunWorkerCompleted_GenerarLista(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                //lblEstadoPrincipal.Content = "Evento Cancelado";

                //statusText.Text = "Cancelled";
            }
            else if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString());
            }
            else
            {
                //Usuarios = mUsuariosPopulator.Usuarios;

                //.Text = "Completed";
                pgrEstado.IsActive = false;
                txtbEstado.Text = mUnidadPopulator.Unidades.Count.ToString() + " unidades cargadas." + Environment.NewLine + mUsuariosPopulator.Usuarios.Count.ToString() + " usuarios cargadas.";
                pgrTipoUsuario.IsActive = true;
                mBackgroundEstadisticasTipoUsuario.RunWorkerAsync();
            }

            //EnableControls((Panel)tbRecarga.Content);
            //Helpers.EnableControls(grdTipoUsuarios);
            Helpers.EnableControls(grdUnidades);
            Helpers.EnableControls(grdUsuarios);
        }

        private void _backgroundWorker_RunWorkerCompleted_BuscarUsuario
            (object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                //lblEstadoPrincipal.Content = "Evento Cancelado";

                //statusText.Text = "Cancelled";
            }
            else if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString());
            }
            else
            {
                if (e.Result != null)
                {
                    currentUsuario = (Usuario)e.Result;
                    txtbEstado.Text = "Usuario encontrado.";
                    loadUsuario(currentUsuario);
                }
                else
                {
                    txtbEstado.Text = "No se encontró al usuario";
                }
            }
            pgrEstado.IsActive = false;

            //MessageBox.Show("Termino");
            //EnableControls((Panel)tbRecarga.Content);
        }

        private void loadUsuario(Usuario user)
        {
            txtDiaM.Clear();
            txtMesM.Clear();
            txtUnidadM.Clear();
            txtNombre.Text = user.sNombre;
            lblEdad.Content = ((int)(((DateTime.Now - user.FechaNacimiento).Days) / 365)).ToString();
            lblTipoUser.Content = Tipos.Usuarios[user.TipoUsuario];
            lblSaldo.Content = "$" + user.Saldo;
            lblEdad.Content = Helpers.Age(user.FechaNacimiento).ToString();
            lblConsumo.Content = "$" + user.Consumo.ToString();
            EstadisticaResult.Datos Result = parseServicios(user.Servicios.ToList());
            txtDiaM.Text = listToString(Result.Dias);
            txtMesM.Text = listToString(Result.Meses);
            txtUnidadM.Text = listToString(Result.Unidades);
            txtCantidadDiasU.Text = Result.Dcount.ToString();
            txtCantidadMesU.Text = Result.Mcount.ToString();
            txtCantidadUnidadU.Text = Result.Ucount.ToString();
            txtTarjetaUsuario.Text = user.TarjetaAsignada;
            
        }

        private string listToString(List<String> input){
            string res="";
            foreach(string var in input){
                res+=var+" ";
            }
            return res;
        }


        private void cuandoCargaFormulario(object sender, RoutedEventArgs e)
        {
            Helpers.DisableControls(grdTipoUsuarios);
            Helpers.DisableControls(grdUnidades);
            Helpers.DisableControls(grdUsuarios);
            txtbEstado.Text = "Cargando base de datos. (Puede tomar varios minutos)";
            pgrEstado.IsActive = true;
            mBackgroundCargarDataset.RunWorkerAsync();
        }

        private void alModificarNumeroTarjeta(object sender, KeyEventArgs e)
        {
            TextBox origen = sender as TextBox;
            if (e.Key != Key.Enter)
            {
                return;
            }
            origen.Background = Brushes.White;
            if (!Helpers.validarCuenta(origen.Text))
            {
                origen.Focus();
                origen.Background = Constantes.ErrorBrush;
                return;
            }
            pgrEstado.IsEnabled = true;
            pgrEstado.IsActive = true;
            txtbEstado.Text = "Buscando " + txtTarjetaUsuario.Text;
            mBackgroundBusquedaUsuario.RunWorkerAsync(txtTarjetaUsuario.Text);
        }

        private void alCerrar(object sender, EventArgs e)
        {
            mDBManagers.ClearServicios();
        }

        private void alModificarNoUnidad(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
            {
                return;
            }
            pgrEstado.IsEnabled = true;
            pgrEstado.IsActive = true;
            txtNuUnidadR.Text = txtNuUnidadR.Text.ToUpper().Trim();
            txtbEstado.Text = "Buscando " + txtNuUnidadR.Text;
            mBackgroundBusquedaUnidad.RunWorkerAsync(txtNuUnidadR.Text);
        }

        private void loadUnidad(Unidad unidad)
        {
            txtDiasMUnidad.Clear();
            txtMesMUnidad.Clear();
            txtUsuariosDUnidad.Clear();
            txtUsuariosMUnidad.Clear();

            List<DateTime> Fecha = (from servicio in unidad.Servicios select servicio.Fecha.Date).ToList();
            List<int> Mes = (from servicio in unidad.Servicios select servicio.Fecha.Month).ToList();
            List<Guid> UsuariosID = (from servicio in unidad.Servicios select servicio.Usuario).ToList();
            HashSet<DateTime> Res;
            HashSet<int> ResM;
            HashSet<Guid> ResU;
            txtUsuariosDUnidad.Text = Helpers.masRepetido<DateTime>(Fecha, out Res).ToString();
            foreach (DateTime valor in Res.Cast<DateTime>().ToList())
            {
                txtDiasMUnidad.Text += valor.ToShortDateString() + " ";
            }
            txtUsuariosMUnidad.Text = Helpers.masRepetido<int>(Mes, out ResM).ToString();
            foreach (int valor in ResM)
            {
                txtMesMUnidad.Text += Tipos.Meses[valor] + " ";
                //txtMesMUnidad.Text += valor.ToString() + " ";
            }
            HashSet<Guid> usuariosMasFrecuentes = new HashSet<Guid>();

            for (int i = 0; i < 1; i++)
            {
                Helpers.masRepetido<Guid>(UsuariosID, out ResU);

                foreach (Guid valor in ResU)
                {
                    usuariosMasFrecuentes.Add(valor);
                    UsuariosID.RemoveAll(Guid => Guid==valor);
                }
            }
            BindingList<Usuario> usuariosDgrid = new BindingList<Usuario>();
            foreach (Guid user in usuariosMasFrecuentes)
            {
                usuariosDgrid.Add(mUsuariosPopulator.Usuarios.Find(Usuario => Usuario.Uid == user));
            }
            dtgrdReporteUnidad.ItemsSource = usuariosDgrid;
        }

        private void dobleClickDtgrd(object sender, MouseButtonEventArgs e)
        {
            if (dtgrdReporteUnidad.SelectedItems.Count == 1)
            {
                currentUsuario = (Usuario)dtgrdReporteUnidad.SelectedItems[0];
                loadUsuario(currentUsuario);
                tbsControles.SelectedIndex = 0;
            }
        }

        private void busquedaEnter(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
            {
                return;
            }
            pgrEstado.IsEnabled = true;
            pgrEstado.IsActive = true;
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
            txtbEstado.Text = "Buscando " + ((TextBox)sender).Text;
            mBackgroundBusquedaGeneral.RunWorkerAsync(((TextBox)sender).Text);
        }

        private void seleccionarBusqueda(object sender, RoutedEventArgs e)
        {
            if(lstBusqueda.SelectedItem!=null){
                loadUsuario((Usuario)lstBusqueda.SelectedItem);
            }
            grdBusqueda.Visibility = Visibility.Hidden;
        }
        private void cancelarBusqueda(object sender, RoutedEventArgs e)
        {
            grdBusqueda.Visibility = Visibility.Hidden;
        }
        
    }
}