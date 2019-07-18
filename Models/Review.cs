using System;
namespace e_commerce_api.Models
{
    public class Review
    {


        public string userFullName { get; set; }

        public string userMail { get; set; }

        public string reviewContent { get; set; }

        public string commentTime { get; set; }

        public int numberOfStars { get; set; }
    }
}
