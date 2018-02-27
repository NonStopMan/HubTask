using HUB.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using HUB.Domain.Model;
using System.Linq;
using Microsoft.AspNetCore.Http;
using HUB.CrossCutting.Identity;

namespace Hub.Domain.Handlers.Post
{
    public class GetPosts
    {
        private IReadOnlyRepository<HUB.Domain.Model.Post> _getUserPosts;
        public GetPosts(IReadOnlyRepository<HUB.Domain.Model.Post> getUserPosts)
        {
            if (getUserPosts == null) throw new ArgumentNullException(nameof(getUserPosts));
            _getUserPosts = getUserPosts;
        }

        public List<HUB.Domain.Model.Post> Execute()
        {
            return _getUserPosts.GetAll("Author").ToList();
        }
    }
}
