using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using S8PLUS.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace S8PLUS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICaptchaValidator _captchaValidator;
        public HomeController(ILogger<HomeController> logger, ICaptchaValidator captchaValidator)
        {
            _captchaValidator = captchaValidator;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string s)
        {
            if (s != null)
                return Redirect($"https://se-pps.ru/search/?pcode={s}");
            return View();
        }
        //
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Abouts()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(string nameuser, string mailto, string subjest, string message, string captcha)
        {
            MailTextClass mailText = new MailTextClass() { NameUser = nameuser, MailTo = mailto, Subjest = subjest, Message = message };
            if (!await _captchaValidator.IsCaptchaPassedAsync(captcha))
            {
                ModelState.AddModelError("captcha", "Captcha validation failed");
            }
            if (ModelState.IsValid)
            {
                ViewData["Message"] = "Сообщение отправленно.";
            }
            return View(mailText);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
