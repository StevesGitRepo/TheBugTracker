using System.ComponentModel;
using System.Security.Policy;

namespace TheBugTracker.Models
{
    public class TicketHistory
    {
        //Id is the Primary Key of a Ticket, and creates a Foreign Key for other tabkes
        public int Id { get; set; }


        [DisplayName("Ticket")]
        public int TicketId { get; set; }

        [DisplayName("Updated Item")]
        public string Property { get; set; }

        [DisplayName("Previous")]
        public string OldValue { get; set; }

        [DisplayName("Current")]
        public string NewValue { get; set; }

        [DisplayName("Date Modified")]
        public DateTimeOffset Created { get; set; }

        [DisplayName("Description of Change")]
        public string Description { get; set; }

        [DisplayName("Team Member")]
        public string UserId { get; set; }

        //Navigation Property
        public virtual Ticket Ticket { get; set; }
        public virtual BTUser User { get; set; }
    }
}
