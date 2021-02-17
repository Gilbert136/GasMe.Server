using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GasMe.Data.Enums;
using GasMe.Data.Models.EntityBase;

namespace GasMe.Data.Models
{
    public class CylinderType : ModelBase
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? price { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? currencyRate { get; set; }
        public int? currencyId { get; set; }
        public int? capacityId { get; set; }
        public string description { get; set; }

        [NotMapped]
        public virtual Currency currency { get; set; }

        [NotMapped]
        public virtual Capacity capacity { get; set; }
    }
}
