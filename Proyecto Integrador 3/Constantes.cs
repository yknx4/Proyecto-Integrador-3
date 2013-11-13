using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MahApps.Metro;

namespace Proyecto_Integrador_3
{
    public static class Constantes
    {
        public const char SeparadorNombre = '&';
        public static string getConnectionString {
            get{
                return @"Data Source=|DataDirectory|\ProyectoIntegrador.sdf"; 
            }
        
        }
        public static decimal PrecioNormal = 6;
        public static decimal PrecioEspecial = 3;
        public static SolidColorBrush ErrorBrush = Brushes.Tomato;


        
            

        
    }
}
