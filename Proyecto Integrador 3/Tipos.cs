using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Integrador_3
{
    static class Tipos
    {
        public static readonly Dictionary<int,string> Usuarios;
        static Tipos()
        {
            Usuarios = new Dictionary<int, string>();
            Usuarios[1] = "General";
            Usuarios[2] = "Estudiante";
            Usuarios[3] = "Tercera Edad";
            Usuarios[4] = "Capacidades Diferentes";


        }
    }
}
