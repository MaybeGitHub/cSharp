using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.IO;
using System.Drawing;
using System.Net.Mail;
using System.Net;

namespace LibreriaAgapea.App_Code.Controladores
{
    public class CMensajeria
    {
        private static void crearPdf()
        {
            PdfDocument pdf = new PdfDocument();
            PdfPageBase pagina = pdf.Pages.Add();
            string[] lineasTxT = File.ReadAllLines(CFichero.rutaLibros);
            string salida = "";
            foreach (string linea in lineasTxT)
            {
                salida += linea + "\n";
            }
            pagina.Canvas.DrawString(salida, new PdfFont(PdfFontFamily.TimesRoman, 14), new PdfSolidBrush(Color.Black), 10, 10);
            pdf.SaveToFile("Recibo.pdf");            
            pdf.Close();
        }

        public static bool mandarMail(string email)
        {            
            crearPdf();
            MailMessage nuevoCorreo = new MailMessage();
            nuevoCorreo.To.Add(new MailAddress(email));
            nuevoCorreo.From = new MailAddress("proyectos.clase.net@gmail.com");
            nuevoCorreo.Subject = "Factura compra en Libreria Agapea";
            nuevoCorreo.Attachments.Add(new Attachment("Recibo.pdf"));
            nuevoCorreo.Body = "Gracias por usar nuestra Pagina";

            SmtpClient servidor = new SmtpClient();
            servidor.Host = "smtp.gmail.com";
            servidor.Port = 587;
            servidor.EnableSsl = true;
            servidor.DeliveryMethod = SmtpDeliveryMethod.Network;
            servidor.Credentials = new NetworkCredential("proyectos.clase.net@gmail.com", "solosequenosenada");
            servidor.Timeout = 20000;
            try {
                servidor.Send(nuevoCorreo);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}