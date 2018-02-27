using System;
using System.Collections.Generic;
using Hub.Domain.Handlers.Post;
using HUB.Domain.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hub.Web.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly Lazy<GetPosts> _getPostsList;
        public PostsController(Lazy<GetPosts> getPostsList)
        {
            if (getPostsList == null) throw new ArgumentNullException(nameof(getPostsList));
            _getPostsList = getPostsList;
        }
        // GET: api/<controller>
        [HttpGet]
        [Route("get-user-posts")]
        public List<Post> Get()
        {
            return _getPostsList.Value.Execute();
        }
    }
}
