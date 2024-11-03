using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCars.Models
{
    public class TrMaintenance
    {
        [Key]
        [MaxLength(36)]
        public string Maintenance_id { get; set; }

        public DateTime? maintenance_date { get; set; } // Nullable DateTime

        [MaxLength(4000)]
        public string description { get; set; }

        public decimal? cost { get; set; } // Nullable decimal

        [ForeignKey("MsCar")] 
        [MaxLength(36)]
        public string car_id { get; set; }

        [ForeignKey("MsEmployee")] 
        [MaxLength(36)]
        public string employee_id { get; set; }

        public virtual MsCar MsCar { get; set; }

       

        public virtual MsEmployee MsEmployee { get; set; }
    }
}
