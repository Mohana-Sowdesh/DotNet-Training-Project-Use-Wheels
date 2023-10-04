namespace UseWheels.Tests;

using Moq;
using Use_Wheels.Models.DTO;
using Use_Wheels.Services.IServices;

public class CategoriesControllerTests
{
    private CategoriesController _categoriesControllerMock;
    private Mock<ICategoriesServices> _categoriesServicesMock;

    public CategoriesControllerTests()
    {
        _categoriesServicesMock = new Mock<ICategoriesServices>();
    }

    [Fact]
    public async Task GetAllCategoriesTest()
    {
        // Arrange
        _categoriesServicesMock.Setup(category => category.GetAllCategories());
        _categoriesControllerMock = new CategoriesController(_categoriesServicesMock.Object);

        // Act
        var result = await _categoriesControllerMock.GetAllCategories();

        // Assert
        _categoriesServicesMock.Verify(category => category.GetAllCategories(), Times.Once);
    }

    [Fact]
    public async Task DeleteCategoryTest()
    {
        // Arrange
        int id = 7;
        _categoriesServicesMock.Setup(category => category.DeleteCategory(id));
        _categoriesControllerMock = new CategoriesController(_categoriesServicesMock.Object);

        // Act
        var result = await _categoriesControllerMock.DeleteCategory(id);

        // Assert
        _categoriesServicesMock.Verify(category => category.DeleteCategory(id), Times.Once);
    }

    [Fact]
    public async Task CreateCategoryTest()
    {
        // Arrange
        CategoryDTO categoryDTO = new CategoryDTO() { Category_Names = "Luxury Sedan" };
        Category category = new Category() {Category_Id = 1, Category_Names = "Luxury Sedan" };
        _categoriesServicesMock.Setup(category => category.CreateCategory(categoryDTO)).ReturnsAsync(category);
        _categoriesControllerMock = new CategoriesController(_categoriesServicesMock.Object);

        // Act
        var result = await _categoriesControllerMock.CreateCategory(categoryDTO);

        // Assert
        _categoriesServicesMock.Verify(category => category.CreateCategory(categoryDTO), Times.Once);
    }
}
