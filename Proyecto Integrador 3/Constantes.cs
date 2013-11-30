using System.Windows.Media;

namespace Proyecto_Integrador_3
{
    public static class Constantes
    {
        public const char SeparadorNombre = '&';

        public static string getConnectionString
        {
            get
            {
                return @"Data Source=|DataDirectory|\ProyectoIntegrador.sdf";
            }
        }

        public static decimal PrecioNormal = 6;
        public static decimal PrecioEspecial = 3;
        public static SolidColorBrush ErrorBrush = Brushes.Tomato;
    }
}