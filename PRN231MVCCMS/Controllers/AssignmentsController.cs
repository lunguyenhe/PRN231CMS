using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRN231APICMS.Models;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PRN231MVCCMS.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AssignmentsController(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _environment = environment;
            _httpContextAccessor = httpContextAccessor; 
        }

        public async Task<IActionResult> DetailAsync(int id)
        {
            Assignment sub = new Assignment();
            string Url = "http://localhost:5200/api/Assignment/id?id=";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(Url + id))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        sub = JsonConvert.DeserializeObject<Assignment>(data);
                    }
                }
            }
           
            int iduser = _httpContextAccessor.HttpContext.Session.GetInt32("UserId") ?? 0;
            if (iduser == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            SubmittedAssignment subass = new SubmittedAssignment();
            string Url2 = "http://localhost:5200/api/SumittedAssignment/id?id=";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(Url2 + iduser+ "&assid="+id))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        subass = JsonConvert.DeserializeObject<SubmittedAssignment>(data);
                        if (subass != null)
                        {
                            ViewData["subass"] = subass;
                        }
                     
                    }
                }
            }
            return View(sub);
        }

        public async Task<IActionResult> CreateAsync(int id)
        {
            id = _httpContextAccessor.HttpContext.Session.GetInt32("UserId") ?? 0;
            if (id == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            List<Subject> sub = new List<Subject>();
            string Url = "http://localhost:5200/api/Subject/IdFindAll?id=";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(Url + id))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        sub = JsonConvert.DeserializeObject<List<Subject>>(data);
                        ViewData["listsub"] = sub;
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> ListAssignmentAsync()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Assignment a, IFormFile file)
        {
            int iduser = _httpContextAccessor.HttpContext.Session.GetInt32("UserId") ?? 0;
            if (iduser == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            a.Title = Request.Form["title"];
            a.Deadline = DateTime.Parse(Request.Form["date"]);
            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                a.File = (string)fileName;
            }
            else
            {
                a.File = null;

            }
            a.SubjectId = int.Parse(Request.Form["subject"]);
            a.WeekNumber = int.Parse(Request.Form["weeknumber"]);
            a.Description= Request.Form["description"];
            a.UserId = iduser;
            string Url2 = "http://localhost:5200/api/Assignment";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsJsonAsync(Url2, a))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (res.IsSuccessStatusCode)
                        {
                            ViewData["mess"] = "Create Success";
                            return RedirectToAction("Create", "Assignment", new { id = 2 });
                        }
                        else
                        {
                            return RedirectToAction("Error", "Home");
                        }
                    }
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            int iduser = _httpContextAccessor.HttpContext.Session.GetInt32("UserId") ?? 0;
            if (iduser == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                SubmittedAssignment subass = new SubmittedAssignment();
                subass.AssignmentId = int.Parse(Request.Form["assId"]);
                subass.File =(string)fileName;
                string submissionTimeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                subass.SubmissionDate = DateTime.Now;
                subass.UserId = iduser;
                string Url2 = "http://localhost:5200/api/SumittedAssignment";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.PostAsJsonAsync(Url2, subass))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            if (res.IsSuccessStatusCode)
                            {
                                ViewData["mess"] = "Create Success";
                                return RedirectToAction("Detail", "Assignments", new {id= subass.AssignmentId });
                            }
                            else
                            {
                                return RedirectToAction("Error", "Home");
                            }
                        }
                    }
                }
               
            }
            else
            {
                ViewBag.Message = "Vui lòng chọn tệp tin để tải lên.";
                return View("Error");
            }
         }
    }
}