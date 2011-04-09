using System.Linq;
using System.Net;
using System.Net.Mail;
using FCOTravelAlerts.Service.Entity;
using FCOTravelAlerts.Service.Repository;

namespace FCOTravelAlerts.Service.Notifier
{
    public class SMTPNotifier : IFCONotifier
    {
        private readonly IUserRepository _userRepository;
        private readonly HashTagParser _parser;

        public SMTPNotifier(IUserRepository userRepository, HashTagParser parser)
        {
            _userRepository = userRepository;
            _parser = parser;
        }

        public void NotifySubsribersAbout(Item item)
        {
            var tag = _parser.Parse(item);
            var users = _userRepository.GetUsersForCountry(tag);

            foreach (var user in users)
            {
                SendMailToUser(user, item.Title);
            }

        }

        private static void SendMailToUser(User user, string content)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("noreply@empty.com")
            };

            mailMessage.To.Add(new MailAddress(user.Email));
            mailMessage.Subject = "Travel Notification";
            mailMessage.Body = content;
            mailMessage.IsBodyHtml = true;

            var mailSmtpClient = new SmtpClient("smtp.gmail.com", 25)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("fconotification@gmail.com", "007fconotification"),
                EnableSsl = true,
            };
            mailSmtpClient.Send(mailMessage);
        }
    }
}
