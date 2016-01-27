using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace testing.Models.NewsManagement
{
    public class News
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Trololo lalala")]
        public string Caption { get; set; }

        [Required(ErrorMessage ="Trololo")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool IsVisible { get; set; }

        public string AuthorsID { get; set; }

        public News() { }
        
    }
}