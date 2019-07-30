using System;
namespace e_commerce_api.Models
{
    public class UserPassword
    {
        public string Email { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
