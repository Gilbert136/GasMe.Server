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
    public interface IValidationBase {
        List<string> errors { get; set; }
    }

    public class ValidationBase : IValidationBase {
        public List<string> errors { get; set; }
    }
}
