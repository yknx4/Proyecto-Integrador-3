using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        public static void SetValidationTextBoxes(Panel panel)
        {
            foreach (Control c in panel.Children.OfType<Control>())
            {
                if (c is TextBox)
                {
                    ((TextBox)c).TextChanged += Helpers.validarTextBoxtColor;
                }
            }
            foreach (Panel p in panel.Children.OfType<Panel>())
            {
                SetValidationTextBoxes(p);
            }
        }

        static public bool validarCuenta(string input)
        {
            long t;
            if (input.Length==12 && long.TryParse(input,out t)) return true;
            return false;
        }
        static public bool validarDinero(string input)
        {
            decimal t;
            if (decimal.TryParse(input,out t)) return true;
            return false;
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

        public static void validarTextBoxtColor(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Background = Brushes.White;
        }

        public static int Age(DateTime bday)
        {
            int age = DateTime.Now.Year - bday.Year;
            if (bday > DateTime.Now.AddYears(-age)) age--;
            return age;

        }

        public static int masRepetido<T>(List<T> datos, out HashSet<T> moda) where T : IComparable<T>
        {
            
            moda= new HashSet<T>();
            if (datos.Count == 0)
            {
                return 0;
            }
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
            return countValorMasFrecuente;
        }

        
    }



}
