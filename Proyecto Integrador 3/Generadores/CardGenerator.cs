using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Integrador_3.Generadores
{
    static class CardGenerator
    {
        static private int _posicion=0;
        static public int Posicion {
            get { return _posicion; }
            set { _posicion = value; }
        }
        static public long Next() {
            Random mRandom = new Random();
            int compRand = mRandom.Next(100, 999);
            int anio= DateTime.Today.Year % 100;
            int actual = 99999 - compRand - (Posicion+1);
            int mes = DateTime.Today.Month;
            string tarjeta = anio.ToString() + actual.ToString() + mes.ToString() + compRand.ToString();
            tarjeta = tarjeta.Substring(0, 6) + new string(tarjeta.Substring(6, 6).Reverse().ToArray());
            long tarjetaFinal = Int64.Parse(tarjeta);
            Posicion++;
            return tarjetaFinal;
        }
    }
}
