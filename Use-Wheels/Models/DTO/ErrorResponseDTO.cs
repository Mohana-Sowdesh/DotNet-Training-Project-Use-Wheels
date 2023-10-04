using System;
namespace Use_Wheels.Models.DTO
{
	public class ErrorResponseDTO
	{
        public ErrorResponseDTO(int statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}

