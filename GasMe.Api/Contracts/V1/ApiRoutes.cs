using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using GasMe.Data.Models;
using GasMe.Data;
using GasMe.Data.Enums;

namespace GasMe.Api.Contracts.V1
{
    public static class ApiRoutesBase
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version + "/";
    }
}
