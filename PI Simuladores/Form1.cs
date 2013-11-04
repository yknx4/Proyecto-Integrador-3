using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_Integrador_3;
using Proyecto_Integrador_3.TiposDato;
using UsuarioDBManager = Proyecto_Integrador_3.DBManagers.UsuarioDBManager;
using UsuariosPopulator = Proyecto_Integrador_3.DBManagers.UsuariosPopulator;
using UnidadDBManager = Proyecto_Integrador_3.DBManagers.UnidadDBManager;
using UnidadPopulator = Proyecto_Integrador_3.DBManagers.UnidadPopulator;
using System.Data.SqlServerCe;


namespace PI_Simuladores
{
    public partial class Form1 : Form
    {

        private static SqlCeConnection conn = new SqlCeConnection(@Proyecto_Integrador_3.Constantes.getConnectionString);

        private static DBManagers mDBManagers = new DBManagers(conn);

        private static UsuarioDBManager mUsuarioDBManager = new UsuarioDBManager(mDBManagers);

        private static UsuariosPopulator mUsuariosPopulator;

        private List<Usuario> Usuarios;

        private static UnidadDBManager mUnidadDBManager = new UnidadDBManager(mDBManagers);

        private static UnidadPopulator mUnidadPopulator;

        private List<Unidad> Unidades;

        private List<string> Tarjetas;
        private List<string> Nombres;

        private List<Usuario> UsuariosBusqueda;

        private Usuario currentUsuario;

        void generarLista()
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
            mUsuariosPopulator = new UsuariosPopulator(mDBManagers);
            mUnidadPopulator = new UnidadPopulator(mDBManagers);
            generarLista();
            Tarjetas= (from usuarios in Usuarios select usuarios.TarjetaAsignada).ToList();
            Nombres = (from usuarios in Usuarios select usuarios.sNombre).ToList();
            cmbTarjetas.SelectedIndex = -1;
            cmbTarjetas.DataSource = Tarjetas;
            txtTarjeta.AutoCompleteCustomSource = ToAutoCompleteStringCollection(Tarjetas);
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cuandoValorCambia(object sender, EventArgs e)
        {
            ComboBox origen = sender as ComboBox;
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
            for (int i = 0; i < Unidades;i++ )
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
    }
}
