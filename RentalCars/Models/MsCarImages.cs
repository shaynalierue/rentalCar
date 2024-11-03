using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCars.Models
{
    public class MsCarImages
    {
        [Key]
        [MaxLength(36)]
        public string Image_car_id { get; set; }

        [ForeignKey("MsCar")] // Menyatakan hubungan dengan MsCar
        [MaxLength(36)]
        public string Car_id { get; set; }

        [MaxLength(2000)]
        public string image_link { get; set; }

        // Navigasi properti untuk hubungan dengan MsCar
        public virtual MsCar MsCar { get; set; }
    }
}
