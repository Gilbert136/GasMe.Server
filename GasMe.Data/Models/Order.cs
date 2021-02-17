using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GasMe.Data.Enums;
using GasMe.Data.Models.EntityBase;

namespace GasMe.Data.Models
{
    public class Order : ModelBase
    {
        [Key]
        public int id { get; set; }
        public string label { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? price { get; set; }
        public DateTime? deliveryDate { get; set; }
        public DateTime? pickupDate { get; set; }
        public TransactionStatus transactionStatus { get; set; }

        public int? cylinderTypeId { get; set; }
        public int? capacityId { get; set; }
        public int? quantityId { get; set; }
        public int? deliveryDateId { get; set; }
        public int? pickupDateId { get; set; }

        [NotMapped]
        public virtual CylinderType cylinderType { get; set; }

        [NotMapped]
        public virtual Capacity capacity { get; set; }

        [NotMapped]
        public virtual Quantity quantity { get; set; }

        // [NotMapped]
        // public virtual Schedule deliveryDate { get; set; }

        // [NotMapped]
        // public virtual Schedule pickupDate { get; set; }
    }
}
