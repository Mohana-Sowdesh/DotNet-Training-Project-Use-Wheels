using System;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Use_Wheels;
using Use_Wheels.Data;
using Use_Wheels.Models.DTO;
using Use_Wheels.Repository.IRepository;
using Use_Wheels.Services;
using Use_Wheels.Utility.IUtilities;

namespace UseWheels.Tests.Services.Tests
{
    public class CarServicesTest
    {
        private Mock<ICarRepository> _dbCarMock;
        private Mock<IMockAPIUtility> _apiUtility;
        private CarServices _carServices;

        public CarServicesTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingConfig());
            });

            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("TestDb").Options;
            var _dbContextMock = new ApplicationDbContext(dbContextOptions);
            _dbCarMock = new Mock<ICarRepository>();
            _apiUtility = new Mock<IMockAPIUtility>();
            _carServices = new CarServices(_dbContextMock, _dbCarMock.Object, mappingConfig.CreateMapper(), _apiUtility.Object);
        }

        [Fact]
        public async Task GetAllCarsTest()
        {
            // Arrange
            string role = "customer";
            IEnumerable<CarResponseDTO> carList = new List<CarResponseDTO>();
            List<Car> listData = new List<Car>
            {
                new Car() { Availability = true, Category = null, Category_Id = 1, Created_Date = DateTime.Now, Description = "description", Img_URL = "D://car1.jpg", Likes = 2, Pre_Owner_Count = 2, Price = 900000, Rc_Details = null, RC_No = "577878", Updated_Date = DateTime.Now, Vehicle_No = "TY 87 GH 7899" }
            };
            List<CarResponseDTO> responseListData = new List<CarResponseDTO>
            {
                new CarResponseDTO() { Category_Id = 1, Description = "description", Img_URL = "D://car1.jpg", Pre_Owner_Count = 2, Price = 900000, Rc_Details = null, RC_No = "577878",Vehicle_No = "TY 87 GH 7899", TrialedCar = null }
            };
            carList = responseListData;
            _dbCarMock.Setup(car => car.GetAllAsync(It.IsAny<Expression<Func<Car, bool>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(listData);

            // Act
            IEnumerable<CarResponseDTO> carResponse = await _carServices.GetAllCars(role);

            // Assert
            Assert.Equivalent(carResponse, carList);
        }

        [Fact]
        public async Task GetAllCarsTest_AdminRole()
        {
            // Arrange
            string role = "admin";
            IEnumerable<CarResponseDTO> carList = new List<CarResponseDTO>();
            List<Car> listData = new List<Car>
            {
                new Car() { Availability = false, Category = null, Category_Id = 1, Created_Date = DateTime.Now, Description = "description", Img_URL = "D://car1.jpg", Likes = 2, Pre_Owner_Count = 2, Price = 900000, Rc_Details = null, RC_No = "577878", Updated_Date = DateTime.Now, Vehicle_No = "TY 87 GH 7899" }
            };
            List<CarResponseDTO> responseListData = new List<CarResponseDTO>
            {
                new CarResponseDTO() { Category_Id = 1, Description = "description", Img_URL = "D://car1.jpg", Pre_Owner_Count = 2, Price = 900000, Rc_Details = null, RC_No = "577878",Vehicle_No = "TY 87 GH 7899", TrialedCar = null }
            };
            carList = responseListData;
            _dbCarMock.Setup(car => car.GetAllAsync(It.IsAny<Expression<Func<Car, bool>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(listData);

            // Act
            IEnumerable<CarResponseDTO> carResponse = await _carServices.GetAllCars(role);

            // Assert
            Assert.Equivalent(carResponse, carList);
        }

        [Fact]
        public async Task GetCarByIdTest()
        {
            // Arrange
            string role = "customer";
            string vehicleNo = "TN 35 YU 8924";
            Car carExample = new Car() { Availability = true, Category = null, Category_Id = 1, Created_Date = DateTime.Now, Description = "description", Img_URL = "D://car1.jpg", Likes = 2, Pre_Owner_Count = 2, Price = 900000, Rc_Details = null, RC_No = "577878", Updated_Date = DateTime.Now, Vehicle_No = "TY 87 GH 7899" };
            CarResponseDTO carResponse = new CarResponseDTO() { Category_Id = 1, Description = "description", Img_URL = "D://car1.jpg", Pre_Owner_Count = 2, Price = 900000, Rc_Details = null, RC_No = "577878", Vehicle_No = "TY 87 GH 7899", TrialedCar = null };
            _dbCarMock.Setup(car => car.GetAsync(It.IsAny<Expression<Func<Car, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(carExample);

            // Act
            CarResponseDTO carResult = await _carServices.GetCarById(vehicleNo, role);

            // Assert
            Assert.Equivalent(carResponse, carResult);
        }

        [Fact]
        public async Task GetCarByIdTest_AdminRole()
        {
            // Arrange
            string role = "admin";
            string vehicleNo = "TN 35 YU 8924";
            Car carExample = new Car() { Availability = true, Category = null, Category_Id = 1, Created_Date = DateTime.Now, Description = "description", Img_URL = "D://car1.jpg", Likes = 2, Pre_Owner_Count = 2, Price = 900000, Rc_Details = null, RC_No = "577878", Updated_Date = DateTime.Now, Vehicle_No = "TY 87 GH 7899" };
            CarResponseDTO carResponse = new CarResponseDTO() { Category_Id = 1, Description = "description", Img_URL = "D://car1.jpg", Pre_Owner_Count = 2, Price = 900000, Rc_Details = null, RC_No = "577878", Vehicle_No = "TY 87 GH 7899", TrialedCar = null };
            _dbCarMock.Setup(car => car.GetAsync(It.IsAny<Expression<Func<Car, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(carExample);

            // Act
            CarResponseDTO carResult = await _carServices.GetCarById(vehicleNo, role);

            // Assert
            Assert.Equivalent(carResponse, carResult);
        }

        [Fact]
        public async Task GetCarByIdTest_ValidationError_Result()
        {
            // Arrange
            string role = "customer";
            string vehicleNo = "TN 35YU 8924";

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _carServices.GetCarById(vehicleNo, role));
        }

        [Fact]
        public async Task AddCarTest()
        {
            // Arrange
            string jwtToken = "Token";
            string username = "User_1";
            RC rc = new RC() { Board_Type = "T board", Vehicle_No = "TN 78 RD 3145", Colour = "Red", Updated_Date = DateTime.Now, Owner_Name = "Owner", Car_Model = "Breeza", Created_Date = DateTime.Now, Date_Of_Reg = new DateOnly(2004, 9, 12), FC_Validity = new DateOnly(2024, 9, 12), Fuel_Type = "Petrol", Insurance_Type = "Third party", Manufactured_Year = 2003, Owner_Address = "Chennai", RC_No = "234567", Reg_Valid_Upto = new DateOnly(2024, 9, 12) };
            CarDTO carDTO = new CarDTO() { Category_Id = 1, Created_Date = DateTime.Now, Description = "description", Img_URL = "D://car1.jpg", Pre_Owner_Count = 2, Price = 900000, Rc_Details = rc, RC_No = "234567", Updated_Date = DateTime.Now, Vehicle_No = "TN 78 RD 3145" };

            // Act
            Car carResult = await _carServices.AddCar(carDTO, username, jwtToken);

            // Assert
            _dbCarMock.Verify(car => car.CreateAsync(carResult), Times.Once);
        }

        [Fact]
        public async Task AddCarTest_AlreadyExists_Exception()
        {
            // Arrange
            string jwtToken = "Token";
            string username = "User_1";
            RC rc = new RC() { Board_Type = "T board", Vehicle_No = "TN 78 RD 3145", Colour = "Red", Updated_Date = DateTime.Now, Owner_Name = "Owner", Car_Model = "Breeza", Created_Date = DateTime.Now, Date_Of_Reg = new DateOnly(2004, 9, 12), FC_Validity = new DateOnly(2024, 9, 12), Fuel_Type = "Petrol", Insurance_Type = "Third party", Manufactured_Year = 2003, Owner_Address = "Chennai", RC_No = "234567", Reg_Valid_Upto = new DateOnly(2024, 9, 12) };
            CarDTO carDTO = new CarDTO() { Category_Id = 1, Created_Date = DateTime.Now, Description = "description", Img_URL = "D://car1.jpg", Pre_Owner_Count = 2, Price = 900000, Rc_Details = rc, RC_No = "234567", Updated_Date = DateTime.Now, Vehicle_No = "TN 78 RD 3145" };
            Car carExample = new Car() { Availability = true, Category = null, Category_Id = 1, Created_Date = DateTime.Now, Description = "description", Img_URL = "D://car1.jpg", Likes = 2, Pre_Owner_Count = 2, Price = 900000, Rc_Details = null, RC_No = "577878", Updated_Date = DateTime.Now, Vehicle_No = "TY 87 GH 7899" };
            _dbCarMock.Setup(car => car.GetAsync(It.IsAny<Expression<Func<Car, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(carExample);

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _carServices.AddCar(carDTO, username, jwtToken));
        }

        [Fact]
        public async Task AddCarTest_DTONull_Exception()
        {
            // Arrange
            string jwtToken = "Token";
            string username = "User_1";
            CarDTO carDTO = null;
            
            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _carServices.AddCar(carDTO, username, jwtToken));
        }

        [Fact]
        public async Task AddCarTest_VehicleNoNotMatch_Exception()
        {
            // Arrange
            string jwtToken = "Token";
            string username = "User_1";
            RC rc = new RC() { Board_Type = "T board", Vehicle_No = "TN 78 RD 3143", Colour = "Red", Updated_Date = DateTime.Now, Owner_Name = "Owner", Car_Model = "Breeza", Created_Date = DateTime.Now, Date_Of_Reg = new DateOnly(2004, 9, 12), FC_Validity = new DateOnly(2024, 9, 12), Fuel_Type = "Petrol", Insurance_Type = "Third party", Manufactured_Year = 2003, Owner_Address = "Chennai", RC_No = "234567", Reg_Valid_Upto = new DateOnly(2024, 9, 12) };
            CarDTO carDTO = new CarDTO() { Category_Id = 1, Created_Date = DateTime.Now, Description = "description", Img_URL = "D://car1.jpg", Pre_Owner_Count = 2, Price = 900000, Rc_Details = rc, RC_No = "234567", Updated_Date = DateTime.Now, Vehicle_No = "TN 78 RD 3145" };

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _carServices.AddCar(carDTO, username, jwtToken));
        }

        [Fact]
        public async Task AddCarTest_DateOfReg_Exception()
        {
            // Arrange
            string jwtToken = "Token";
            string username = "User_1";
            RC rc = new RC() { Board_Type = "T board", Vehicle_No = "TN 78 RD 3143", Colour = "Red", Updated_Date = DateTime.Now, Owner_Name = "Owner", Car_Model = "Breeza", Created_Date = DateTime.Now, Date_Of_Reg = new DateOnly(2024, 9, 12), FC_Validity = new DateOnly(2024, 9, 12), Fuel_Type = "Petrol", Insurance_Type = "Third party", Manufactured_Year = 2003, Owner_Address = "Chennai", RC_No = "234567", Reg_Valid_Upto = new DateOnly(2024, 9, 12) };
            CarDTO carDTO = new CarDTO() { Category_Id = 1, Created_Date = DateTime.Now, Description = "description", Img_URL = "D://car1.jpg", Pre_Owner_Count = 2, Price = 900000, Rc_Details = rc, RC_No = "234567", Updated_Date = DateTime.Now, Vehicle_No = "TN 78 RD 3145" };

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _carServices.AddCar(carDTO, username, jwtToken));
        }

        [Fact]
        public async Task DeleteCarTest()
        {
            // Arrange
            string vehicle_no = "TH 89 YT 6754";
            Car carExample = new Car() { Availability = true, Category = null, Category_Id = 1, Created_Date = DateTime.Now, Description = "description", Img_URL = "D://car1.jpg", Likes = 2, Pre_Owner_Count = 2, Price = 900000, Rc_Details = null, RC_No = "577878", Updated_Date = DateTime.Now, Vehicle_No = "TY 87 GH 7899" };
            _dbCarMock.Setup(car => car.GetAsync(It.IsAny<Expression<Func<Car, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(carExample);

            // Act
            await _carServices.DeleteCar(vehicle_no);

            // Assert
            _dbCarMock.Verify(car => car.RemoveAsync(carExample), Times.Once);
        }

        [Fact]
        public async Task DeleteCar_InvalidVehicle_Exception()
        {
            // Arrange
            string vehicle_no = "TH 89 Y8T 6754";

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _carServices.DeleteCar(vehicle_no));
        }

        [Fact]
        public async Task DeleteCar_VehicleNotFound_Exception()
        {
            // Arrange
            string vehicle_no = "TH 89 YT 6754";
            Car carNull = null;
            _dbCarMock.Setup(car => car.GetAsync(It.IsAny<Expression<Func<Car, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(carNull);

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _carServices.DeleteCar(vehicle_no));
        }

        [Fact]
        public async Task UpdateCarTest()
        {
            // Arrange
            string vehicle_no = "TH 89 YT 6754";
            CarUpdateDTO example = new() { Availability = true, Category_Id = 1, Description = "description", Img_URL = "D://car1.jpg", Pre_Owner_Count = 2, Price = 90000, Rc_Details = null, RC_No = "902345", Updated_Date = DateTime.Now, Vehicle_No = "TH 89 YT 6754" };
            Car carMapped = new() { Vehicle_No = "TH 89 YT 6754", Availability = true, Category = null, Category_Id = 1, Created_Date = DateTime.Now, Description = "description", Img_URL = "D://car1.jpg", Likes = 0, Pre_Owner_Count = 2, Price = 90000, Rc_Details = null, RC_No = "902345", Updated_Date = DateTime.Now };
            _dbCarMock.Setup(car => car.GetAsync(It.IsAny<Expression<Func<Car, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(carMapped);
            _dbCarMock.Setup(car => car.UpdateAsync(It.IsAny<Car>()));

            // Act
            await _carServices.UpdateCar(vehicle_no, example);

            // Assert
            _dbCarMock.Verify(car => car.UpdateAsync(It.IsAny<Car>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCarTest_InvalidVehicleNo_Exception()
        {
            // Arrange
            string vehicle_no = "THU 89 YT 6754";
            CarUpdateDTO example = new() { Availability = true, Category_Id = 1, Description = "description", Img_URL = "D://car1.jpg", Pre_Owner_Count = 2, Price = 90000, Rc_Details = null, RC_No = "902345", Updated_Date = DateTime.Now, Vehicle_No = "TH 89 YT 6754" };

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _carServices.UpdateCar(vehicle_no, example));
        }

        [Fact]
        public async Task UpdateCarTest_VehicleNoNotMatch_Exception()
        {
            // Arrange
            string vehicle_no = "TH 88 YT 6754";
            CarUpdateDTO example = new() { Availability = true, Category_Id = 1, Description = "description", Img_URL = "D://car1.jpg", Pre_Owner_Count = 2, Price = 90000, Rc_Details = null, RC_No = "902345", Updated_Date = DateTime.Now, Vehicle_No = "TH 89 YT 6754" };

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _carServices.UpdateCar(vehicle_no, example));
        }

        [Fact]
        public async Task UpdateCarTest_VehicleNotFound_Exception()
        {
            // Arrange
            string vehicle_no = "TH 89 YT 6754";
            CarUpdateDTO example = new() { Availability = true, Category_Id = 1, Description = "description", Img_URL = "D://car1.jpg", Pre_Owner_Count = 2, Price = 90000, Rc_Details = null, RC_No = "902345", Updated_Date = DateTime.Now, Vehicle_No = "TH 89 YT 6754" };
            Car response = null;
            _dbCarMock.Setup(car => car.GetAsync(It.IsAny<Expression<Func<Car, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(response);

            // Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _carServices.UpdateCar(vehicle_no, example));
        }
    }
}

