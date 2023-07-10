using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231APICMS.Models;

namespace PRN231APICMS.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubjectController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			try
			{
				using (PRN231CMS1Context context = new PRN231CMS1Context())
				{
					var data = context.Subjects.ToList();
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

        [HttpGet("Id")]
        public IActionResult Get(int id)
        {
            try
            {
                using (PRN231CMS1Context context = new PRN231CMS1Context())
                {
                    var data = context.Subjects.ToList().FirstOrDefault(s=>s.SubjectId==id);
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
        [HttpGet("IdFindAll")]
        public IActionResult GetFindAll(int id)
        {
            try
            {
                using (PRN231CMS1Context context = new PRN231CMS1Context())
                {
                    var data = context.Subjects.ToList().FindAll(s => s.UserId == id);
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
