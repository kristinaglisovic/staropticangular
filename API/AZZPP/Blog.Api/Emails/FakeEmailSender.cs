using Blog.Application.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Api.Emails
{
    public class FakeEmailSender : IEmailSender
    {
        public void SendEmail(MailDto mail)
        {
            Console.WriteLine("Sending email to: "+ mail.To);
            Console.WriteLine("Title: " + mail.Title);
            Console.WriteLine("Body: " + mail.Body);
        }
    }
}
