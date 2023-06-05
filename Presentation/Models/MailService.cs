using DataAccess.Concrete;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.Models
{
    public class MailService
    {
        Context db = new Context();

        public void SendEmailForNewTraining(string trainingTitle, int trainingId)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Gönderen", "akin.unal@outlook.com"));
            message.Subject = "Yeni Eğitim Yazısı Yayınlandı!";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<p>Okumak İçin Tıklayın:</p> <a href='https://akinunal.com/Training/Details/{trainingId}'>{trainingTitle}</a>";

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);

                client.Authenticate("akin.unal@outlook.com", "Yamaha4859");

                var emails = GetEmailsFromNewsletter();

                foreach (var email in emails)
                {
                    message.To.Clear();
                    message.To.Add(new MailboxAddress("", email));

                    client.Send(message);
                }

                client.Disconnect(true);
            }
        }

        public void SendEmailForNewPost(string postTitle, int postId)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Gönderen", "akin.unal@outlook.com"));
            message.Subject = "Yeni Blog Yazısı Yayınlandı!";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<p>Okumak İçin Tıklayın:</p> <a href='https://akinunal.com/Post/Details/{postId}'>{postTitle}</a>";

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);

                client.Authenticate("akin.unal@outlook.com", "Yamaha4859");

                var emails = GetEmailsFromNewsletter();

                foreach (var email in emails)
                {
                    message.To.Clear();
                    message.To.Add(new MailboxAddress("", email));

                    client.Send(message);
                }

                client.Disconnect(true);
            }
        }

        public List<string> GetEmailsFromNewsletter()
        {
            var emails = db.Newsletters.Select(x => x.Email).ToList();
            return emails;
        }

    }
}
