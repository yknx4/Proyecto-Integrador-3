using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Integrador_3.Generadores
{
    public static class RandChar
    {
        static Random mRandom = new Random();
        static public char Next()
        {
            
            int compRand = mRandom.Next(64, 91);
            byte charByte = (byte)compRand;
            return (char)charByte;
        }
    }
}
