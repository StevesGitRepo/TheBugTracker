using Microsoft.CodeAnalysis.Options;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TheBugTracker.Models
{
    public class Project
    {
        //Id
        public int Id { get; set; }

        //CompanyId
        [DisplayName("Company Id")]
        public int CompanyId { get; set; }

        //Name
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }

        //Description
        [DisplayName("Project Description")]
        public string Description { get; set; }

        //StartDate
        [DisplayName("Project Start Date")]
        public DateTimeOffset StartDate { get; set; }

        //EndDate
        [DisplayName("Project End Date")]
        public DateTimeOffset EndDate { get; set; }

        //ProjectPriorityId
        [DisplayName("Project Priority Id")]
        public int ProjectPriorityId { get; set; }

        //Attachment
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile Attachment { get; set; }

        //Archived
        prop

        //Navigation

        public virtual Company Company { get; set; }

        public virtual ProjectPriority ProjectPriority { get; set; }

        public virtual Members Members { get; set; }

        public virtual Tickets Tickets { get; set; }
    }

    //use the TicketAttachment class for example to create Attachment
}
