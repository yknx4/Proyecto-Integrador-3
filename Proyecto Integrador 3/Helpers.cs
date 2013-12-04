using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;

namespace Proyecto_Integrador_3
{
    /// <summary>
    /// Clases de ayuda general para el programa completo
    /// </summary>
    internal static class Helpers
    {
        /// <summary>
        /// Limpia todas las cajas de texto dentro de un Panel.
        /// </summary>
        /// <param name="panel">El panel.</param>
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

        /// <summary>
        /// Asigna la validación a todos los cuadros de texto de el panel.
        /// </summary>
        /// <param name="panel">El panel.</param>
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

        /// <summary>
        /// Realiza validación estandar a un cuadro de texto.
        /// </summary>
        /// <param name="txt">El cuadro de texto.</param>
        /// <returns></returns>
        public static bool validarTextBoxEstandar(TextBox txt)
        {
            //var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
            var regexItem = new Regex(@"^\w*(\s\w*)*$");
            bool estado = true;
            if (!regexItem.IsMatch(txt.Text)) estado = false;
            if (txt.Text.Length < 3) estado = false;

            if (!estado)
            {
                txt.Background = Constantes.ErrorBrush;

                //txt.Background.Opacity = 70;
            }
            return estado;
        }

        /// <summary>
        /// Realiza la validación necesario para cersiorarse de que el texto en un cuadro de téxto es un número telefónico de 10 dígitos..
        /// </summary>
        /// <param name="txt">El cuadro de texto.</param>
        /// <returns></returns>
        public static bool validarTextBoxTelefono(TextBox txt)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
            bool estado = true;
            long t;
            if (!regexItem.IsMatch(txt.Text)) estado = false;
            if (!long.TryParse(txt.Text, out t)) estado = false;
            if (txt.Text.Length != 10) estado = false;
            if (!estado)
            {
                txt.Background = Constantes.ErrorBrush;

                //txt.Background.Opacity = 70;
            }
            return estado;
        }

        /// <summary>
        /// Realiza la validación necesario para cersiorarse de que el texto en un cuadro de téxto es un número .
        /// </summary>
        /// <param name="txt">El cuadro de texto.</param>
        /// <returns></returns>
        public static bool validarTextBoxNumero(TextBox txt)
        {
            bool estado = true;
            int t;
            if (!int.TryParse(txt.Text, out t)) estado = false;
            if (txt.Text.Length < 1) estado = false;

            if (!estado)
            {
                txt.Background = Constantes.ErrorBrush;

                //txt.Background.Opacity = 70;
            }
            return estado;
        }

        /// <summary>
        /// Validar la entrada para ver si es un numero de tarjeta válido.
        /// </summary>
        /// <param name="input">La entrada.</param>
        /// <returns>Verdadero si es válido, Falso si es inválido</returns>
        static public bool validarCuenta(string input)
        {
            long t;
            if (input.Length == 12 && long.TryParse(input, out t)) return true;
            return false;
        }

        /// <summary>
        /// Validar la entrada para ver si es una cantidad de dinero válida.
        /// </summary>
        /// <param name="input">La entrada.</param>
        /// <returns>Verdadero si es válido, Falso si es inválido</returns>
        static public bool validarDinero(string input)
        {
            decimal t;
            if (decimal.TryParse(input, out t)) return true;
            return false;
        }

        /// <summary>
        /// Deshabilita todos los controles en el Panel.
        /// </summary>
        /// <param name="panel">El panel.</param>
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

        /// <summary>
        /// Habilita todos los controles en el Panel.
        /// </summary>
        /// <param name="panel">El panel.</param>
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

        /// <summary>
        /// Devuelve el valor mas repetido en una lista.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datos">Los datos.</param>
        /// <param name="moda">Los valores mas repetidos.</param>
        /// <returns>El número de veces que se repitió el valor mas repetido.</returns>
        public static int masRepetido<T>(List<T> datos, out HashSet<T> moda) where T : IComparable<T>
        {
            moda = new HashSet<T>();
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

                if (valor.CompareTo(valorActual) == 0 && !last)
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