using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GasMe.Data.Enums;

namespace GasMe.Data.Models.EntityBase
{
    public class ModelBase
    {
        [NotMapped]
        public string connectionId { get; set; }
        public string createdBy { get; set; }
        public string modifiedBy { get; set; }
        public DateTime? modifiedDate { get; set; }
        public DateTime? createdDate { get; set; }
        public EntityStatus? status { get; set; }
    }
}
