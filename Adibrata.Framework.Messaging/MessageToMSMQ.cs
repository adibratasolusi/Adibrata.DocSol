using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adibrata.Framework.Logging;
using System.Messaging;

namespace Adibrata.Framework.Messaging
{
    public static class MessageToMSMQ
    {
        
        public static void SendMessageToMSMQ()
        {
            Message _msg = new Message();
            MessageEntities _ent = new MessageEntities();
            try
            {
                _msg.Label = _ent.MessageLabel;
                _msg.Body = _ent.MessageContent;
                _msg.UseDeadLetterQueue = true;  // to send the message to the dead letter queue in case if there is some issue while sending. 
                //Create an object of the queue to which you want to send the message:
                MessageQueue msgQ = new MessageQueue(".\\private$\\QueueName");
                msgQ.Send(_msg);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "MSMQ",
                    NameSpace = "Adibrata.Framework.Messaging",
                    ClassName = "MessageToMSMQ",
                    FunctionName = "SendMessageToMSMQ",
                    ExceptionNumber = 1,
                    EventSource = "Messaging",
                    ExceptionObject = _exp,
                    EventID = 1,
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        public static MessageEntities ReceiveMessageFromMSMSQ()
        {
            MessageEntities _ent = new MessageEntities();
            MessageQueue msgQ = new MessageQueue(".\\private$\\QueueName");
            Message _msg = new Message();
            try
            {
                _msg = msgQ.Receive();
                _ent.MessageContent = _msg.Body.ToString();   

            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserLogin = "MSMQ",
                    NameSpace = "Adibrata.Framework.Messaging",
                    ClassName = "MessageToMSMQ",
                    FunctionName = "ReceiveMessageFromMSMSQ",
                    ExceptionNumber = 1,
                    EventSource = "Messaging",
                    ExceptionObject = _exp,
                    EventID = 1,
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _ent;
        }
    }
}
