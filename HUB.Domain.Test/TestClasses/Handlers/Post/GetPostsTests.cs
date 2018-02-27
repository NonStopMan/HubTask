using Hub.Domain.Handlers.Post;
using Hub.Domain.Test.Helper;
using HUB.CrossCutting.Identity;
using HUB.Data;
using HUB.Domain.Repositories;
using HUB.Domain.Tests.Mocks;
using Moq;
using System.Linq;
using Xunit;

namespace HUB.Domain.Tests.TestClasses.Handlers.Post
{
    public class GetPostsTests
    {
        private readonly IReadOnlyRepository<Model.Post> _repository;
        private readonly IQueryableUnitOfWork _unitOfWork;


        public GetPostsTests()
        {
            _unitOfWork = new FakeUnitOfWork();
            _repository = new FakeReadOnlyRepository<Model.Post>(_unitOfWork);
        }

       

        [Fact(DisplayName = "ShouldReturnActionsWhenExistingInDatabase")]
        public void ShouldReturnActionsWhenExistingInDatabase()
        {
            //Arrange
            var handler = new GetPosts(_repository);
            var currentUserName = IdentityProviderFactory.CreateCLPIdentity().GetCurrentIdentity();
            var name = currentUserName.Contains("\\") ? currentUserName.Split('\\')[1] : currentUserName;

            var newposts = PostTestHelper.GetPosts();

            foreach (var post in newposts)
            {
                _unitOfWork.AddOrUpdate(post);
            }

            var posts = handler.Execute();

            //Assert
            Assert.NotNull(posts);
            Assert.NotEmpty(posts);
            Assert.Equal(3, posts.Count());

        }

        [Fact(DisplayName = "WhenNoActionsShouldReturnEmptyList")]
        public void WhenNoActionsShouldReturnEmptyList()
        {
            //Arrange
            var handler = new GetPosts(_repository);

            //Act
            var posts =  handler.Execute();

            //Assert
            Assert.NotNull(posts);
            Assert.Empty(posts);
        }
    }
}
