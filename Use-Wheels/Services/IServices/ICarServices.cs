namespace Use_Wheels.Services.IServices
{
	public interface ICarServices
	{
        // Method to get all cars from DB
        Task<IEnumerable<CarResponseDTO>> GetAllCars(string role);

        // Method to get 1 particular car from DB by ID 
        Task<CarResponseDTO> GetCarById(string vehicle_no, string role);

        // Method to add a new car to DB
        Task<Car> AddCar(CarDTO carDTO, string username, string jwtToken);

        // Method to delete a car from DB
        Task DeleteCar(string vehicle_no);

        // Method to update a car in DB
        Task UpdateCar(string vehicle_no, CarUpdateDTO carUpdateDTO);
    }
}

