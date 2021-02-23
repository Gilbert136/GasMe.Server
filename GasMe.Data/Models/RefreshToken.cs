using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using GasMe.Data.Models.EntityBase;

namespace GasMe.Data.Models
{
    public class RefreshToken : ModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string token { get; set; }
        public string jwtId { get; set; }
        public DateTime? expiryDate { get; set; }
        public bool? used { get; set; }
        public bool? invalidated { get; set; }
        public string identityUserId { get; set; }

        [ForeignKey(nameof(identityUserId))]
        public virtual IdentityUser identityUser { get; set; }
    }
}
