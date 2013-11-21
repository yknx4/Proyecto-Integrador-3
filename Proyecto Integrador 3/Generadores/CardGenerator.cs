using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Integrador_3.Generadores
{
    public static class CardGenerator
    {
        static public long Next() {
            Random mRandom = new Random();
            int compRand = mRandom.Next(100, 999);
            int anio= DateTime.Today.Year % 100;
            int dia = DateTime.Today.Day;
            int remove = dia > 9 ? 2 : 1;
            int actual = 99999 - compRand ;
            int mes = DateTime.Today.Month;
            string tarjeta = anio.ToString() + dia.ToString() +actual.ToString().Substring(remove) + mes.ToString() + compRand.ToString();
            tarjeta = tarjeta.Substring(0, 6) + new string(tarjeta.Substring(6, 6).Reverse().ToArray());
            long tarjetaFinal = Int64.Parse(tarjeta);
            return tarjetaFinal;
        }
    }
}
