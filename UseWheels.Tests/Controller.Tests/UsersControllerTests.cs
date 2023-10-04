using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json.Linq;
using Use_Wheels.Models.DTO;
using Use_Wheels.Repository.IRepository;

namespace UseWheels.Tests
{
    public class UsersControllerTests
    {
        private UsersController _usersControllerMock;
        private readonly Mock<IUserRepository> _userRepoMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public UsersControllerTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var claims = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Name, "Admin_1")
            }));

            _httpContextAccessorMock.Setup(req => req.HttpContext.User).Returns(claims);
        }

        [Fact]
        public async Task LoginTest()
        {
            // Arrange
            LoginRequestDTO loginRequestDTO = new LoginRequestDTO() { UserName = "User_1", Password = "User@1" };
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO() { User = new UserDTO() { }, Token = "SGhsjhdd" };
            _userRepoMock.Setup(user => user.Login(loginRequestDTO)).ReturnsAsync(loginResponseDTO); ;
            _usersControllerMock = new UsersController(_userRepoMock.Object);

            // Act
            await _usersControllerMock.Login(loginRequestDTO);

            // Assert
            _userRepoMock.Verify(user => user.Login(loginRequestDTO), Times.Once);
        }

        [Fact]
        public async Task LoginTest_WrongCredentials_Exception()
        {
            // Arrange
            LoginRequestDTO loginRequestDTO = new LoginRequestDTO() { UserName = "User_1", Password = "User@4" };
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO() { User = null, Token = "" };
            _userRepoMock.Setup(user => user.Login(loginRequestDTO)).ReturnsAsync(loginResponseDTO);
            _usersControllerMock = new UsersController(_userRepoMock.Object);

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _usersControllerMock.Login(loginRequestDTO));
        }

        [Fact]
        public async Task RegisterTest()
        {
            // Arrange
            RegisterationRequestDTO registerationRequestDTO = new RegisterationRequestDTO() { Dob = DateOnly.Parse("2023-12-18"), Email = "user4@gmail.com", First_Name = "user", Gender = "male", Last_Name = "user", Password = "User@4", Phone_Number = "9872727262", Role = "customer", Username = "User_4" };
            UserDTO userDTO = new UserDTO() { Dob = DateOnly.Parse("2023-12-18"), Email = "user4@gmail.com", First_Name = "user", Gender = "male", Last_Name = "user", Phone_Number = "9872727262", Role = "customer", UserName = "User_4" };
            _userRepoMock.Setup(user => user.Register(registerationRequestDTO)).ReturnsAsync(userDTO); ;
            _usersControllerMock = new UsersController(_userRepoMock.Object);

            // Act
            await _usersControllerMock.Register(registerationRequestDTO);

            // Assert
            _userRepoMock.Verify(user => user.Register(registerationRequestDTO), Times.Once);
        }

        [Fact]
        public async Task RegisterTest_InvalidAge_Exception()
        {
            // Arrange
            RegisterationRequestDTO registerationRequestDTO = new RegisterationRequestDTO() { Dob = DateOnly.Parse("2023-12-18"), Email = "user4@gmail.com", First_Name = "user", Gender = "male", Last_Name = "user", Password = "User@4", Phone_Number = "9872727262", Role = "customer", Username = "User_4" };
            UserDTO userDTO = new UserDTO() { Dob = DateOnly.Parse("2023-12-18"), Email = "user4@gmail.com", First_Name = "user", Gender = "male", Last_Name = "user", Phone_Number = "9872727262", Role = "customer", UserName = "User_4" };
            _usersControllerMock = new UsersController(_userRepoMock.Object);

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _usersControllerMock.Register(registerationRequestDTO));
        }

        [Fact]
        public async Task UnblackSellerTest()
        {
            // Arrange
            string username = "Seller_31";
            _userRepoMock.Setup(user => user.UnblackSeller(username)).ReturnsAsync(1);
            _usersControllerMock = new UsersController(_userRepoMock.Object);

            // Act
            await _usersControllerMock.UnblackSeller(username);

            // Assert
            _userRepoMock.Verify(user => user.UnblackSeller(username), Times.Once);
        }

        [Fact]
        public async Task UnblackSellerTest_NotFound_Exception()
        {
            // Arrange
            string username = "Seller_31";
            _userRepoMock.Setup(user => user.UnblackSeller(username)).ReturnsAsync(-1);
            _usersControllerMock = new UsersController(_userRepoMock.Object);

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _usersControllerMock.UnblackSeller(username));
        }

        [Fact]
        public async Task UnblackSellerTest_NotBlacked_Exception()
        {
            // Arrange
            string username = "Seller_31";
            _userRepoMock.Setup(user => user.UnblackSeller(username)).ReturnsAsync(0);
            _usersControllerMock = new UsersController(_userRepoMock.Object);

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _usersControllerMock.UnblackSeller(username));
        }

        [Fact]
        public async Task LogoutTest()
        {
            // Arrange
            string jwtToken = "Token";
            _usersControllerMock = new UsersController(_userRepoMock.Object);
            Mock<HttpContext> mockContext = new Mock<HttpContext>();
            Mock<HttpRequest> mockRequest = new Mock<HttpRequest>();
            var requestHeaders = new HeaderDictionary
            {
                { "Authorization", "Bearer " + jwtToken }
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "Admin_1"),
                new Claim(ClaimTypes.Role, "admin"),
            }));
            mockRequest.Setup(c => c.Headers).Returns(requestHeaders);
            mockContext.Setup(c => c.Request).Returns(mockRequest.Object);
            mockContext.Setup(c => c.User).Returns(user);
            _usersControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = mockContext.Object
            };

            // Act
            await _usersControllerMock.Logout();
        }

        [Fact]
        public async Task LogoutTest_CustomerRole()
        {
            // Arrange
            string jwtToken = "Token";
            _usersControllerMock = new UsersController(_userRepoMock.Object);
            Mock<HttpContext> mockContext = new Mock<HttpContext>();
            Mock<HttpRequest> mockRequest = new Mock<HttpRequest>();
            var requestHeaders = new HeaderDictionary
            {
                { "Authorization", "Bearer " + jwtToken }
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "User_1"),
                new Claim(ClaimTypes.Role, "customer"),
            }));
            mockRequest.Setup(c => c.Headers).Returns(requestHeaders);
            mockContext.Setup(c => c.Request).Returns(mockRequest.Object);
            mockContext.Setup(c => c.User).Returns(user);
            _usersControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = mockContext.Object
            };

            // Act
            await _usersControllerMock.Logout();
        }
    }
}

