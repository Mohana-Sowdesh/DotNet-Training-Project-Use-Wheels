using System;
namespace Use_Wheels.Models.DTO
{
	public class CarResponseDTO
	{
        public string Vehicle_No { get; set; }

        public int Category_Id { get; set; }

        public string Description { get; set; }

        public int Pre_Owner_Count { get; set; }

        public string Img_URL { get; set; }

        public float Price { get; set; }

        public string? TrialedCar { get; set; }

        [ForeignKey("Rc_Details")]
        public string RC_No { get; set; }
        public RC Rc_Details { get; set; }
    }
}

