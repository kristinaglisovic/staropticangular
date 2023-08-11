using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Emails
{
    public interface IEmailSender
    {
        void SendEmail(MailDto mail);
    }
}
