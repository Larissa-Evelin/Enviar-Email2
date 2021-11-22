using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorpageEmail.Models;
using System.Net.Mail;

namespace RazorpageEmail.Pages
{
    public class ContactModel : PageModel
    {

        public string Message { get; set; }
        [BindProperty]
        public Email mails { get; set; }


        public void OnGet()
        {
            Message = "Your contact page";
        }
        public async Task OnPost()
        {
            using (var smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;   
                smtp.PickupDirectoryLocation = @"C:\Mymail";
                var msg = new MailMessage
                {
                    Body = mails.Body,
                    Subject = mails.Subject,
                    From = new MailAddress(mails.From)
                };
                msg.To.Add(mails.To);  
                await smtp.SendMailAsync(msg);
            }
        }
    }
}
