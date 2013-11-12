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
using UsuariosPopulator = Proyecto_Integrador_3.DBManagers.UsuariosPopulator;
using UnidadPopulator = Proyecto_Integrador_3.DBManagers.UnidadPopulator;
using Proyecto_Integrador_3.TiposDato;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using System.Globalization;

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

        private BackgroundWorker mBackgroundGenerarLista = new BackgroundWorker();
        private BackgroundWorker mBackgroundBusquedaUsuario = new BackgroundWorker();
        private BackgroundWorker mBackgroundCargarDataset = new BackgroundWorker();

        public ReporteFrecuenciaDeUso(ref DBManagers Dbmgr)
        {
            
            mDBManagers = Dbmgr;
            //mDBManagers.Fill();
            mUnidadPopulator = new UnidadPopulator(ref Dbmgr, true);
            mUsuariosPopulator = new UsuariosPopulator(ref Dbmgr, true);
            InitializeComponent();
            SetBackgroundWorkers();
        }

        private void SetBackgroundWorkers()
        {
            mBackgroundGenerarLista.DoWork += _backgroundWorker_DoWork_GenerarLista;
            mBackgroundGenerarLista.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted_GenerarLista; // Run the Background Worker
            mBackgroundBusquedaUsuario.DoWork += _backgroundWorker_DoWork_BuscarUsuario;
            mBackgroundBusquedaUsuario.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted_BuscarUsuario;
            mBackgroundCargarDataset.DoWork += _backgroundWorker_DoWork_CargarDataset;
            mBackgroundCargarDataset.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted_CargarDataset;
           
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
            Helpers.DisableControls(grdTipoUsuarios);
            Helpers.DisableControls(grdUnidades);
            Helpers.DisableControls(grdUsuarios);
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
            }
            //EnableControls((Panel)tbRecarga.Content);
            Helpers.EnableControls(grdTipoUsuarios);
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
            #region PruebModa
            //List<DateTime> Fecha = new List<DateTime>();
            //Fecha.Clear();
            ////List<Unidad> Unidad = new List<Unidad>();
            //DateTime valorActual = user.Servicios.First().Fecha.Date;
            ////MessageBox.Show(valorActual.ToString());
            //int countValorActual = 0;
            //int countValorMasFrecuente = 0;
            //int countEspecial=0, countGeneral=0;
            //for (int j = 0; j <= user.Servicios.Count; j++)
            //{
            //    var valor = new DateTime();
            //    bool last = j == user.Servicios.Count;
            //    if (!last)
            //    {
            //        valor = user.Servicios.ToList()[j].Fecha.Date;
            //        if (user.Servicios.ToArray()[j].TipoUsuario != 1)
            //        {
            //            countEspecial++;
            //        }
            //        else
            //        {
            //            countGeneral++;
            //        }

            //    }


            //    if (valor == valorActual && !last)
            //    {
            //        countValorActual++;
            //    }
            //    else
            //    {
            //        if (countValorActual == countValorMasFrecuente)
            //        {
            //            Fecha.Add(valorActual);
            //        }
            //        else if (countValorActual > countValorMasFrecuente)
            //        {
            //            countValorMasFrecuente = countValorActual;
            //            Fecha.Clear();
            //            Fecha.Add(valorActual);
            //        }
            //        if (!last) valorActual = valor;
            //        countValorActual = 1;
            //    }
            //}
            #endregion
            int countEspecial = 0, countGeneral = 0;
            foreach (Servicio serv in user.Servicios)
            {
                if (serv.TipoUsuario != 1)
                {
                    countEspecial++;
                }
                else
                {
                    countGeneral++;
                }
            }
            decimal consumo = (countEspecial * Constantes.PrecioEspecial) + (countGeneral * Constantes.PrecioNormal);
            lblConsumo.Content = "$"+consumo.ToString();

            List<DateTime> Fecha = (from servicio in user.Servicios select servicio.Fecha.Date).ToList();
            List<int> Mes = (from servicio in user.Servicios select servicio.Fecha.Month).ToList();
            List<Guid> UnidadesID = (from servicio in user.Servicios select servicio.Unidad).ToList();
            List<DateTime> Res;
            List<int> ResM;
            List<Guid> ResU;
            Helpers.masRepetido<DateTime>(Fecha,out Res);
            foreach (DateTime valor in Res.Cast<DateTime>().ToList())
            {
                txtDiaM.Text += valor.ToShortDateString() + " ";
            }
            Helpers.masRepetido<int>(Mes, out ResM);
            foreach (int valor in ResM)
            {
                txtMesM.Text += new DateTime(2000, valor, 1).ToString("MMM", CultureInfo.InvariantCulture) +" ";
            }
            Helpers.masRepetido<Guid>(UnidadesID, out ResU);
            Unidad res;
            foreach (Guid valor in ResU)
            {
                res = mUnidadPopulator.Unidades.Find(
            delegate(Unidad bk)
            {
                return bk.Uid == valor;
            }
            );
                txtUnidadM.Text +=res.NoUnidad + " ";
            }
        }

        private void cuandoCargaFormulario(object sender, RoutedEventArgs e)
        {
            txtbEstado.Text = "Cargando base de datos. (Puede tomar varios minutos)";
            pgrEstado.IsActive = true;
            mBackgroundCargarDataset.RunWorkerAsync();
        }

        private void alModificarNumeroTarjeta(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
            {
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

        
    }
}
