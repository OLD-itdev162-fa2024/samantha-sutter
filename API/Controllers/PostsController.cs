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

        /// <summary>
        /// Get API Posts
        /// </summary>
        /// <returns>A lists of posts</returns> <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetPosts")]
        public ActionResult<List<Post>> Get()
        {
            return this.context.Posts.ToList();
        }
        
        /// <summary>
        /// GET api/post/[id]
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>A single pot</returns>
        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<Post> GetById(Guid id){
            var post = this.context.Posts.Find(id);
            if(post is null){
                return NotFound();
            }

            return Ok(post);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception> <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(Name = "Create")]
        public ActionResult<Post> Create([FromBody]Post request){
            var post = new Post
            {
                Id = request.Id,
                Title = request.Title,
                Body = request.Body,
                Date = request.Date
            };

            context.Posts.Add(post);
            var success = context.SaveChanges() > 0;

            if(success){
                return Ok(post);
            }

            throw new Exception("Error creating post");
        }

        [HttpPut(Name = "Update")]
        public ActionResult<Post> Update([FromBody]Post request){
            var post = context.Posts.Find(request.Id);
            if(post == null){
                throw new Exception("Could not find post");
            }

            //Update the post properties with request values, if present
            post.Title = request.Title != null ? request.Title : post.Title;
            post.Body = request.Body != null ? request.Body : post.Body;
            post.Date = request.Date != DateTime.MinValue ? request.Date : post.Date;

            var success = context.SaveChanges() > 0;
            if(success){
                return Ok(post);
            }

            throw new Exception("Error updating post");
        }
    }
}