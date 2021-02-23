using System;

namespace GasMe.Service.Options
{
    public class JwtSettings
    {
        public string Secret { get; set; }  
        public TimeSpan TokenLifeTime { get; set; }
    }
}

