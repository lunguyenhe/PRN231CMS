using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN231APICMS.Mapper;
using PRN231APICMS.Models;

namespace PRN231APICMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestQuestionController : ControllerBase
    {
        [HttpGet("Id")]
        public IActionResult Get(int id)
        {
            try
            {
                using (PRN231CMS1Context context = new PRN231CMS1Context())
                {
                    var data = context.TestQuestions.Include(s=>s.Question).ToList().FindAll(s => s.TestId == id);
                    if (data == null)
                    {
                        return NotFound();
                    }
                    var config = new MapperConfiguration(config => config.AddProfile(new MapperProfile()));
                    var mapper = config.CreateMapper();
                    List<TestQuestionDTO> result = data.Select(r => mapper.Map<TestQuestion, TestQuestionDTO>(r)).ToList();
                    return Ok(result);
                  
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
 
    }
}
