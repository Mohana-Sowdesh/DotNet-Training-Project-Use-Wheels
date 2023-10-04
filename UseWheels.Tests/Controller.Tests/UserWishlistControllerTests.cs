using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Moq;
using Use_Wheels.Services.IServices;
using Use_Wheels.Models.DTO;

namespace UseWheels.Tests
{
	public class UserWishlistControllerTests
	{
        private UserWishlistController _userWishlistControllerMock;
        private Mock<IUserWishlistServices> _userWishlistServicesMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public UserWishlistControllerTests()
		{
			_userWishlistServicesMock = new Mock<IUserWishlistServices>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var claims = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Name, "Admin_1")
            }));

            _httpContextAccessorMock.Setup(req => req.HttpContext.User).Returns(claims);
        }

        [Fact]
		public async Task AddToWishlistTest()
		{
            // Arrange
            string vehicle_number = "TN 67 VF 6708";
            string username = "User_1";
            _userWishlistServicesMock.Setup(wishlist => wishlist.AddToWishlist(vehicle_number, username));
            _userWishlistControllerMock = new UserWishlistController(_userWishlistServicesMock.Object);
            var mockHttpContext = new Mock<HttpContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, "customer"),
                new Claim(ClaimTypes.Name, "User_1")
            }));
            mockHttpContext.Setup(c => c.User).Returns(user);
            _userWishlistControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            // Act
            var result = await _userWishlistControllerMock.AddToWishlist(vehicle_number);

            // Assert
            _userWishlistServicesMock.Verify(wishlist => wishlist.AddToWishlist(vehicle_number, username), Times.Once);
        }

        [Fact]
		public async Task DeleteElementFromWishListTest()
		{
            // Arrange
            string vehicle_number = "TN 67 VF 6708";
            string username = "User_1";
            _userWishlistServicesMock.Setup(wishlist => wishlist.DeleteElementFromWishList(vehicle_number, username));
            _userWishlistControllerMock = new UserWishlistController(_userWishlistServicesMock.Object);
            var mockHttpContext = new Mock<HttpContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, "customer"),
                new Claim(ClaimTypes.Name, "User_1")
            }));
            mockHttpContext.Setup(c => c.User).Returns(user);
            _userWishlistControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            // Act
            var result = await _userWishlistControllerMock.DeleteElementFromWishList(vehicle_number);

            // Assert
            _userWishlistServicesMock.Verify(wishlist => wishlist.DeleteElementFromWishList(vehicle_number, username), Times.Once);
        }

		[Fact]
		public async Task GetWishlistTest()
		{
			// Arrange
			string username = "User_1";
            _userWishlistServicesMock.Setup(wishlist => wishlist.GetWishlist(username));
            _userWishlistControllerMock = new UserWishlistController(_userWishlistServicesMock.Object);
            var mockHttpContext = new Mock<HttpContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, "customer"),
                new Claim(ClaimTypes.Name, "User_1")
            }));
            mockHttpContext.Setup(c => c.User).Returns(user);
            _userWishlistControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            // Act
            var result = await _userWishlistControllerMock.GetWishlist();

            // Assert
            _userWishlistServicesMock.Verify(wishlist => wishlist.GetWishlist(username), Times.Once);
        }

        [Fact]
        public async Task GetWishlistTest_ReturnsListCase()
        {
            // Arrange
            string username = "User_1";
            List<Car> listData = new List<Car>
            {
                new Car() { Availability = true, Category = null, Category_Id = 1, Created_Date = DateTime.Now, Description = "description", Img_URL = "D://car1.jpg", Likes = 2, Pre_Owner_Count = 2, Price = 900000, Rc_Details = null, RC_No = "577878", Updated_Date = DateTime.Now, Vehicle_No = "TY 87 GH 7899" }
            };
            _userWishlistServicesMock.Setup(wishlist => wishlist.GetWishlist(username)).Returns(listData);
            _userWishlistControllerMock = new UserWishlistController(_userWishlistServicesMock.Object);
            var mockHttpContext = new Mock<HttpContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, "customer"),
                new Claim(ClaimTypes.Name, "User_1")
            }));
            mockHttpContext.Setup(c => c.User).Returns(user);
            _userWishlistControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            // Act
            var result = await _userWishlistControllerMock.GetWishlist();

            // Assert
            _userWishlistServicesMock.Verify(wishlist => wishlist.GetWishlist(username), Times.Once);
        }
    }
}

