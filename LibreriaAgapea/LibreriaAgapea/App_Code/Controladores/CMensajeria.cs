using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.IO;
using System.Drawing;
using System.Net.Mail;

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

        public static void mandarMail(string email)
        {
            crearPdf();
            MailMessage nuevoCorreo = new MailMessage();
            nuevoCorreo.To.Add(new MailAddress(email));
            nuevoCorreo.From = new MailAddress(email);
            nuevoCorreo.Subject = "Factura compra en Libreria Agapea";
            nuevoCorreo.Attachments.Add(new Attachment("Recibo.pdf"));
            nuevoCorreo.Body = "Gracias por usar nuestra Pagina";

            SmtpClient servidor = new SmtpClient();
            servidor.Host = "smtp-relay.gmail.com";
            servidor.Port = 25;
            servidor.Credentials = new System.Net.NetworkCredential(email, "1jesusin2unibus");
            try {
                servidor.Send(nuevoCorreo);
            }
            catch
            {

            }
        }
    }
}