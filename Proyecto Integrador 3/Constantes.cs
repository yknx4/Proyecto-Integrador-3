using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        
            

        
    }
}
