using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Login.Models
{
    public partial class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required] 
        public int? Age { get; set; }
        [Required] 
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
