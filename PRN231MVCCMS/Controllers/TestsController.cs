using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRN231APICMS.Mapper;
using PRN231APICMS.Models;

namespace PRN231MVCCMS.Controllers
{
	public class TestsController : Controller
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public TestsController(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<IActionResult> TestAsync(int? id, string? submit)
		{
			int iduser = _httpContextAccessor.HttpContext.Session.GetInt32("UserId") ?? 0;
			if (iduser == 0)
			{
				return RedirectToAction("Login", "Home");
			}
			bool checktest = await CheckUserTestAsync(iduser, (int)id);

			if (checktest == false)
			{
				List<TestQuestionDTO> list = new List<TestQuestionDTO>();
				List<Option> listq = new List<Option>();
				string Url = "http://localhost:5200/api/TestQuestion/Id?id=";
				using (HttpClient client = new HttpClient())
				{

					using (HttpResponseMessage res = await client.GetAsync(Url + id))
					{
						using (HttpContent content = res.Content)
						{

							string data = await content.ReadAsStringAsync();
							//   Console.WriteLine($"{data}");
							list = JsonConvert.DeserializeObject<List<TestQuestionDTO>>(data);

						}


					}

				}

				string Url2 = "http://localhost:5200/api/Question/options";
				using (HttpClient client = new HttpClient())
				{

					using (HttpResponseMessage res = await client.GetAsync(Url2))
					{
						using (HttpContent content = res.Content)
						{

							string data = await content.ReadAsStringAsync();
							//   Console.WriteLine($"{data}");
							listq = JsonConvert.DeserializeObject<List<Option>>(data);

							ViewData["listq"] = listq;
						}


					}

				}

				return View(list);
			}
			else
			{
				return RedirectToAction("Index", "Subject");
			}
			
		}


		private async Task<List<TestQuestionDTO>> GetTestQuestionsAsync(int? id)
		{
			List<TestQuestionDTO> list = new List<TestQuestionDTO>();
			string url = "http://localhost:5200/api/TestQuestion/Id?id=" + id;

			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.GetAsync(url))
				{
					using (HttpContent content = res.Content)
					{
						string data = await content.ReadAsStringAsync();
						list = JsonConvert.DeserializeObject<List<TestQuestionDTO>>(data);
					}
				}
			}

			return list;
		}

		private async Task<List<Option>> GetOptionsAsync()
		{
			List<Option> list = new List<Option>();
			string url = "http://localhost:5200/api/Question/options";

			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.GetAsync(url))
				{
					using (HttpContent content = res.Content)
					{
						string data = await content.ReadAsStringAsync();
						list = JsonConvert.DeserializeObject<List<Option>>(data);
					}
				}
			}

			return list;
		}
		private async Task<bool> AddOptionUserAsync(UserQuestion u)
		{
			
			string url = "http://localhost:5200/api/Question/options";

			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.PostAsJsonAsync(url,u))
				{
					using (HttpContent content = res.Content)
					{
						if (res.IsSuccessStatusCode)
						{
							return true;
						}
					}
				}
			}

			return false;
		}
		private async Task<bool> AddUserQuestionAsync(UserQuestion u)
		{

			string url = "http://localhost:5200/api/UserQuestion";

			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.PostAsJsonAsync(url, u))
				{
					using (HttpContent content = res.Content)
					{
						if (res.IsSuccessStatusCode)
						{

							return true;
						}
					}
				}
			}

			return false;
		}
		private async Task<bool> CheckUserTestAsync(int userId,int testid)
		{
			bool check = false;
			string url = "http://localhost:5200/api/UserQuestion/";

			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.GetAsync(url+ "id?id="+userId+ "&testid="+testid))
				{
					using (HttpContent content = res.Content)
					{
						if (res.IsSuccessStatusCode)
						{
						
							string data = await content.ReadAsStringAsync();
							check = bool.Parse(data);
							return check;
							
						}
					}
				}
			}

			return false;
		}
		[HttpPost]
		public async Task<IActionResult> AnswerAsync(int? id, string submit)
		{
			int iduser = _httpContextAccessor.HttpContext.Session.GetInt32("UserId") ?? 0;
			if (iduser == 0)
			{
				return RedirectToAction("Login", "Home");
			}
			if (id == null)
			{
				// Handle invalid id here
				return RedirectToAction("Index");
			}

			List<TestQuestionDTO> list = await GetTestQuestionsAsync(id);
			List<Option> listq = await GetOptionsAsync();
			List<UserQuestionDTO> listu = new List<UserQuestionDTO>();
			bool check = false;
			if (!string.IsNullOrEmpty(submit))
			{
				foreach (TestQuestionDTO t in list)
				{
					string key = "question-" + t.QuestionId;


					if (Request.Form.TryGetValue(key, out var value))
					{
						int useoption = int.Parse(value);

						
						foreach (Option o in listq)
						{
							if(o.OptionId == useoption)
							{
								UserQuestionDTO u = new UserQuestionDTO();
								u.OptionId=useoption;
								u.UserId = iduser;
								u.TestId = id;
								u.QuestionId = t.QuestionId;
								u.QuestionName = t.Content;
								u.IsCorrect = o.IsCorrect;
								u.OptionName = o.Content;
								u.QuestionCount = list.Count();

								UserQuestion u2= new UserQuestion();
								u2.OptionId = useoption;
								u2.UserId = iduser;
								u2.TestId = id;
								u2.QuestionId= t.QuestionId;
								 check = await AddUserQuestionAsync(u2);
								listu.Add(u);
							}
						}
					}
				}
			}
			if (check == true)
			{
				return View(listu);
			}
			else
			{
				return RedirectToAction("Error", "Home");
			}
		}
	}
}
