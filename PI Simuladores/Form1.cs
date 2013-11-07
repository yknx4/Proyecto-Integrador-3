using Proyecto_Integrador_3;
using Proyecto_Integrador_3.TiposDato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ServicioDBManager = Proyecto_Integrador_3.DBManagers.ServicioDBManager;
using UnidadDBManager = Proyecto_Integrador_3.DBManagers.UnidadDBManager;
using UnidadPopulator = Proyecto_Integrador_3.DBManagers.UnidadPopulator;
using UsuarioDBManager = Proyecto_Integrador_3.DBManagers.UsuarioDBManager;
using UsuariosPopulator = Proyecto_Integrador_3.DBManagers.UsuariosPopulator;
using System.IO;

namespace PI_Simuladores
{
    public partial class Form1 : Form
    {
        //private static SqlCeConnection conn = new SqlCeConnection(@Proyecto_Integrador_3.Constantes.getConnectionString);

        private static DBManagers mDBManagers = new DBManagers();

        private static UsuarioDBManager mUsuarioDBManager = new UsuarioDBManager(mDBManagers);

        private static UsuariosPopulator mUsuariosPopulator;

        private static ServicioDBManager mServicioDBManager = new ServicioDBManager(mDBManagers);

        private List<Usuario> Usuarios;

        private static UnidadDBManager mUnidadDBManager = new UnidadDBManager(mDBManagers);

        private static UnidadPopulator mUnidadPopulator;

        private List<Unidad> Unidades;

        private List<string> Tarjetas;
        private List<string> Nombres;

        private List<Usuario> UsuariosBusqueda;

        private Usuario currentUsuario;

        private static Random mRandom = new Random();

        private void generarLista()
        {
            mUsuariosPopulator.generarLista();
            Usuarios = mUsuariosPopulator.Usuarios;
            mUnidadPopulator.generarLista();
            Unidades = mUnidadPopulator.Unidades;
            if (Unidades.Count <= 0)
            {
                grpServicio.Enabled = false;
            }
            else
            {
                grpServicio.Enabled = true;
            }
        }

        public static AutoCompleteStringCollection ToAutoCompleteStringCollection(IEnumerable<string> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            var autoComplete = new AutoCompleteStringCollection();
            foreach (var item in enumerable) autoComplete.Add(item);
            return autoComplete;
        }

        public Form1()
        {
            InitializeComponent();
            mDBManagers.Fill();
            mUsuariosPopulator = new UsuariosPopulator(mDBManagers,false);
            mUnidadPopulator = new UnidadPopulator(mDBManagers);
            generarLista();
            Tarjetas = (from usuarios in Usuarios select usuarios.TarjetaAsignada).ToList();
            Nombres = (from usuarios in Usuarios select usuarios.sNombre).ToList();
            cmbTarjetas.SelectedIndex = -1;
            cmbTarjetas.DataSource = Tarjetas;
            txtTarjeta.AutoCompleteCustomSource = ToAutoCompleteStringCollection(Tarjetas);
            lblStatus.Text = Usuarios.Count.ToString() + " usuarios cargados.";
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void cuandoValorCambia(object sender, EventArgs e)
        {
            ComboBox origen = sender as ComboBox;
            if (origen.SelectedItem == null) return;
            txtTarjeta.Text = origen.SelectedValue.ToString();
        }

        private void cuandoTarjetaCambia(object sender, EventArgs e)
        {
            TextBox origen = sender as TextBox;
            if (origen.Text != "")
            {
                //txtNombreBusqueda.IsReadOnly = true;
                UsuariosBusqueda = (from usuarios in Usuarios where usuarios.TarjetaAsignada == origen.Text select usuarios).ToList();

                if (UsuariosBusqueda.Count > 0)
                {
                    currentUsuario = UsuariosBusqueda.First();
                    txtNombre.Text = currentUsuario.sNombre;
                    txtSaldo.Text = currentUsuario.Saldo.ToString();
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

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void btnGenerarUnidades_Click(object sender, EventArgs e)
        {
            int Unidades = int.Parse(txtCantidadUnidades.Text);
            Unidad unidadNueva;
            for (int i = 0; i < Unidades; i++)
            {
                unidadNueva = new Unidad();
                mUnidadDBManager.setItem(unidadNueva);
                try
                {
                    mUnidadDBManager.AddToDB();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            generarLista();
        }

        private Unidad NextUnidad()
        {
            return Unidades[mRandom.Next(Unidades.Count)];
        }

        private DateTime NextFecha()
        {
            DateTime startDate = new DateTime(2013, 1, 1);
            DateTime endDate = DateTime.Now;
            TimeSpan timeSpan = endDate - startDate;
            TimeSpan newSpan = new TimeSpan(mRandom.Next(0, (int)timeSpan.TotalDays), 0, 0, 0);
            DateTime newDate = startDate + newSpan;
            return newDate;
        }

        private Usuario NextUsuario()
        {
            return Usuarios[mRandom.Next(Usuarios.Count)];
        }

        private void AnadirServicio(Usuario user, DateTime deeto)
        {
            Servicio nuevoServicio;
            Unidad tmpUnidad = NextUnidad();
            nuevoServicio = new Servicio
            {
                TipoUsuario = user.TipoUsuario,
                Unidad = tmpUnidad.Uid,
                Usuario = user.Uid,
                Fecha = deeto,
                UnidadObject = tmpUnidad,
                UsuarioObject = user
            };
            mServicioDBManager.setItem(nuevoServicio);
            try
            {
                mServicioDBManager.AddToDB();
                lblStatus.Text = nuevoServicio.UsuarioObject.sNombre + " ha abordado la unidad " + tmpUnidad.NoUnidad.ToString() + " el día " + nuevoServicio.Fecha.ToShortDateString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AnadirServicio()
        {
            AnadirServicio(currentUsuario, dtpFecha.Value);
        }

        private void btnServico_Click(object sender, EventArgs e)
        {
            AnadirServicio();
        }

        private void cuandoDobleClick(object sender, MouseEventArgs e)
        {
            mDBManagers.Refresh();
            generarLista();
            Tarjetas = (from usuarios in Usuarios select usuarios.TarjetaAsignada).ToList();
            Nombres = (from usuarios in Usuarios select usuarios.sNombre).ToList();
            cmbTarjetas.SelectedIndex = -1;
            cmbTarjetas.DataSource = Tarjetas;
            txtTarjeta.AutoCompleteCustomSource = ToAutoCompleteStringCollection(Tarjetas);
            lblStatus.Text = Usuarios.Count.ToString() + " usuarios recargados.";
        }

        private void btnRandom_Click(object sender, EventArgs e)
                {
                    
                    
            Usuario tmpUsuario;
            DateTime tmpFecha;
            string tmpLog = "";
            for (int i = 0; i < 40; i++)
            {
                tmpUsuario = NextUsuario();
                tmpFecha = NextFecha();
                AnadirServicio(tmpUsuario, tmpFecha);
                tmpLog += tmpUsuario.sNombre + " ha abordado el día " + tmpFecha.ToShortDateString() + Environment.NewLine;
                //File.AppendAllText(@"D:\ServiciosFalsos.txt", tmpUsuario.sNombre + " ha abordado el día " + tmpFecha.ToShortDateString() + Environment.NewLine);
            }
            MessageBox.Show(tmpLog);
            File.AppendAllText(@"ServiciosRecord.txt", tmpLog);
        }
    }
}