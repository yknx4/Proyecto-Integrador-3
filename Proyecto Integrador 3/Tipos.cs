using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Integrador_3
{
    static class Tipos
    {
        public static readonly Dictionary<int, string> Usuarios;
        public static readonly Dictionary<int, int> Precio;
        public static readonly List<string> Sangre;
        public static readonly List<string> Municipios;
        static Tipos()
        {
            Usuarios = new Dictionary<int, string>();
            Usuarios[1] = "General";
            Usuarios[2] = "Estudiante";
            Usuarios[3] = "Tercera Edad";
            Usuarios[4] = "Capacidades Diferentes";
            Precio = new Dictionary<int, int>();
            Precio[1] = 6;
            Precio[2] = 3;
            Precio[3] = 3;
            Precio[4] = 3;
            String[] tipos = {"O-","O+","A−","A+","B−","B+","AB−","AB+"};
            String[] municipios = { "Armería", "Colima", "Comala", "Coquimatlán", "Cuahutemoc", "Ixtlahuacán", "Manzanillo", "Minatitlán", "Tecomán", "Villa de Alvarez" };
            Sangre = new List<string>(tipos);
            Sangre.Sort();
            Municipios = new List<string>(municipios);
            Municipios.Sort();
        }
    }
}
