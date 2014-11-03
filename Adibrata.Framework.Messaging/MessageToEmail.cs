using Adibrata.Framework.Logging;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading; 

namespace Adibrata.Framework.Messaging
{
    // Cara Penggunaan 

    //MailEntities _ent = new MailEntities
    //{

    //    From = "henry@ad-ins.com",
    //    To = "henry@ad-ins.com",
    //    MailSubject = "Test Email",
    //    UserMail = "henry",
    //    PasswordMail = "",
    //    DomainMail = "ad-ins.com",
    //    MailBody = "<b>Test Mail tanpa List </b>",
    //};
    //MessageToEmail i = new MessageToEmail(_ent);

    public class MessageToEmail
    {
        private Semaphore sem = new Semaphore(10, 10);
        static MailMessage _mail = new MailMessage();
        static SmtpClient _smtp = new SmtpClient();
        public MessageToEmail(MailEntities _ent)
        {
            try
            {
                _ent.SmtpConfiguration = SMTPConfiguration(_ent);
                _ent.MailConfiguration = MailConfiguration(_ent);
                SendMessageToEmail(_ent);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "EMAIL",
                    NameSpace = "Adibrata.Framework.Messaging",
                    ClassName = "MessageToEmail",
                    FunctionName = "Constructor",
                    ExceptionNumber = 1,
                    EventSource = "Email",
                    ExceptionObject = _exp,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void SendMessageToEmail(MailEntities _ent)
        {
          
            try
            {
               
                sem.WaitOne();
                ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(SendMessageThread), _ent);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "EMAIL",
                    NameSpace = "Adibrata.Framework.Messaging",
                    ClassName = "MessageToEmail",
                    FunctionName = "SendMessageToEmail",
                    ExceptionNumber = 1,
                    EventSource = "Email",
                    ExceptionObject = _exp,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        private void SendMessageThread(object _obj)
        {
            MailEntities _ent = (MailEntities)_obj;
            try
            {
                _ent.SmtpConfiguration.Send(_ent.MailConfiguration);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "EMAIL",
                    NameSpace = "Adibrata.Framework.Messaging",
                    ClassName = "MessageToEmail",
                    FunctionName = "SendMessageThread",
                    ExceptionNumber = 1,
                    EventSource = "Email",
                    ExceptionObject = _exp,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            finally
            {
                sem.Release();
            }
        }
      
        private static SmtpClient SMTPConfiguration(MailEntities _ent)
        {
            SmtpClient _smtp = new SmtpClient();
            try
            {
                _smtp.Credentials = new NetworkCredential(_ent.UserMail, _ent.PasswordMail, _ent.DomainMail);
                _smtp.Host = _ent.SMTPServer;
                _smtp.Port = _ent.SMTP_Port;
                _smtp.UseDefaultCredentials = _ent.UseDefaultCredentials;
                _smtp.EnableSsl = _ent.EnableSSL;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "EMAIL",
                    NameSpace = "Adibrata.Framework.Messaging",
                    ClassName = "MessageToEmail",
                    FunctionName = "SMTPConfiguration",
                    ExceptionNumber = 1,
                    EventSource = "Email",
                    ExceptionObject = _exp,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _smtp;
        }

        private static MailMessage MailConfiguration (MailEntities _ent)
        {
            MailMessage _mail = new MailMessage();
            try
            {
                _mail.From = new MailAddress(_ent.From);
                _mail.To.Add(_ent.To.Replace(";", ","));
                if (_ent.CC != null) { _mail.CC.Add(_ent.CC.Replace(";", ",")); }
                if (_ent.BCC != null) { _mail.Bcc.Add(_ent.BCC.Replace(";", ",")); }
           
                //foreach (string _tomail in _ent.To) { _mail.To.Add(_tomail); }
                //if (_ent.CC != null) foreach (string _ccmail in _ent.CC) {_mail.CC.Add(_ccmail);}
                //if (_ent.BCC != null) foreach (string _bccmail in _ent.BCC) { _mail.Bcc.Add(_bccmail); }
           
                _mail.Body = _ent.MailBody;
                _mail.Subject = _ent.MailSubject;
                _mail.IsBodyHtml = true;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "EMAIL",
                    NameSpace = "Adibrata.Framework.Messaging",
                    ClassName = "MessageToEmail",
                    FunctionName = "MailConfiguration",
                    ExceptionNumber = 1,
                    EventSource = "Email",
                    ExceptionObject = _exp,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _mail;

        }

    }
}
