using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRN231APICMS.Models;

namespace PRN231MVCCMS.Controllers
{
	public class SubjectController : Controller
	{
		public async Task<IActionResult> IndexAsync()
		{
			List<Subject> sub = new List<Subject>();
			string Url = "http://localhost:5200/api/Subject";
			using (HttpClient client = new HttpClient())
			{

				using (HttpResponseMessage res = await client.GetAsync(Url))
				{
					using (HttpContent content = res.Content)
					{
						string data = await content.ReadAsStringAsync();
						//   Console.WriteLine($"{data}");
						sub = JsonConvert.DeserializeObject<List<Subject>>(data);

					}


				}

			}
			return View(sub);
		}

		public async Task<IActionResult> DetailAsync(int id)
		{
            Subject sub = new Subject();
            string Url = "http://localhost:5200/api/Subject/Id?id=";
            using (HttpClient client = new HttpClient())
            {

                using (HttpResponseMessage res = await client.GetAsync(Url+id))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        //   Console.WriteLine($"{data}");
                        sub = JsonConvert.DeserializeObject<Subject>(data);

                    }


                }

            }
            return View(sub);
		}
	}
}
