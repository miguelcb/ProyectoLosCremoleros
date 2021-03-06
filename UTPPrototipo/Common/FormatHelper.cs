﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace UTPPrototipo.Common
{
    public static class FormatHelper
    {
        public static string FormatoFrecuencia(this HtmlHelper helper, DateTime fecha)
        {
            DateTime fechaactual = DateTime.Now;
            TimeSpan ts = fechaactual - fecha;
            string fechatexto = "01/01/2014";
            if (ts.Seconds <= 15 && ts.Minutes == 0 && ts.Hours == 0 && ts.Days == 0)
            {
                fechatexto = "Hace un momento";
            }
            else
            {
                if (ts.Seconds <= 59 && ts.Minutes == 0 && ts.Hours == 0 && ts.Days == 0)
                {
                    fechatexto = "Hace " + ts.Seconds + " segundo";
                }
                else
                {
                    if (ts.Minutes <= 59 && ts.Hours == 0 && ts.Days == 0)
                    {
                        if (ts.Minutes == 1) fechatexto = "Hace " + ts.Minutes + " minuto"; //Se puede ingresar directamente Hace 1 minuto
                        else fechatexto = "Hace " + ts.Minutes + " minutos";
                    }
                    else
                    {
                        if (ts.Hours <= 23 && ts.Days == 0)
                        {
                            fechatexto = "Hace " + ts.Hours + " horas";
                        }
                        else
                        {
                            if (ts.Days <= 7)
                            {
                                if (ts.Days == 1)
                                    fechatexto = "Hace " + ts.Days + " día";
                                else
                                    fechatexto = "Hace " + ts.Days + " días";
                            }
                            else
                            {
                                fechatexto = fecha.ToString("dd/MM/yyyy");
                            }
                        }
                    }
                }
            }




            return fechatexto;
        }

        public static string FormatoFechaAnioMes(this HtmlHelper helper, int mes, int anio)
        {
            string fechatexto = string.Empty;
            if (mes > 0 && anio > 0)
            {
                fechatexto = mes.ToString("0#") + "/" + anio.ToString();
            }
            else
            {
                fechatexto = "Actualidad";
            }

            return fechatexto;
        }
        public static string FormatoFechaAnio(this HtmlHelper helper, int anio)
        {
            string fechatexto = string.Empty;
            if (anio == 1)
            {
                fechatexto = anio.ToString() + " año";
            }
            else if (anio > 1)
            {
                fechatexto = anio.ToString() + " años";
            }

            return fechatexto;
        }
        public static string FormatoFechaMes(this HtmlHelper helper, int mes)
        {
            string fechatexto = string.Empty;
            if (mes == 1)
            {
                fechatexto = mes.ToString() + " mes";
            }
            else if (mes > 1)
            {
                fechatexto = mes.ToString() + " meses";
            }

            return fechatexto;
        }


        //public static FileContentResult Imagen(this HtmlHelper helper, byte[] imagen, string MimeType)
        //{
        //    if (imagen != null && string.IsNullOrEmpty(MimeType)==false) return new FileContentResult(imagen, MimeType);
        //    else return null;
        //}

        public static HtmlString Imagen(this HtmlHelper helper, byte[] image)
        {
            HtmlString hs =
                  new HtmlString(string.Format("<image src=\"{0}\" alt='alt text' />",
                                                  new FileContentResult(image, "image/jpeg")));
            return hs;
        }

        public static string Cumple(this HtmlHelper helper, bool estado)
        {
            string cumple = string.Empty;
            if (estado)
                cumple = "Si Cumple";
            else
                cumple = "No Cumple";
            return cumple;
        }


        public static string CompatibilidaOferta(this HtmlHelper helper, int compatiblidad)
        {

            string estilo = string.Empty;
            StringBuilder sbhtml = new StringBuilder();
            if (compatiblidad >= 80)
            {
                estilo = "progress-bar-success";
            }
            else if (compatiblidad < 80 && compatiblidad >= 60)
            {
                estilo = "progress-bar-warning";
            }
            else if (compatiblidad < 60)
            {
                estilo = "progress-bar-danger";
            }

            sbhtml.AppendLine("<div class='progress-bar " + estilo + " progress-bar' role='progressbar' aria-valuenow='" + (compatiblidad + 10).ToString() + "' aria-valuemin='0' aria-valuemax='100' style='width:" + (compatiblidad + 10).ToString() + "%;'>");
            sbhtml.AppendLine(compatiblidad.ToString() + " %");
            sbhtml.AppendLine("</div>");
            return sbhtml.ToString();
        }

        public static string UppercaseFirst(this HtmlHelper helper, string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }

}