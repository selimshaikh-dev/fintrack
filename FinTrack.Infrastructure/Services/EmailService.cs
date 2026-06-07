using Amazon.Runtime;
using Amazon.SimpleEmail.Model;
using Amazon.SimpleEmail;
using FinTrack.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using FinTrack.Infrastructure.Utilities;

namespace FinTrack.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private string _awsAccessKey = "";
        private string _awsSecretKey = "";
        private string _awsSenderEmail = "<notification@globedse.net>";
        public async Task<HttpStatusCode> SendWithAwsAsync(string senderEmailDisplayName, string receiverEmail, string subject, string emailBody, string subjectLineForTemplate, string receiverEmailDisplayName)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            string senderEmail;
            try
            {
                SiteCredentials cred = new SiteCredentials();

                senderEmail = $"{senderEmailDisplayName} {_awsSenderEmail}";

                if (!string.IsNullOrEmpty(receiverEmailDisplayName))
                {
                    receiverEmail = $"{receiverEmailDisplayName} <{receiverEmail}>";
                }

                var awsCred = new BasicAWSCredentials(_awsAccessKey, _awsSecretKey);

                using (var client = new AmazonSimpleEmailServiceClient(awsCred, RegionEndpoint.USWest2))
                {
                    var emailRequest = new SendEmailRequest()
                    {
                        Source = senderEmail,
                        Destination = new Destination()
                        {
                            ToAddresses = new List<string>() { receiverEmail }
                        },
                        Message = new Message
                        {
                            Body = new Body
                            {
                                Html = new Content
                                {
                                    Charset = "UTF-8",
                                    Data = emailBody
                                }
                            },
                            Subject = new Content(subject)
                        }
                    };
                    try
                    {
                        var response =await client.SendEmailAsync(emailRequest);
                        statusCode = response.HttpStatusCode;
                    }
                    catch (Exception)
                    {
                        return statusCode;
                    }
                }

            }
            catch (Exception)
            {
                return statusCode;
            }
            return statusCode;
        }
    }
}
