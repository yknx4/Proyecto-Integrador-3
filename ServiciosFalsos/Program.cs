using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicioDBManager = Proyecto_Integrador_3.DBManagers.ServicioDBManager;
using UnidadDBManager = Proyecto_Integrador_3.DBManagers.UnidadDBManager;
using UnidadPopulator = Proyecto_Integrador_3.DBManagers.UnidadPopulator;
using UsuarioDBManager = Proyecto_Integrador_3.DBManagers.UsuarioDBManager;
using UsuariosPopulator = Proyecto_Integrador_3.DBManagers.UsuariosPopulator;
using System.IO;
using Proyecto_Integrador_3.TiposDato;
using Proyecto_Integrador_3;

namespace ServiciosFalsos
{
    class Program
    {

        private static DBManagers mDBManagers = new DBManagers();

        private static UsuarioDBManager mUsuarioDBManager = new UsuarioDBManager(mDBManagers);

        private static UsuariosPopulator mUsuariosPopulator;

        private static ServicioDBManager mServicioDBManager = new ServicioDBManager(mDBManagers);

        private List<Usuario> Usuarios;

        private static UnidadDBManager mUnidadDBManager = new UnidadDBManager(mDBManagers);

        private static UnidadPopulator mUnidadPopulator;

        private List<Unidad> Unidades;

        private static Random mRandom = new Random();

        private void generarLista()
        {
            mUsuariosPopulator.generarLista();
            Usuarios = mUsuariosPopulator.Usuarios;
            mUnidadPopulator.generarLista();
            Unidades = mUnidadPopulator.Unidades;
            
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

        Servicio nuevoServicio;
        Unidad tmpUnidad;
        private void AnadirServicio(Usuario user, DateTime deeto)
        {
            tmpUnidad = NextUnidad();
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
                mServicioDBManager.AddToDataset();
                Console.WriteLine(nuevoServicio.UsuarioObject.sNombre + " ha abordado la unidad " + tmpUnidad.NoUnidad.ToString() + " el día " + nuevoServicio.Fecha.ToShortDateString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        

        static void Main(string[] args)
        {
            Program newP = new Program();
            newP.iniciar();
           
            
            //File.AppendAllText(@"ServiciosRecord.txt", tmpLog);
        }

        void iniciar()
        {
            mDBManagers.Fill();
            mUsuariosPopulator = new UsuariosPopulator(mDBManagers);
            mUnidadPopulator = new UnidadPopulator(mDBManagers);
            generarLista();
            Usuario tmpUsuario;
            DateTime tmpFecha;
            string tmpLog = "";
            for (var i = 0; i < 2000000; i++)
            {
                Console.Title = "Servicio: "+i.ToString();
                tmpUsuario = NextUsuario();
                tmpFecha = NextFecha();
                AnadirServicio(tmpUsuario, tmpFecha);
                tmpLog = tmpUsuario.sNombre + " ha abordado el día " + tmpFecha.ToShortDateString() + Environment.NewLine;
                File.AppendAllText(@"ServiciosFalsos_.txt", tmpLog);
                if (i % 50000 == 0 || mRandom.Next(100000) == 5) {
                    mServicioDBManager.UpdateDBFromDataset();
                }
            }
            mServicioDBManager.UpdateDBFromDataset();

        }
    }
}
