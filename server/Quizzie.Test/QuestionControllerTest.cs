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
    public class QuestionControllerTest
    {
        private CreateQuestionDto _questionDto;
        private IQuizRepository _quizRepository;
        private IOptionRepository optionRepository;
        private Question newQuestion;
        private Mock<IQuestionRepository> _questionRepoMock;
        private Fixture _fixture;
        private QuestionController _controller;
        private IMapper _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfiles>(); }));

        public QuestionControllerTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _questionRepoMock = new Mock<IQuestionRepository>();
        }

        [TestMethod]
        public async Task Add_Question_OkResponse()
        {
            Guid quizId = Guid.NewGuid();
            var questionDto = _fixture.Build<CreateQuestionDto>().Create();
            var question = _fixture.Create<Question>();

            _questionRepoMock.Setup(x => x.GetById(quizId)).ReturnsAsync((question));
            _questionRepoMock.Setup(x => x.Add(newQuestion));
             _questionRepoMock.Setup(x => x.Add(It.IsAny<Question>()));
            _questionRepoMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(true);

            _controller = new QuestionController(_questionRepoMock.Object, _quizRepository, _mapper, optionRepository);
            var result = await _controller.AddQuestion(_questionDto, quizId);
            var OkResult = result as ObjectResult;

            Assert.IsNotNull(result);
             Assert.AreEqual(201, OkResult.StatusCode);
        }


    }
}
