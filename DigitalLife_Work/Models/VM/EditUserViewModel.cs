using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLife_Work.Models.VM
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }
}
