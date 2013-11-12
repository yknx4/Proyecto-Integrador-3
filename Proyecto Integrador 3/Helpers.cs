using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Proyecto_Integrador_3
{
    static class Helpers
    {
        public static void ClearTextBoxes(Panel panel)
        {
            foreach (Control c in panel.Children.OfType<Control>())
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
            }
            foreach (Panel p in panel.Children.OfType<Panel>())
            {
                ClearTextBoxes(p);
            }
        }

        public static void DisableControls(Panel panel)
        {
            foreach (Control c in panel.Children.OfType<Control>())
            {
                c.IsEnabled = false;
            }
            foreach (Panel p in panel.Children.OfType<Panel>())
            {
                DisableControls(p);
            }
        }

        public static void EnableControls(Panel panel)
        {
            foreach (Control c in panel.Children.OfType<Control>())
            {
                c.IsEnabled = true;
            }
            foreach (Panel p in panel.Children.OfType<Panel>())
            {
                EnableControls(p);
            }
        }

        public static int Age(DateTime bday)
        {
            int age = DateTime.Now.Year - bday.Year;
            if (bday > DateTime.Now.AddYears(-age)) age--;
            return age;

        }

        public static void masRepetido<T>(List<T> datos, out List<T> moda) where T : IComparable<T>
        {
            moda= new List<T>();
            T valorActual = datos.First();
            int countValorActual = 0;
            int countValorMasFrecuente = 0;
            for (var j = 0; j <= datos.Count; j++)
            {
                T valor = default(T);
                bool last = j == datos.Count;
                if (!last)
                {
                    valor = datos[j];
                }

                if (valor.CompareTo(valorActual)==0 && !last)
                {
                    countValorActual++;
                }
                else
                {
                    if (countValorActual == countValorMasFrecuente)
                    {
                        moda.Add(valorActual);
                    }
                    else if (countValorActual > countValorMasFrecuente)
                    {
                        countValorMasFrecuente = countValorActual;
                        moda.Clear();
                        moda.Add(valorActual);
                    }
                    if (!last) valorActual = valor;
                    countValorActual = 1;
                }
            }
        }
    }
}
