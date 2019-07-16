using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce_api.Helpers
{
    public class AppSettings : IAppSettings
    {
        public string Secret { get; set; }
        public string Email { get; set; }
        public string EmailPassword { get; set; }

    }
    public interface IAppSettings
    {
        string Secret { get; set; }
        string Email { get; set; }
        string EmailPassword { get; set; }

    }
}
