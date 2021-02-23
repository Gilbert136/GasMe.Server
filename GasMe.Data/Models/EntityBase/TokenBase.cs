using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GasMe.Data.Models;
using GasMe.Data;
using GasMe.Data.Enums;

namespace GasMe.Data.Models.EntityBase
{
    public interface ITokenBase {
        string token { get; set; }
        string refreshToken { get; set; }
    }

    public class TokenBase : ITokenBase {
        public string token { get; set; }

        public string refreshToken { get; set; }
    }
}
