using System;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Use_Wheels;
using Use_Wheels.Data;
using Use_Wheels.Models;
using Use_Wheels.Models.DTO;
using Use_Wheels.Repository.IRepository;
using Use_Wheels.Services;
using Use_Wheels.Utility.IUtilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace UseWheels.Tests.Services.Tests
{
    public static class Data
    {
        public static OrderDTO orderDTO = new() { Email = "user1@gmail.com", Payment_Type = "cash", Vehicle_No = "TN 61 BC 1265" };
    }

    public class OrderServicesTests
    {
        private Mock<IOrderRepository> _orderRepoMock;
        private Mock<ICarRepository> _carRepoMock;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<IMockAPIUtility> _apiUtilityMock;
        private readonly OrderServices _orderServices;
        private DbContextOptions<ApplicationDbContext> _options;

        public OrderServicesTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingConfig());
            });

            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("TestDb").Options;
            var _dbContextMock = new ApplicationDbContext(dbContextOptions);
            _userManagerMock = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);
            _orderRepoMock = new Mock<IOrderRepository>();
            _carRepoMock = new Mock<ICarRepository>();
            _apiUtilityMock = new Mock<IMockAPIUtility>();
            _orderServices = new OrderServices(_dbContextMock, _orderRepoMock.Object, _carRepoMock.Object, mappingConfig.CreateMapper(), _apiUtilityMock.Object, _userManagerMock.Object);
        }

        [Fact]
        public async Task CreateOrderTest()
        {
            // Arrange
            OrderDTO orderDTO = new() { Payment_Type = "cash", Email = "user1@gmail.com", Vehicle_No = "TV 78 TY 56432" };
            Car carExample = new() { Vehicle_No = "TV 78 TY 56432", Availability = true, Category = null, Category_Id = 1, Created_Date = DateTime.Now, Description = "description", Img_URL = "D://car1.jpg", Likes = 0, Pre_Owner_Count = 2, Price = 90000, Rc_Details = null, RC_No = "902345", Updated_Date = DateTime.Now };
            User userExample = new User() { UserName = "User_1", Role = "customer", Phone_Number = "2626277908", Gender = "female", Last_Name = "Repeat", First_Name = "User", Email = "user1@gmail.com", AccessFailedCount = 0, ConcurrencyStamp = null, Dob = new DateOnly(2000, 12, 15), EmailConfirmed = false, Id = null, isBlacked = false, Last_Login = DateTime.Now, LockoutEnabled = false, LockoutEnd = null, NormalizedEmail = null, NormalizedUserName = null, PasswordHash = null, PhoneNumber = null, PhoneNumberConfirmed = false, SecurityStamp = null, TwoFactorEnabled = false };
            _userManagerMock.Setup(user => user.FindByEmailAsync("user1@gmail.com")).ReturnsAsync(userExample);
            _carRepoMock.Setup(car => car.GetAsync(It.IsAny<Expression<Func<Car, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(carExample);
            
            // Act
            Orders orderResult = await _orderServices.CreateOrder(orderDTO);

            // Assert
            _orderRepoMock.Verify(order => order.CreateAsync(orderResult), Times.Once);
        }

        [Fact]
        public async Task CreateOrderTest_EmailNotExists_Exception()
        {
            // Arrange
            OrderDTO orderDTO = new() { Payment_Type = "cash", Email = "user1@gmail.com", Vehicle_No = "TV 78 TY 56432" };
            
            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _orderServices.CreateOrder(orderDTO));
        }

        [Fact]
        public async Task CreateOrderTest_VehicleNotFound_Exception()
        {
            // Arrange
            OrderDTO orderDTO = new() { Payment_Type = "cash", Email = "user1@gmail.com", Vehicle_No = "TV 78 TY 56432" };
            Car carExample = null;
            User userExample = new User() { UserName = "User_1", Role = "customer", Phone_Number = "2626277908", Gender = "female", Last_Name = "Repeat", First_Name = "User", Email = "user1@gmail.com", AccessFailedCount = 0, ConcurrencyStamp = null, Dob = new DateOnly(2000, 12, 15), EmailConfirmed = false, Id = null, isBlacked = false, Last_Login = DateTime.Now, LockoutEnabled = false, LockoutEnd = null, NormalizedEmail = null, NormalizedUserName = null, PasswordHash = null, PhoneNumber = null, PhoneNumberConfirmed = false, SecurityStamp = null, TwoFactorEnabled = false };
            _userManagerMock.Setup(user => user.FindByEmailAsync("user1@gmail.com")).ReturnsAsync(userExample);
            _carRepoMock.Setup(car => car.GetAsync(It.IsAny<Expression<Func<Car, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(carExample);

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _orderServices.CreateOrder(orderDTO));
        }
    }
}

