using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
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

        //Avatar
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile AvatarFormFile { get; set; }

        [DisplayName("Avatar")]
        public string AvatarFileName { get; set; }
        public byte[] AvatarFileData { get; set; }

        [DisplayName("File Extension")]
        public string AvatarContentType { get; set; }

        //Foreign Key - every user must be attached to a company
        public int? CompanyId { get; set; }

        //Navigation
        public virtual Company Company { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
