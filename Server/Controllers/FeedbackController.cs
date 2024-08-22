using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class FeedbackController : ControllerBase
    {
        [HttpPost]
        public IActionResult PostFeedback([FromBody] Feedback feedback)
        {
            using var db = new Delivery();

            db.Feedbacks.Add(feedback);
            db.SaveChanges();

            return Ok("Feedback Added Successfully");
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetFeedbacks()
        {
            using var db = new Delivery();

            List<Feedback> feedbacks = db.Feedbacks.ToList();

            return Ok(feedbacks);
        }
    }
}