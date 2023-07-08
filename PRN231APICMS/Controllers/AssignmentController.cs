using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231APICMS.Models;

namespace PRN231APICMS.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AssignmentController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			try
			{
				using (PRN231CMS1Context context = new PRN231CMS1Context())
				{
					var data = context.Assignments.ToList();
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
        [HttpGet("week")]
        public IActionResult Get(int id,int week)
        {
            try
            {
                using (PRN231CMS1Context context = new PRN231CMS1Context())
                {
                    var data = context.Assignments.ToList().FirstOrDefault(s=>s.WeekNumber==week&&s.SubjectId==id);
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
