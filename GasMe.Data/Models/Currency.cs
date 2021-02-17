using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GasMe.Data.Enums;
using GasMe.Data.Models.EntityBase;

namespace GasMe.Data.Models
{
    public class Currency : ModelBase
    {
        [Key]
        public int id { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? Rate { get; set; }
        public string code { get; set; }
        public string alias { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
