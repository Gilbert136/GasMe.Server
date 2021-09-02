using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GasMe.Data.Enums;
using GasMe.Data.Models.EntityBase;

namespace GasMe.Data.Models
{
    public class Capacity : ModelBase
    {
        [Key]
        public int id { get; set; }
        public string alias { get; set; }
        public string name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? price { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? value { get; set; }
        public string description { get; set; }



        public int? unitId { get; set; }
        public int? currencyId { get; set; }



        [ForeignKey(nameof(Models.Capacity.unitId))]
        public virtual Unit unit { get; set; }

        [ForeignKey(nameof(Models.Capacity.currencyId))]
        public virtual Currency currency { get; set; }
    }
}
