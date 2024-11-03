using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCars.Models
{
    public class TrPayment
    {
        [Key]
        [MaxLength(36)]
        public string Payment_id { get; set; }

        public DateTime? payment_date { get; set; }

        public decimal? amount { get; set; }

        [MaxLength(100)]
        public string payment_method { get; set; }

        [ForeignKey("TrRental")]
        [MaxLength(36)]
        public string rental_id { get; set; }

        public virtual TrRental TrRental { get; set; }
    }
}
