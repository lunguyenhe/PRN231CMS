using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231APICMS.Models;

namespace PRN231APICMS.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserQuestionController : ControllerBase
	{
		[HttpPost]
		public IActionResult Post(UserQuestion user)
		{
			try
			{
				using (PRN231CMS1Context context = new PRN231CMS1Context())
				{
					context.UserQuestions.Add(user);
					context.SaveChanges();
					return Ok();
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}
		[HttpGet("id")]
		public IActionResult Get(int id, int testid)
		{
			try
			{
				using (PRN231CMS1Context context = new PRN231CMS1Context())
				{
					var data = context.UserQuestions.ToList().FirstOrDefault(s => s.UserId == id && s.TestId == testid);
					if (data == null)
					{
						return Ok(false);
					}
					return Ok(true);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}
	}
}
