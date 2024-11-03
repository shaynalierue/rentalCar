using System.ComponentModel.DataAnnotations;

namespace RentalCars.Models
{
    public class MsCustomer
    {
        [Key]
        [MaxLength(36)]
        public string Customer_id { get; set; }

        [Required]
        [MaxLength(100)]
        public string email { get; set; }

        [Required]
        [MaxLength(100)]
        public string password { get; set; }

        [Required]
        [MaxLength(200)]
        public string name { get; set; }

        [MaxLength(50)]
        public string phone_number { get; set; }

        [MaxLength(500)]
        public string address { get; set; }

        [MaxLength(100)]
        public string driver_license_number { get; set; }
    }
}
