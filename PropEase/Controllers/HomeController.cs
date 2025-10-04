using Microsoft.AspNetCore.Mvc;
using PropEase.Models;
using PropEase.Repository;
using System.Diagnostics;

namespace PropEase.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactMessageRepository ContactMessageRepo;

        public HomeController(IContactMessageRepository cmr)
        {
            this.ContactMessageRepo = cmr;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(string name,string email,string subject,string message)
        {
            var detail = new ContactDetail
            {
                Name = name,
                Email=email,
                Subject=subject,
                Message=message
            };
            await this.ContactMessageRepo.SaveContactMessage(detail.Name,detail.Email,detail.Subject,detail.Message);
            return Json(new { success = true, responseText = "Data Saved" });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}