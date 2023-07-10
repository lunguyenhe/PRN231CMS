using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRN231APICMS.Models;
using PRN231MVCCMS.Models;
using System.Diagnostics;

namespace PRN231MVCCMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public async Task<IActionResult> LoginAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                return View();
            }
            using (HttpClient client = new HttpClient())
            {
                string link = "http://localhost:5200/api/User";
                using (HttpResponseMessage res = await client.GetAsync(link + "?email=" + email + "&pass=" + password))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();

                        User user = JsonConvert.DeserializeObject<User>(data);
                        int role = (int)user.RoleId;
                        if (user.RoleId == 2)
                        {

                            HttpContext.Session.SetInt32("role", role);
                            HttpContext.Session.SetString("Email", email);
                            HttpContext.Session.SetInt32("UserId", user.UserId);
                            return RedirectToAction("Index", "Subject");
                        }
                        if (user.RoleId == 3)
                        {
                            HttpContext.Session.SetInt32("role", role);
                            HttpContext.Session.SetString("Email", email);
                            HttpContext.Session.SetInt32("UserId", user.UserId);
                            return RedirectToAction("Index", "Subject");
                        }
                        else
                        {
                            ViewData["message"] = "Do not have that account!";
                        }



                    }
                }
                return View();
            }
        }
        public async Task<IActionResult> LogoutAsync(string email, string password)
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");

        }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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