using System.Net.Mail;
using System.Text;

namespace GRCServices
{
    public class EmailService
    {
        private readonly string _sHost;
        private readonly int _port;
        private readonly string _sUserID;
        private readonly string? _sPwd;
        private readonly bool _bEnableSsl;
        public EmailService(string sHost, int port, string sUserID, string? sPwdorCode, bool bEnableSsl)
        {
            _sHost = sHost;
            _port = port;
            _sUserID = sUserID;
            _sPwd = sPwdorCode;
            _bEnableSsl = bEnableSsl;
        }

        public bool SendEmail(string sToAddresses, string sFromAddress, string sSubject, string sMailbodyasHtml)
        {
            try
            {
                string sMsg = "";
                if (_sHost == null || _sHost == "")
                    sMsg += "Host shoud not be Empty;\t";

                if (_port <= 0)
                    sMsg += "Port is not valid;\t";

                if (_sUserID == null || _sUserID == "")
                    sMsg += "UserID should not be Empty;\t";

                if (sToAddresses == null || sToAddresses == "")
                    sMsg += "There are no To Addresses mentioned;\t";

                if (sFromAddress == null || sFromAddress == "")
                    sMsg += "There are np from Address mentioned;\t";

                if (sMsg != "")
                {
                    throw new ArgumentNullException(sMsg);
                }

                MailMessage message = new MailMessage(sFromAddress, sToAddresses, sSubject, sMailbodyasHtml);
                message.From = new MailAddress(sFromAddress, "TECHOPTIMA_GRC");
                message.BodyEncoding = Encoding.UTF8;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(message.Body, null, "text/html");
                //LinkedResource logo = new LinkedResource(@"C:\Providerportalsvc\LoginService\Firstsource-logo.png", "image/png");
               // LinkedResource logo = new LinkedResource(@"D:\ProviderPortal\Release_folders\fshealth-apiservices-restructured-code\FSHealthCare\FSHealthBackend\LoginService\Firstsource-logo.png", "image/png");
                //logo.ContentId = "logoImage"; // set a unique content ID
                //htmlView.LinkedResources.Add(logo);
                message.AlternateViews.Add(htmlView);
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient(_sHost, _port);
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential(_sUserID, _sPwd);
                client.EnableSsl = _bEnableSsl;
                client.UseDefaultCredentials = false;
                client.Credentials = basicCredential1;

                client.Send(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }
    }
}
