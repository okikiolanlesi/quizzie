
using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using Quizzie.Controllers;
using Quizzie.DTOs;
using Quizzie.Models;
using Quizzie.Repositories;
using Quizzie.RequestHelpers;

namespace Quizzie.Test
{
    [TestClass]
    public class CategoryControllerTest
    {
        private Mock<ICategoryRepository> _categoryRepoMock;
        private Fixture _fixture;
        private CategoryController _controller;
        private IMapper _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfiles>(); }));

        public CategoryControllerTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _categoryRepoMock = new Mock<ICategoryRepository>();
        }
        
        
            

            [TestMethod]
        public async Task Get_AllCategory_ReturnOK()
        {
            var CategoryList = _fixture.CreateMany<Category>().ToList();
            _categoryRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(CategoryList);
            _controller = new CategoryController(_mapper, _categoryRepoMock.Object);

            var result = await _controller.Get_All();
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);

        }

        [TestMethod]

        public async Task Get_CategoryById_ReturnsOK()
        {
            
            Guid categoryId = Guid.NewGuid(); 
            var category = _fixture.Create<CategoryDto>(); 

            _categoryRepoMock.Setup(repository => repository.GetById(categoryId)).ReturnsAsync(category);

            _controller = new CategoryController(_mapper, _categoryRepoMock.Object);

           
            var result = await _controller.GetBy_Id(categoryId);
            
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

           
        }
        [TestMethod]
        public async Task Post_CreateCategory_CreatedResponse()
        {
            string title = "Name";
            var categoryDto = _fixture.Build<CreateOrUpdateCategoryDto>().With(dto => dto.Title, title).Create();
            var category = _fixture.Create<Category>();

            _categoryRepoMock.Setup(repo => repo.GetByTitle(title)).ReturnsAsync((Category)null);
            _categoryRepoMock.Setup(x => x.Add(category));
            _categoryRepoMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(true);

            _controller = new CategoryController(_mapper, _categoryRepoMock.Object);
            var result = await _controller.CreateCategory(categoryDto);
            var okResult = result as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(201, okResult.StatusCode);
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));


        }
        [TestMethod]
        public async Task Delete_Category_ReturnOk()
        {

            Guid delete_Id = Guid.NewGuid();
            var category = _fixture.Create<Category>();
            _categoryRepoMock.Setup(repository => repository.DeleteById(delete_Id)).ReturnsAsync(category);

            _categoryRepoMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(true);
            
            _controller = new CategoryController(_mapper, _categoryRepoMock.Object);

            var result = await _controller.DeleteBy_Id(delete_Id);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [TestMethod]
        public async  Task Update_Category_ReturnOk()
        {
            Guid update_Id = Guid.NewGuid();
            var category = _fixture.Create<CategoryDto>();
            var _updatecategoryDto = new CreateOrUpdateCategoryDto
            {
                Title = "Updated Title",
                Description = "Updated Description"
            };

            _categoryRepoMock.Setup(x=> x.GetById(update_Id)).ReturnsAsync(category);
            _categoryRepoMock.Setup(x => x.MarkAsModified(It.IsAny<Category>()));
            _categoryRepoMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(true);

            _controller = new CategoryController(_mapper, _categoryRepoMock.Object);


            var result = await _controller.UpdateCategory(update_Id, _updatecategoryDto );


            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }
      

   

    }


}

