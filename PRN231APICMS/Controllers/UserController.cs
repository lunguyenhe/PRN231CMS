using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231APICMS.Models;

namespace PRN231APICMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUser(string email, string pass)
        {
            try
            {
                using (PRN231CMS1Context context = new PRN231CMS1Context())
                {
                    var data = context.Users.FirstOrDefault(e => e.Email == email && e.Password == pass);
                    if (data == null)
                    {
                        return NotFound();
                    }
                    return Ok(data);
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
