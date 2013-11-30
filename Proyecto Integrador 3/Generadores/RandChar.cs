using System;

namespace Proyecto_Integrador_3.Generadores
{
    public static class RandChar
    {
        private static Random mRandom = new Random();

        static public char Next()
        {
            int compRand = mRandom.Next(64, 91);
            byte charByte = (byte)compRand;
            return (char)charByte;
        }
    }
}