using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231APICMS.Models;

namespace PRN231APICMS.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestsController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			try
			{
				using (PRN231CMS1Context context = new PRN231CMS1Context())
				{
					var data = context.Tests.ToList();
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
        [HttpGet("test")]
        public IActionResult Get(int id,int week)
        {
            try
            {
                using (PRN231CMS1Context context = new PRN231CMS1Context())
                {
                    var data = context.Tests.ToList().FirstOrDefault(s => s.SubjectId == id&&s.WeekNumber==week);
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
