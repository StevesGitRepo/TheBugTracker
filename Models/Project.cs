using Microsoft.CodeAnalysis.Options;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheBugTracker.Models
{
    public class Project
    {
        //Primary Key, Entity Framework recognizes "Id" as the primary key
        public int Id { get; set; }

        //CompanyId - Foreign Key
        [DisplayName("Company Id")]
        public int? CompanyId { get; set; }

        //Name
        [Required]
        [StringLength(50)]
        [DisplayName("Project Name")]
        public string? Name { get; set; }

        //Description
        [DisplayName("Description")]
        public string? Description { get; set; }

        //StartDate
        [DisplayName("Start Date")]
        public DateTimeOffset StartDate { get; set; }

        //EndDate
        [DisplayName("End Date")]
        public DateTimeOffset EndDate { get; set; }

        //ProjectPriorityId - Foreign Key
        [DisplayName("Priority")]
        public int ProjectPriorityId { get; set; }

        //Attachment
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile ImageFormFile { get; set; }

        [DisplayName("File Name")]
        public string? ImageFileName { get; set; }
        public byte[] ImageFileData { get; set; }

        [DisplayName("File Extension")]
        public string? ImageContentType { get; set; }

        //Archived
        [DisplayName("Archived")]
        public bool Archived { get; set; }

        //Navigation

        public virtual Company Company { get; set; }

        public virtual ProjectPriority ProjectPriority { get; set; }

        public virtual ICollection<BTUser> Members { get; set; } = new HashSet<BTUser>();

        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }

}
