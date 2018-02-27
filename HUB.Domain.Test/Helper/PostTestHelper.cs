using HUB.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hub.Domain.Test.Helper
{
    public class PostTestHelper
    {
        internal static List<Post> GetPosts()
        {
            return new List<Post>() {
                new Post(){
                    AuthorId = Guid.NewGuid(),
                    Content = "Content",
                    PostId = Guid.NewGuid(),
                    Title = "Title"
                },
                new Post(){
                    AuthorId = Guid.NewGuid(),
                    Content = "Content",
                    PostId = Guid.NewGuid(),
                    Title = "Title"
                },
                new Post(){
                    AuthorId = Guid.NewGuid(),
                    Content = "Content",
                    PostId = Guid.NewGuid(),
                    Title = "Title"
                },
                new Post(){
                    AuthorId = Guid.NewGuid(),
                    Content = "Content",
                    PostId = Guid.NewGuid(),
                    Title = "Title"
                },
                new Post(){
                    AuthorId = Guid.NewGuid(),
                    Content = "Content",
                    PostId = Guid.NewGuid(),
                    Title = "Title"
                },
                new Post(){
                    AuthorId = Guid.NewGuid(),
                    Content = "Content",
                    PostId = Guid.NewGuid(),
                    Title = "Title"
                }
            };
        }
    }
}
