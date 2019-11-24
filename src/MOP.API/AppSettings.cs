using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOP.API
{
    public class AppSettings
    {
        public string SqlConnectionString { get; set; }
        public string JwtSecret { get; set; }
    }
}
