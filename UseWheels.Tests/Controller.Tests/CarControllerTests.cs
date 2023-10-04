using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Use_Wheels.Models.DTO;
using Use_Wheels.Services.IServices;

namespace UseWheels.Tests
{
    public class CarControllerTests
    {
        private CarController _carControllerMock;
        private Mock<ICarServices> _carServicesMock;
        private Mock<IMemoryCache> _cacheMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public CarControllerTests()
        {
            _carServicesMock = new Mock<ICarServices>();
            _cacheMock = new Mock<IMemoryCache>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var claims = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Name, "Admin_1")
            }));

            _httpContextAccessorMock.Setup(req => req.HttpContext.User).Returns(claims);
        }

        [Fact]
        public async Task GetCarByIdTest()
        {
            // Arrange
            string id = "TN 67 VF 6708";
            string role = "admin";
            
            _carServicesMock.Setup(car => car.GetCarById(id, role)).ReturnsAsync(new CarResponseDTO(){ });
            _carControllerMock = new CarController(_carServicesMock.Object, _cacheMock.Object);
            var mockHttpContext = new Mock<HttpContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "Admin_1"),
                new Claim(ClaimTypes.Role, "admin"),
            }));
            mockHttpContext.Setup(c => c.User).Returns(user);
            _carControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            // Act
            var result = await _carControllerMock.GetCarById(id);

            // Assert
            _carServicesMock.Verify(car => car.GetCarById(id, role), Times.Once);
        }

        [Fact]
        public async Task AddCarTest()
        {
            // Arrange
            string username = "Admin_1";
            string jwtToken = "Token";
            CarDTO carDTO = new CarDTO { Category_Id = 1, Description = "", Img_URL = "", Pre_Owner_Count = 2, Price = 300000, Rc_Details = null, RC_No = "524424", Vehicle_No = "TN 76 GH 4132" };
            Car car = new Car { Availability = true, Category_Id = 1, Created_Date = DateTime.Now, Category = null, Description = "", Img_URL = "", Likes = 0, Pre_Owner_Count = 2, Price = 300000, Rc_Details = null, RC_No = "524424", Updated_Date = DateTime.Now, Vehicle_No = "TN 76 GH 4132" };
            _carServicesMock.Setup(car => car.AddCar(It.IsAny<CarDTO>(), username, jwtToken)).ReturnsAsync(car);
            _carControllerMock = new CarController(_carServicesMock.Object, _cacheMock.Object);

            Mock<HttpContext> mockContext = new Mock<HttpContext>();
            Mock<HttpRequest> mockRequest = new Mock<HttpRequest>();
            var requestHeaders = new HeaderDictionary
            {
                { "Authorization", "Bearer " + jwtToken }
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Name, "Admin_1")
            }));
            mockRequest.Setup(c => c.Headers).Returns(requestHeaders);
            mockContext.Setup(c => c.Request).Returns(mockRequest.Object);
            mockContext.Setup(c => c.User).Returns(user);
            _carControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = mockContext.Object
            };

            // Act
            var result = await _carControllerMock.AddCar(carDTO);

            // Assert
            _carServicesMock.Verify(car => car.AddCar(carDTO, username, jwtToken), Times.Once);
        }

        [Fact]
        public async Task DeleteCarTest()
        {
            // Arrange
            string id = "TN 67 VF 6708";
            _carServicesMock.Setup(car => car.DeleteCar(id));
            _carControllerMock = new CarController(_carServicesMock.Object, _cacheMock.Object);

            // Act
            var result = await _carControllerMock.DeleteCar(id);

            // Assert
            _carServicesMock.Verify(car => car.DeleteCar(id), Times.Once);
        }

        [Fact]
        public async Task UpdateCarTest()
        {
            // Arrange
            string id = "TN 67 VF 6708";
            CarUpdateDTO carUpdateDTO = new CarUpdateDTO { Availability = true, Vehicle_No = "TN 67 VF 6708", Category_Id = 2, Description = "", Img_URL = "", Pre_Owner_Count = 2, Price = 300000, Rc_Details = null, RC_No = "6772882", Updated_Date = DateTime.Now };
            _carServicesMock.Setup(car => car.UpdateCar(id, carUpdateDTO));
            _carControllerMock = new CarController(_carServicesMock.Object, _cacheMock.Object);

            // Act
            var result = await _carControllerMock.UpdateCar(id, carUpdateDTO);

            // Assert
            _carServicesMock.Verify(car => car.UpdateCar(id, carUpdateDTO), Times.Once);
        }

        [Fact]
        public async Task GetAllCarsTest()
        {
            // Arrange
            string role = "admin";

            _carServicesMock.Setup(car => car.GetAllCars(role)).ReturnsAsync(new List<CarResponseDTO>() { new CarResponseDTO() });
            _carControllerMock = new CarController(_carServicesMock.Object, _cacheMock.Object);
            var mockHttpContext = new Mock<HttpContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "Admin_1"),
                new Claim(ClaimTypes.Role, "admin"),
            }));
            mockHttpContext.Setup(c => c.User).Returns(user);
            _carControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            // Act
            var result = await _carControllerMock.GetAllCars();

            // Assert
            _carServicesMock.Verify(car => car.GetAllCars(role), Times.Once);
        }
    }
}

