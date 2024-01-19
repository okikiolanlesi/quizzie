using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Quizzie.Controllers;
using Quizzie.DTOs;
using Quizzie.Models;
using Quizzie.Repositories;
using Quizzie.RequestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzie.Test
{
    [TestClass]
    public class UserControllerTest
    {
        private Mock<IUserRepository> _userRepoMock;
        private Fixture _fixture;
        private UserController _controller;
        private IMapper _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfiles>(); }));


        public UserControllerTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _userRepoMock = new Mock<IUserRepository>();
        }

        [TestMethod]
        public async Task GetUserProfileById_OkResponse()
        {
            Guid userId = Guid.NewGuid();
            var user = _fixture.Create<User>();

            _userRepoMock.Setup(x => x.GetById(userId)).ReturnsAsync(user);
            _controller = new UserController(_userRepoMock.Object, _mapper);

            var result = await _controller.GetUserProfile(userId);
            //  var okResult = result as OkOResult;
            Assert.IsNotNull(result);
            //Assert.AreEqual(200, okResult.StatusCode);

        }
        [TestMethod]
        public async Task UpdateProfile_OkResponse()
        {
            Guid userId = Guid.NewGuid();
            var user = _fixture.Create<User>();
            var updatedUserDto = new UpdateUserDto
            {
                FirstName = "Updated FirstName",
                LastName = "Updated LastName"
            };

          
            _userRepoMock.Setup(x => x.GetById(userId)).ReturnsAsync(user);
            _userRepoMock.Setup(x => x.MarkAsModified(It.IsAny<User>()));
            _userRepoMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(true);

            _controller = new UserController(_userRepoMock.Object, _mapper);
            var result = await _controller.UpdateProfile(userId, updatedUserDto);

           
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
          


        }
    }

}
