using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EasyLife.Application.Services
{
	public class EmailSender
	{
		//public async Task SendEmailAsync(string email, string subject, string message)
		//{
		//	using (MailMessage mailMessage = new MailMessage())
		//	{
		//		mailMessage.From = new MailAddress("nikolay.danev16@gmail.com");
		//		mailMessage.Subject = subject;
		//		mailMessage.Body = message;
		//		mailMessage.IsBodyHtml = true;
		//		mailMessage.To.Add(new MailAddress(email));
		//		SmtpClient smtp = new SmtpClient();
		//		smtp.Host = "smtp.gmail.com";
		//		smtp.EnableSsl = true;
		//		System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
		//		NetworkCred.UserName = "nikolay.danev16@gmail.com";
		//		NetworkCred.Password = "jijituhlatop14";
		//		smtp.UseDefaultCredentials = false;
		//		smtp.Credentials = NetworkCred;
		//		smtp.Port = 587;
		//		smtp.Send(mailMessage);
		//	}
		//	await Task.CompletedTask;
		//}
	}
}