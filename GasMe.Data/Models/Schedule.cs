using System;
using System.Collections.Generic;
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
        public string alias { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime? scheduleDate { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? Time { get; set; }
        public virtual ICollection<Day> PeridDays { get; set; }
    }

    public enum ScheduleBasis : byte
    {
        pickUp = 1,
        delivery,
    }

    public enum Day : byte
    {
        Sunday = 1,
        Monday,
        Tuesday,
        Wednesday,
        Thurssday,
        Friday,
        Saturday,
    }
}
