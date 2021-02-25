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

    public static class OrderRoute{
        public const string Gets = ApiRoutesBase.Base + "Order";
        public const string Post = ApiRoutesBase.Base + "Order";
        public const string Get = ApiRoutesBase.Base + "Order/{id}";
    }

    public static class UserRoute{
        public const string Gets = ApiRoutesBase.Base + "User";
    }

    public static class IdentityRoute{
        public const string Auth = ApiRoutesBase.Base + "Identity/auth";

        public const string Register = ApiRoutesBase.Base + "Identity/register";
    }

    public static class InboxRoute{
        public const string Gets = ApiRoutesBase.Base + "Inbox";
    }
}
