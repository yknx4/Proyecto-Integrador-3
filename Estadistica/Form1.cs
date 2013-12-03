﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Estadistica
{
    public partial class frmEstadistica : Form
    {
        public frmEstadistica():this(new List<Double>())
        {
            
            
        }
        public frmEstadistica(List<Double> valores) {
            InitializeComponent();
            datos = new List<Double>();
            Moda = new List<double>();
            dtgrDatos = valores;
        }

        List<Double> dtgrDatos;


        void llenarTabla()
        {
            /*Limpieza de la tabla*/
            dtgrTabla.Rows.Clear();
            dtgrTabla.Rows.Insert(0, numeroClaseRedondead());

            /*Codigo para llenar la tabla*/
            int intervaloClase = (int)datos.First();
            int frecuenciaAcumulada = 0;
            int numeroClaseRedondeado = numeroClaseRedondead();
            for (int i = 0; i < numeroClaseRedondeado; i++)
            {

                /*Marcas de Clase*/
                dtgrTabla[0, i].Value = (i + 1).ToString();
                /*Intervalos de Clase*/
                int intervaloInicial = intervaloClase;
                intervaloClase += (int)Math.Round(intClase, MidpointRounding.AwayFromZero);
                dtgrTabla[1, i].Value = intervaloInicial.ToString() + " - " + intervaloClase.ToString();

                /*Frecuencia*/
                int frecuencia = 0;
                foreach (double dato in datos)
                {
                    if (dato > intervaloClase) break;
                    if (dato >= intervaloInicial) frecuencia++;
                }
                dtgrTabla[2, i].Value = frecuencia.ToString();

                /*Frecuencia Acumulada*/
                frecuenciaAcumulada += frecuencia;
                dtgrTabla[3, i].Value = frecuenciaAcumulada.ToString();

                /*Frecuencia Relativa*/
                double frecuenciaRelativa = (double)frecuencia / (double)cantidadDatos();
                //MessageBox.Show(frecuencia.ToString() +" "+cantidadDatos().ToString());
                dtgrTabla[4, i].Value = Math.Round(frecuenciaRelativa, 3).ToString();

                /*frecuenciaRelativaAcumulada*/
                double frecuenciaRelativaAcumulada = (double)frecuenciaAcumulada / (double)cantidadDatos();
                dtgrTabla[5, i].Value = Math.Round(frecuenciaRelativaAcumulada, 3).ToString();

                /*Marca de Clase*/
                double marcaDeClase = ((double)(intervaloClase + intervaloInicial)) / 2;
                dtgrTabla[6, i].Value = Math.Round(marcaDeClase, 3).ToString();

                /*Grados*/
                double grados = ((double)(360 * frecuencia)) / cantidadDatos();
                dtgrTabla[7, i].Value = Math.Round(grados, 3).ToString();

                /*Acomodar para el siguiente registro*/
                intervaloClase++;
            }
        }
        void calcularPromedio()
        {
            double suma = 0;
            foreach (double value in datos)
            {
                suma += value;
            }
            promedio = suma / cantidadDatos();
            lblPromedio.Text = Math.Round(promedio, 3).ToString();
        }
        void calcularNumeroDeClase()
        {
            int cantidadDeDatos = cantidadDatos();
            if (cantidadDeDatos < 50)
            {
                numeroClase = 1 + 3.3 * Math.Log10(cantidadDeDatos);
            }
            else
            {
                numeroClase = 3 + 3.3 * Math.Log10(cantidadDeDatos);
            }
            lblNoClase.Text = Math.Round(numeroClase, 3).ToString() + " ~ " + Math.Round(numeroClase, MidpointRounding.AwayFromZero).ToString();
        }

        /// <summary>
        /// Calculars the moda.
        /// </summary>
        void calcularModa()
        {
            if (datos.First() == datos.Last())
            {
                lblModa.Text = datos.First().ToString();
                return;
            }
            Moda.Clear();
            lblModa.Text = "";
            double valorActual = datos.First();
            //MessageBox.Show(valorActual.ToString());
            int countValorActual = 0;
            int countValorMasFrecuente = 0;
            for (int j = 0; j <= datos.Count;j++ )
            {
                double valor = 0;
                bool last = j == datos.Count;
                if(!last) valor = datos[j];
                if (valor == valorActual && !last)
                {
                    countValorActual++;
                } 
                else
                {
                    if (countValorActual == countValorMasFrecuente)
                    {
                        Moda.Add(valorActual);
                    } 
                    else if(countValorActual>countValorMasFrecuente)
                    {
                        countValorMasFrecuente = countValorActual;
                        Moda.Clear();
                        Moda.Add(valorActual);
                    }
                    if (!last)valorActual = valor;
                    countValorActual = 1;
                }
            }



            foreach (double valor in Moda)
            {
                lblModa.Text += valor.ToString() + " ";
            }

        }
        void calcularVarianza()
        {
            double temporal = 0;
            foreach (double dato in datos)
            {
                temporal += Math.Pow(dato - promedio, 2);
            }
            varianza = temporal / (cantidadDatos() - 1);
            desvEstandar = Math.Sqrt(varianza);
            lblVarianza.Text = Math.Round(varianza, 3).ToString();
            lblDesvEsta.Text = Math.Round(desvEstandar, 3).ToString();

        }
        void calcularIntervaloDeClase()
        {
            /*lblMediana.Text = datos.First().ToString();
            lblModa.Text = datos.Last().ToString();*/
            intClase = (datos.Last() - datos.First()) / ((int)Math.Round(numeroClase, MidpointRounding.AwayFromZero));
            lblIntClase.Text = Math.Round(intClase, 3).ToString() + " ~ " + Math.Round(intClase, MidpointRounding.AwayFromZero).ToString();
        }
        void calcularMediana()
        {
            int numeroDeDatos = cantidadDatos();
            if (numeroDeDatos == 1)
            {
                Mediana = datos.First();
                lblMediana.Text = Mediana.ToString();
                return;
            }
            if (numeroDeDatos % 2 == 0)
            {
                Mediana = (datos[(numeroDeDatos / 2) - 1] + datos[(numeroDeDatos / 2)]) / 2;
            }
            else
            {
                Mediana = datos[((numeroDeDatos - 1) / 2)];
            }
            lblMediana.Text = Mediana.ToString();
        }
        int cantidadDatos()
        {
            return dtgrDatos.Count - 1;
        }

        void refreshData()
        {
            datos.Clear();
            double valor;
            for (int i = 0; i < cantidadDatos(); i++)
            {
                valor = dtgrDatos[i];
                datos.Add(valor);
            }
            datos.Sort();
        }

        List<Double> datos;
        double numeroClase;
        double intClase;
        double varianza;
        double promedio;
        double desvEstandar;
        double Mediana;
        List<double> Moda;

        


        void rehacer()
        {
            if (cantidadDatos() == 0) return;

            refreshData();
            calcularPromedio();
            calcularNumeroDeClase();
            calcularIntervaloDeClase();
            calcularMediana();
            calcularVarianza();
            calcularModa();
            llenarTabla();
        }

        private int numeroClaseRedondead()
        {
            return (int)(Math.Round(numeroClase, MidpointRounding.AwayFromZero));
        }
        private void cuandoDatosCambian(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // rehacer();
        }

        private void cuandoDatosCambian(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            rehacer();
        }

        private void cuandoValorCambia(object sender, DataGridViewCellEventArgs e)
        {
            rehacer();
        }

       
    }
}
