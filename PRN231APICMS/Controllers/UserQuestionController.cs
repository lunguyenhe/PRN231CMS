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
	}
}
