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
        public class ResultBase<T> {
            public bool state { get; set; }
            public T data { get; set; }
    }
}
