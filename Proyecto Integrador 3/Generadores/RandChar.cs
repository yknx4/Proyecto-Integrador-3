using System;

namespace Proyecto_Integrador_3.Generadores
{
    /// <summary>
    /// Clase generadora de caracteres aleatorios
    /// </summary>
    public static class RandChar
    {
        private static Random mRandom = new Random();

        /// <summary>
        /// Devuelve un caracter al azar.
        /// </summary>
        /// <returns>Caracter</returns>
        static public char Next()
        {
            int compRand = mRandom.Next(64, 91);
            byte charByte = (byte)compRand;
            return (char)charByte;
        }
    }
}