using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Adibrata.Framework.Messaging
{

    [Serializable]
    public class MessageEntities
    {
        public string UserName { get; set;}
        public string MessageLabel { get; set;  }
        public string MessageContent { get; set;}
        public string MessageName { get; set;}
    }

    [Serializable]
    public class MailEntities
    {
        public string UserMail { get; set; }
        public string PasswordMail { get; set; }
        public string DomainMail { get; set; }
        public string From { get; set; }

        public string To { get ; set; }
        public string CC { get; set; }
        public string BCC { get; set; }

        public string Attahment { get; set; }
        public string MailBody { get; set;}
        public string MailSubject { get; set;}
        public bool UseDefaultCredentials { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["UseDefaultCredentials"].ToString()); } }
        public string SMTPServer
        {
            get { return ConfigurationManager.AppSettings["SMTPServer"].ToString(); }
        }
        public int SMTP_Port { get { return Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString()); } }
        public bool EnableSSL { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"].ToString()); }}
        public System.Net.Mail.SmtpClient SmtpConfiguration { get; set; }
        public System.Net.Mail.MailMessage MailConfiguration { get; set; }
    }


    [Serializable]
    public class WCFEntities
    {
        public string DicExtString { get; set; }
        public string DicFileString { get; set; }
        
    }

    [Serializable]
    public class BITSEntities
    {
        public string ServerName { get; set; }
        public string LocalFileName { get; set; }
        public string RemoteFileName { get; set; }
        public string Type { get; set; }

        public int SMTP_Port { get { return Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString()); } }
        public bool EnableSSL { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"].ToString()); } }
        public System.Net.Mail.SmtpClient SmtpConfiguration { get; set; }
        public System.Net.Mail.MailMessage MailConfiguration { get; set; }
    }
}
