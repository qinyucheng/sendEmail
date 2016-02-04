using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using System.Configuration;  //read web.config xml files
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Text;
using System.Xml;
using System.Net.Mail;
namespace sendEmail.Controllers
{
   
    public class sendEmailController : ApiController
    {

        public string Post(getContent getValues)
        {
            if (getValues.Name != null)
            {
                string getStatu=sendGmail(getValues.Name, getValues.Email, getValues.Message);
                return getStatu;
            }
            else
            {
                return "failure";
            }

        }
        public string sendGmail(string name, string userEmail, string value)
        {
            string reciverEmail = "AutoTutorMem@gmail.com";
            string content = "'" + name + "' send a new message from AutoTutor Website\n\n Email address: " + userEmail + "\n\n Message content: " + value;
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(reciverEmail);
            msg.To.Add(reciverEmail);
            msg.Subject = "From AutoTutor Website! " + DateTime.Now.ToString();
            msg.Body = content;
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(reciverEmail, "TNMemFIT410");
            client.Timeout = 20000;
            try
            {
                client.Send(msg);
                return "Mail has been successfully sent!";
            }
            catch (Exception ex)
            {
                return "Fail Has error" + ex.Message;
            }
            finally
            {
                msg.Dispose();
            }
        }

    }
   

    public class getContent
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
