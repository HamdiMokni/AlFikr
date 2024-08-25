using AlFikr.ThesisService.Business.Extensions;
using AlFikr.ThesisService.Data.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace AlFikr.ThesisService.Business;

public class EmailService
{
	public void SendEmail(User userEntity)
	{
		var message = new MimeMessage();
		message.From.Add(new MailboxAddress("Al Fikr", "info@al-fikr.com"));
		message.To.Add(new MailboxAddress($"Mme/Mr. ", userEntity.Email));
		message.Subject = "Confirmation d'identité";

		message.Body = new TextPart("plain")
		{
			Text = @$"Bonjour,

Votre compte a été créé avec succès.

Mot de passed : {PasswordExtensions.Decrypt(userEntity.Password, userEntity.Code)}

Bien a vous,

al-fikr.com"
		};

		using (var client = new SmtpClient())
		{
			client.Connect("mail.al-fikr.com", 25, false);

			// Note: only needed if the SMTP server requires authentication
			client.Authenticate("info@al-fikr.com", "159753JUSa");

			client.Send(message);
			client.Disconnect(true);
		}
	}
}
