using System;
using e_commerce_api.Models;

namespace e_commerce_api.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public Address[] Addresses { get; set; }
    }
}
