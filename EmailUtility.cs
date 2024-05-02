using System.Data;

namespace GRCServices
{
    public static class EmailUtility
    {
        private static IConfiguration? _cApp;

        public static void Email(IConfiguration configuration)
        {
            _cApp = configuration;
        }

        public static string SendEmail(string sSubj, string sBody, string toAddresses)
        {
            var host = _cApp.GetSection(key: "Email:Host").Value;
            var port = _cApp.GetSection(key: "Email:Port").Value;
            var userid = _cApp.GetSection(key: "Email:UserID").Value;
            var pwd = _cApp.GetSection(key: "Email:Key").Value;
            var enablessl = _cApp.GetSection(key: "Email:EnableSSL").Value;

            if (host == null || port == null || userid == null)
                throw new Exception("EmailUtility:SendEmail - Missing App Settings info for Email");

            try
            {
                string _sSubj = sSubj;
                string _sHost = host;
                int _port = Convert.ToInt32(port);
                string _sUserID = userid;
                string? _sPwd = pwd;
                bool _bEnableSsl = Convert.ToBoolean(enablessl);

                GRCServices.EmailService emSvc = new GRCServices.EmailService(_sHost, _port, _sUserID, _sPwd, _bEnableSsl);
                emSvc.SendEmail(toAddresses, "mailt9745@gmail.com", _sSubj, sBody);
            }
            catch (ArgumentNullException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "";
        }

        public static string GetTableHtml(DataTable dataTable)
        {
            try
            {

                string messageBody = " <table style=\"border:0px solid #cfc5c5; border-collapse: collapse;\"><tr style=\"background:#e2e2e2\">";

                foreach (DataColumn column in dataTable.Columns)
                {
                    messageBody += "<td style=\"border:1px solid #cfc5c5; border-collapse: collapse;text-align:center;padding:5px;\"><b>" + column.ColumnName + "</b></td>";
                }
                messageBody += "</tr>";

                for (int loopCount = 0; loopCount < dataTable.Rows.Count; loopCount++)
                {
                    messageBody += "<tr>";
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        messageBody += "<td style=\"border:1px solid #cfc5c5; border-collapse: collapse;\">" + dataTable.Rows[loopCount][i] + "</td>";
                    }
                    messageBody += "</tr>";
                }
                messageBody += "</table>";

                return messageBody;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
