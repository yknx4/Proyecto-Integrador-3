using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsuarioDBManager = Proyecto_Integrador_3.DBManagers.UsuarioDBManager;
using Proyecto_Integrador_3;
using Proyecto_Integrador_3.TiposDato;
using System.IO;

namespace Bulk_Usuarios_Parser
{
    public partial class Form1 : Form
    {
        //GUID	GivenName	StreetAddress	TelephoneNumber	BloodType	UPS	Surname	Gender	Birthday	City	State
        //0     1           2               3               4           5   6       7       8           9       10              
        //const string userString = "62ea8d13-7adf-49c8-9a76-f2c84424fe6e	Eiya	4499 Tea Berry Lane	715-733-0549	O+	1Z 251 917 29 0339 447 6	Sekine	male	5/24/1979	Barron	WI";
        List<Usuario> UsuariosFalsos = new List<Usuario>();
        private static DBManagers mDBManagers = new DBManagers();

        private static UsuarioDBManager mUsuarioDBManager = new UsuarioDBManager(mDBManagers);
        public Form1()
        {
            InitializeComponent();
            Random mRamdom = new Random();
            List<string> Nombres = new List<string>(File.ReadAllLines(@"D:\nfalsos.txt"));
            prgAdd.Minimum = 0;
            prgAdd.Maximum = Nombres.Count;
            prgAdd.Value = 0;

            List<string> TiposSange = new List<string>(Tipos.Sangre);
            foreach (string userString in Nombres)
            {
                string[] separados = userString.Split('\t');
                Usuario.Contacto tmpContacto = new Usuario.Contacto
                {
                    Nombre = "Contacto Temporal",
                    Telefono = "3121064917"
                };
                Usuario.Domicilio tmpDomicilio = new Usuario.Domicilio
                {
                    Calle = separados[2],
                    Colonia = separados[9],
                    Municipio = (short)mRamdom.Next(Tipos.Municipios.Count),
                    Numero = mRamdom.Next(9999)
                };
                //GUID	GivenName	StreetAddress	TelephoneNumber	BloodType	UPS	Surname	Gender	Birthday	City	State
                //0     1           2               3               4           5   6       7       8           9       10              

                /*Edad*/
                DateTime today = DateTime.Today;
                string fnss = separados[8];
                string[] fnac = fnss.Split('/');
                //5/24/1979
                DateTime bday = new DateTime(int.Parse(fnac[2]), int.Parse(fnac[0]), int.Parse(fnac[1]));
                int age = today.Year - bday.Year;
                if (bday > today.AddYears(-age)) age--;
                /*******/
                /*Usuarios[1] = "General";
                Usuarios[2] = "Estudiante";
                Usuarios[3] = "Tercera Edad";
                Usuarios[4] = "Capacidades Diferentes";*/

                byte tmpTUsuario = 1;
                if (age < 24)
                {
                    tmpTUsuario = 2;
                }
                else if (age > 65)
                {
                    tmpTUsuario = 3;
                }
                else
                {
                    if (mRamdom.Next(15) == 4)
                    {
                        tmpTUsuario = 4;
                    }
                }
                int tipoS = TiposSange.FindIndex(s => s == separados[4]);
                Usuario actual = new Usuario
                {
                    Uid = Guid.Parse(separados[0]),
                    Saldo = mRamdom.Next(1000),
                    Sexo = separados[7] == "male" ? "hombre" : "mujer",
                    Alergias = separados[10],
                    Celular = separados[3],
                    FechaNacimiento = bday,
                    Nombre = separados[1] +" "+ separados[6],
                    TarjetaAsignada = Proyecto_Integrador_3.Generadores.CardGenerator.Next().ToString(),
                    TipoSangre = tipoS==-1?1:tipoS,
                    TipoUsuario = tmpTUsuario,
                    Telefono = separados[3],
                    mContacto = tmpContacto,
                    mDomicilio = tmpDomicilio
                };
                UsuariosFalsos.Add(actual);
                
            }
            

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Usuario usuarioNuevo in UsuariosFalsos)
            {
                mUsuarioDBManager.setItem(usuarioNuevo);
                try
                {
                    mUsuarioDBManager.AddToDB(usuarioNuevo.Uid);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                prgAdd.Value++;
                lbltotal.Text = prgAdd.Value.ToString();
            }
            
        }
    }
}
