using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GasMe.Data.Enums;
using GasMe.Data.Models.EntityBase;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;


namespace GasMe.Data.Models
{
    public class UserBase : ModelBase, ITokenBase, IValidationBase {
        public string userName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string otherName { get; set; }
        public string identityUserId { get; set; }

        [NotMapped]
        public string token { get; set; }

        [NotMapped]
        public string refreshToken { get; set; }

        [NotMapped] 
        public List<string> errors { get; set; }
    }

    public class User : UserBase
    {
        [Key]
        public int id { get; set; }

        [ForeignKey(nameof(identityUserId))]
        public virtual IdentityUser identityUser { get; set; }
    }
}
