using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace EasyLife.Application.Services
{
	public class EmailSender : IEmailSender
	{
		public async Task SendEmailAsync(string email, string subject, string message)
		{
			//From Address    
			string FromAddress = "nikolay.danev16@gmail.com";
			string FromAdressTitle = "EasyLife";
			//To Address    
			string ToAddress = email;
			string Subject = subject;
			string BodyContent = message;

			//Smtp Server    
			string SmtpServer = "smtp.office365.com";
			//Smtp Port Number    
			int SmtpPortNumber = 587;

			var mimeMessage = new MimeMessage();
			mimeMessage.From.Add(new MailboxAddress
			(FromAdressTitle,
				FromAddress
			));
			mimeMessage.To.Add(new MailboxAddress
			(
				ToAddress
			));
			mimeMessage.Subject = Subject; //Subject  
			mimeMessage.Body = new TextPart("plain")
			{
				Text = BodyContent
			};

			using (var client = new SmtpClient())
			{
				client.Connect(SmtpServer, SmtpPortNumber, false);
				client.Authenticate(
					"nikolay.danev16@gmail.com",
					"zvojihzzeglgohbw"
				);
				await client.SendAsync(mimeMessage);
				await client.DisconnectAsync(true);
			}
		}
	}
}