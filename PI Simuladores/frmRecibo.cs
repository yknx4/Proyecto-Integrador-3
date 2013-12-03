using Proyecto_Integrador_3;
using Proyecto_Integrador_3.TiposDato;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PI_Simuladores
{
    public partial class frmRecibo : Form
    {
        Usuario currentUsuario;
        Random mRandom = new Random();
        
        public frmRecibo(Usuario current)
        {
            int mes = mRandom.Next(1,12);
            DateTime ini = new DateTime(DateTime.Now.Year, mes, 1);
            DateTime fin = new DateTime(DateTime.Now.Year, mes+1, 1);
            fin.AddDays(-1);
            unidadesServicios = new HashSet<Guid>();
            foreach(Guid uni in (from uni in (from serv in current.Servicios where serv.Fecha.Date>=ini.Date && serv.Fecha.Date<=fin.Date select serv) select uni.Unidad)){
                unidadesServicios.Add(uni);
            }
            InitializeComponent();
            this.currentUsuario = current;
            this.Text += currentUsuario.sNombre + " " + mRandom.Next(2400, 2768).ToString()+" "+Tipos.Meses[mes] ;
            
        }
        HashSet<Guid> unidadesServicios;
        private void frmRecibo_Load(object sender, EventArgs e)
        {
            foreach (Guid uni in unidadesServicios) {

                List<Servicio> serviciosUnidad = (from servi in currentUsuario.Servicios where servi.Unidad == uni select servi).ToList();
                dtgrReportes.Rows.Add(serviciosUnidad.Count.ToString(), serviciosUnidad.Find(Servicio => Servicio.Unidad == uni).NoUnidad, "Uso de la unidad " + serviciosUnidad.Find(Servicio => Servicio.Unidad == uni).NoUnidad, 6, currentUsuario.TipoUsuario == 1 ? "0" : ".5", currentUsuario.TipoUsuario == 1 ? serviciosUnidad.Count * Constantes.PrecioNormal : serviciosUnidad.Count * Constantes.PrecioEspecial);
            }
            for (int i = 0; i < 5; i++) {
                dtgrReportes.Rows.Add(1, "Saldo", "Recarga de saldo a la tarjeta "+currentUsuario.TarjetaAsignada, mRandom.Next(203, 316), 0, 0);
            }
            
        }
    }
}
