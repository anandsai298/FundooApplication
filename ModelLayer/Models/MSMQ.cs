using com.sun.org.apache.xerces.@internal.impl.msg;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using Experimental.System.Messaging;


namespace ModelLayer.Models
{
    public class MSMQ
    {
        //object ref for messagequeue
        MessageQueue messageQueue = new MessageQueue();
        private string recieverEmail;
        private string recieverName;
        //send token using MessageQueue and Delegate
        public void SendMessage(string token,string Email,string Name)
        {
            recieverEmail = Email;
            recieverName = Name;
            messageQueue.Path = @".\Private$\Token";
            try
            {
                if(!MessageQueue.Exists(messageQueue.Path))
                {
                    MessageQueue.Create(messageQueue.Path);
                }
                messageQueue.Formatter =new XmlMessageFormatter(new Type[] { typeof(string) });
                messageQueue.ReceiveCompleted += MessageQueue_RecieveCompleted;
                messageQueue.Send(token);
                messageQueue.BeginReceive();
                messageQueue.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //delegate to send token as message to sender Email using smtp and mailmessage
        private void MessageQueue_RecieveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = messageQueue.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials= new NetworkCredential("anandsai298@gmail.com", "wurubwccbiqlkfqt"),
                };
                mailMessage.From = new MailAddress("anandsai298@gmail.com");
                mailMessage.To.Add(new MailAddress(recieverEmail));
                string mailBody = $"<!DOCTYPE html>" + $"<html>" + $"<style>" + $".blink" + $"</style>" +
                                    $"<body style =\"background-color:White;text-align:center;padding:5px;\">" +
                                    $"<h1 style =\"color:Red;border-bottom:3px sokid #84AF08;margin-top:5px;\"> Dear <b>{recieverName}</b> </h1>\n" +
                                    $"<h3 style = \"color:#BAB411;\"> For resetting Password the Below Link is Issued</h3>" +
                                    $"<h3 style = \"color:#BAB411;\"> Please Click the Link Below To Reset Your Password</h3>" +
                                    $"<a style =\"color:#00802b; text-decoration:none;font-size:20px;\"href=''>Click me</a>" +
                                    $"<h3 style = \"color:#BAB411;margin-bottom:5px;\"><blink> This Token is valid for Next 6 hrs<blink></h3>" +
                                    $"</body>" + $"</html>";
                mailMessage.Body = mailBody;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = "Fundoo Notes Password Reset Link";
                smtpClient.Send(mailMessage);
            }
            catch(Exception ex)
            { 
                throw new Exception(ex.Message); 
            }
        }
    }
}
