using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task<HttpStatusCode> SendWithAwsAsync(string senderEmailDisplayName, string receiverEmail, string subject, string emailBody,
            string subjectLineForTemplate, string receiverEmailDisplayName);
    }
}
