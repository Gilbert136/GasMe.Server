using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GasMe.Data.Enums;
using GasMe.Data.Models.EntityBase;

namespace GasMe.Data.Models
{
    public class Schedule : ModelBase
    {
        [Key]
        public int id { get; set; }
        public ScheduleBasis? scheduleBasis { get; set; }
        public DateTime? scheduleDate { get; set; }
        public DateTime? deliveryDate { get; set; }
        public DateTime? pickupDate { get; set; }
        public TransactionStatus? transactionStatus { get; set; }
    }

    public enum ScheduleBasis: byte {
        pickUp = 1,
        delivery,
    }
}
