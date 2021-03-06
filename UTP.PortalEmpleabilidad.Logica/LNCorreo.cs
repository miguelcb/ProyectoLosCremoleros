﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTP.PortalEmpleabilidad.Logica
{
    public static class LNCorreo
    {
        public static async Task<bool> EnviarCorreo(Mensaje mensaje)
        {
            try
            {
                bool mensajesEnProduccion = Convert.ToBoolean(ConfigurationManager.AppSettings["MensajeCorreoEnProduccion"]);
                string usuario = ConfigurationManager.AppSettings["MensajeCorreoUsuario"];
                string contrasena = ConfigurationManager.AppSettings["MensajeCorreoContrasena"];
                string host = ConfigurationManager.AppSettings["MensajeCorreoHost"];
                int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["MensajeCorreoPuerto"]);
                bool enableSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["MensajeCorreoEnableSSL"]);
                string deDesarrollo = ConfigurationManager.AppSettings["MensajeCorreoUsuarioDeDesarrollo"];
                string paraDesarrollo = ConfigurationManager.AppSettings["MensajeCorreoUsuarioParaDesarrollo"];
                string prefijoAsunto = "Portal de Empleabilidad: ";  //ConfigurationManager.AppSettings["MensajeCorreoPrefijoAsunto"];

                //MailMessage Message = new MailMessage(mensajesEnProduccion ? mensaje.DeUsuarioCorreoElectronico : deDesarrollo,
                //                                        mensajesEnProduccion ? mensaje.ParaUsuarioCorreoElectronico : paraDesarrollo
                //                                      );

                MailMessage Message = new MailMessage();

                //Cambiar a ConfigurationManager.AppSettings["MensajeCorreoAliasFrom"], Agregar tag en web.config
                string correoDe = mensajesEnProduccion ? mensaje.DeUsuarioCorreoElectronico : deDesarrollo;
                string correoPara = mensajesEnProduccion ? mensaje.ParaUsuarioCorreoElectronico : paraDesarrollo;
                string correoDeNombre = mensaje.DeUsuarioNombre == null ? "" :  mensaje.DeUsuarioNombre;
                string correoParaNombre = mensaje.ParaUsuarioNombre == null ? "" : mensaje.ParaUsuarioNombre;

                Message.From = new MailAddress(correoDe, correoDeNombre, Encoding.UTF8);
                Message.To.Add(new MailAddress(correoPara, correoParaNombre, Encoding.UTF8));
                
              
                Message.Body = mensaje.MensajeTexto;
                Message.Subject = prefijoAsunto + " " + mensaje.Asunto;
                Message.SubjectEncoding = Encoding.UTF8;

                SmtpClient client = new SmtpClient();
                client.Host = host;
                client.Port = puerto;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //27MAR15: Comentado en desarrollo para homolgar con UTP.
                //Notar que en desarrollo el correo NO se envía al estar comentada estas dos líneas.
                //client.EnableSsl = enableSSL;
                //client.Credentials = new NetworkCredential(usuario, contrasena);  //Cuenta en criteria para el envío de correos.
                client.Send(Message);
                await Task.Yield();
                
                //await client.SendAsync(Message, "token");
                return true;
            }
            catch (Exception ex)
            {
                int a = 0;
                return false;
            }
        }

    }
}
