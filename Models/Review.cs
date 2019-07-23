using System;

namespace e_commerce_api.Models
{
    public class Review
    {

        public string UserFullName { get; set; }

        public string UserMail { get; set; }

        public string ReviewContent { get; set; }

        public string CommentTime { get; set; }

        public int NumberOfStars { get; set; }
    }
}
