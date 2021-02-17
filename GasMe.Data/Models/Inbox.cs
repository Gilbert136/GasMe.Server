using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GasMe.Data.Enums;
using GasMe.Data.Models.EntityBase;

namespace GasMe.Data.Models
{
    public class Inbox : ModelBase
    {
        [Key]
        public int id { get; set; }

        public string message { get; set; }
    }
}
