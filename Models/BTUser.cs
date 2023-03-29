using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheBugTracker.Models
{
    //BTUser extends from IdentityUser data
    public class BTUser : IdentityUser
    {

    //We extended by adding FirstName, LastName, FullName
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //string interpolation, concatination
        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }
    }
}
