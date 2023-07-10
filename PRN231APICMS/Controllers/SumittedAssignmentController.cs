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
	public class SumittedAssignmentController : ControllerBase
	{
        [HttpPost]
        public IActionResult Post(SubmittedAssignment a)
        {
            try
            {
                using (PRN231CMS1Context context = new PRN231CMS1Context())
                {
                    context.SubmittedAssignments.Add(a);
                    context.SaveChanges();
                    return Ok(a);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("id")]
        public IActionResult Get(int id,int assid)
        {
            try
            {
                using (PRN231CMS1Context context = new PRN231CMS1Context())
                {
                    var data = context.SubmittedAssignments.ToList().FirstOrDefault(s => s.UserId == id&&s.AssignmentId==assid);
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
        [HttpGet("Assignment")]
        public IActionResult GetAssignment(int id)
        {
            try
            {
                using (PRN231CMS1Context context = new PRN231CMS1Context())
                {
                    var data = context.SubmittedAssignments.Include(s=>s.User)
                       .ToList().FindAll(s => s.AssignmentId == id);
                    if (data == null)
                    {
                        return NotFound();
                    }
                    var config = new MapperConfiguration(config => config.AddProfile(new MapperProfile()));
                    var mapper = config.CreateMapper();
                    List<SubmitAssignmentDTO> result = data.Select(r => mapper.Map<SubmittedAssignment, SubmitAssignmentDTO>(r)).ToList();
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
