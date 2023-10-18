using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly DataContext context;

        public PostController(DataContext context)
        {
            this.context = context;
        }

        //GET api/posts
        [HttpGet(Name = "GetPosts")]
        public ActionResult<List<Post>> Get()
        {
            return this.context.Posts.ToList();
        }
    }
}