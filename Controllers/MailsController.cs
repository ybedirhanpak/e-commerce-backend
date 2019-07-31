using System;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using e_commerce_api.Models;
using e_commerce_api.Services;


namespace e_commerce_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MailsController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly IUserService _userService;


        public MailsController(IMailService mailService, IUserService userService)
        {
            _mailService = mailService;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult SendEmail([FromBody] Email email)
        {

            _mailService.sendEmail(email);
            return NoContent();

        }

        [HttpPost]
        public IActionResult ResetPasswordMail([FromBody] Email email)
        {
            var mailInDb = _userService.GetByEmail(email.senderEmail);
            if (mailInDb == null)
            {
                return NotFound();
            }
            else
            {
                _mailService.resetPasswordMail(email,mailInDb);
                return NoContent();
            }
            
        }


    }
}
