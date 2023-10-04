using System;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using Use_Wheels;
using Use_Wheels.Models.DTO;
using Use_Wheels.Repository.IRepository;
using Use_Wheels.Services;

namespace UseWheels.Tests.Services.Tests
{
    public class CategoryServicesTest
    { 
        private Mock<ICategoryRepository> _categoryRepoMock;
        private readonly CategoriesServices _categoriesServices;

        public CategoryServicesTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingConfig());
            });

            _categoryRepoMock = new Mock<ICategoryRepository>();
            _categoriesServices = new CategoriesServices(_categoryRepoMock.Object, mappingConfig.CreateMapper());
        }

        [Fact]
        public async Task GetAllCategories()
        {
            // Arrange
            IEnumerable<Category> categoryList = new List<Category>();
            List<Category> listData = new List<Category>
            {
                new Category { Category_Id = 1, Category_Names = "Minivan"},
                new Category { Category_Id = 2, Category_Names = "Sedan" },
            };
            categoryList = listData;
            _categoryRepoMock.Setup(category => category.GetAllAsync(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(listData);

            // Act
            var categoriesList = await _categoriesServices.GetAllCategories();

            // Assert
            Assert.Equal(categoryList, categoriesList);
        }

        [Fact]
        public async Task DeleteCategoryTest()
        {
            // Arrange
            Category categoryExample = new() { Category_Id = 1, Category_Names = "Sedan" };
            _categoryRepoMock.Setup(category => category.GetAsync(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(categoryExample);

            // Act
            await _categoriesServices.DeleteCategory(1);

            // Assert
            _categoryRepoMock.Verify(category => category.RemoveAsync(categoryExample), Times.Once);
        }

        [Fact]
        public async Task DeleteCategory_Invalid_ID_Exception_Test()
        {
            // Arrange
            _categoryRepoMock.Setup(category => category.GetAsync(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<bool>(), It.IsAny<string>()));

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _categoriesServices.DeleteCategory(-1));
        }

        [Fact]
        public async Task DeleteCategory_NotFound_Exception_Test()
        {
            // Arrange
            _categoryRepoMock.Setup(category => category.GetAsync(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<bool>(), It.IsAny<string>()));

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _categoriesServices.DeleteCategory(21));
        }

        [Fact]
        public async Task CreateCategoryTest()
        {
            // Arrange
            CategoryDTO categoryDTO = new() { Category_Names = "Minivan" };

            // Act
            Category categoryResult = await _categoriesServices.CreateCategory(categoryDTO);

            // Assert
            _categoryRepoMock.Verify(category => category.CreateAsync(categoryResult), Times.Once);
        }

        [Fact]
        public async Task CreateCategory_AlreadyExists_Exception_Test()
        {
            // Arrange
            CategoryDTO categoryDTO = new() { Category_Names = "Minivan" };
            Category categoryExample = new() { Category_Id = 1, Category_Names = "Minivan" };
            _categoryRepoMock.Setup(category => category.GetAsync(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(categoryExample);

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _categoriesServices.CreateCategory(categoryDTO));
        }
    }
}

