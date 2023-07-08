using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRN231APICMS.Mapper;
using PRN231APICMS.Models;

namespace PRN231MVCCMS.Controllers
{
	public class TestsController : Controller
	{
		public async Task<IActionResult> TestAsync(int? id, string? submit)
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
			if (submit != null)
			{
				string useoption = Request.Form["question"];
				Console.WriteLine(useoption);
				foreach (TestQuestionDTO t in list)
				{
					//string questionhtml = "question-" + t.QuestionId;
					foreach (Option o in listq)
					{



					}

				}
			}
			return View(list);
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

		[HttpPost]
		public async Task<IActionResult> AnswerAsync(int? id, string submit)
		{
			if (id == null)
			{
				// Handle invalid id here
				return RedirectToAction("Index");
			}

			List<TestQuestionDTO> list = await GetTestQuestionsAsync(id);
			List<Option> listq = await GetOptionsAsync();
			List<UserQuestionDTO> listu = new List<UserQuestionDTO>();
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
								u.UserId = 5;
								u.TestId = id;
								u.QuestionId = t.QuestionId;
								u.IsCorrect = o.IsCorrect;
								u.OptionName = o.Content;
								u.QuestionCount = list.Count();
								listu.Add(u);
							}
						}
					}
				}
			}

			return View(listu);
		}
	}
}
