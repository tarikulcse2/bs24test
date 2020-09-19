using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSExam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BSExam.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly DBContext _db;

        public PostController(DBContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get(int page = 1, int pageSize = 5, string filterKey = "")
        {
            int skip = (page - 1) * pageSize;
            List<Post> posts = _db.Posts.Include(t => t.Comments)
                                .Where(r => r.Text == (filterKey != "" ? filterKey : r.Text)).Skip(skip).Take(pageSize).ToList();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Post post = _db.Posts.SingleOrDefault(t => t.Id == id);
            return Ok(post);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Post post)
        {
            _db.Posts.Add(post);
            _db.SaveChanges();
            return Ok(post);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Post post)
        {
            _db.Posts.Update(post);
            _db.SaveChanges();
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var post = _db.Posts.SingleOrDefault(r => r.Id == id);
            _db.Posts.Remove(post);
            _db.SaveChanges();
            return Ok();
        }
    }
}
