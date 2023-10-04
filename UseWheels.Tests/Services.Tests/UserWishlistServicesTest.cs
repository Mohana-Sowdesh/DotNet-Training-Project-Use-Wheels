using System;
using Moq;
using Use_Wheels.Models.DTO;
using Use_Wheels.Repository.IRepository;
using Use_Wheels.Services;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace UseWheels.Tests.Services.Tests
{
    public class UserWishlistServicesTest
    {
        private Mock<ICarRepository> _dbCarMock;
        private readonly UserWishlistServices _userWishlistServices;

        public UserWishlistServicesTest()
        {
            _dbCarMock = new Mock<ICarRepository>();
            _userWishlistServices = new UserWishlistServices(_dbCarMock.Object);
        }

        [Fact]
        public async Task AddToWishlistTest()
        {
            // Arrange
            string vehicle_no = "TU 65 HO 8765";
            string username = "User_1";
            Car carExample = new Car() { Availability = true, Category = null, Category_Id = 1, Created_Date = DateTime.Now, Description = "description", Img_URL = "D://car1.jpg", Likes = 2, Pre_Owner_Count = 2, Price = 900000, Rc_Details = null, RC_No = "577878", Updated_Date = DateTime.Now, Vehicle_No = "TU 65 HO 8765" };
            _dbCarMock.Setup(car => car.GetAsync(It.IsAny<Expression<Func<Car, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(carExample);

            // Act
            await _userWishlistServices.AddToWishlist(vehicle_no, username);

            // Assert
            _dbCarMock.Verify(car => car.UpdateAsync(carExample), Times.Once);
        }

        [Fact]
        public async Task AddToWishlistTest_VehicleNotFound_Exception()
        {
            // Arrange
            string vehicle_no = "TU 65 HO 8765";
            string username = "User_1";
            Car carExample = new Car() { Availability = false, Category = null, Category_Id = 1, Created_Date = DateTime.Now, Description = "description", Img_URL = "D://car1.jpg", Likes = 2, Pre_Owner_Count = 2, Price = 900000, Rc_Details = null, RC_No = "577878", Updated_Date = DateTime.Now, Vehicle_No = "TU 65 HO 8765" };
            _dbCarMock.Setup(car => car.GetAsync(It.IsAny<Expression<Func<Car, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(carExample);

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _userWishlistServices.AddToWishlist(vehicle_no, username));
        }

        [Fact]
        public async Task AddToWishlistTest_VehicleNotFoundNull_Exception()
        {
            // Arrange
            string vehicle_no = "TU 65 HO 8765";
            string username = "User_1";
            Car carExample = null;
            _dbCarMock.Setup(car => car.GetAsync(It.IsAny<Expression<Func<Car, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(carExample);

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _userWishlistServices.AddToWishlist(vehicle_no, username));
        }

        [Fact]
        public async Task DeleteElementFromWishListTest()
        {
            // Arrange
            string vehicle_no = "TU 65 HO 8765";
            string username = "User_1";

            // Act
            _userWishlistServices.DeleteElementFromWishList(vehicle_no, username);
      }

        [Fact]
        public async Task DeleteElementFromWishListTest_VehicleNoNotValid_Exception()
        {
            // Arrange
            string vehicle_no = "TUY 65 HO 8765";
            string username = "User_1";

            // Assert
            Assert.Throws<BadHttpRequestException>(() => _userWishlistServices.DeleteElementFromWishList(vehicle_no, username));
        }

        [Fact]
        public async Task GetWishlistTest()
        {
            // Arrange
            string username = "User_1";

            // Act
            _userWishlistServices.GetWishlist(username);
        }
    }
}

