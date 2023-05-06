using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TheBugTracker.Models
{
    public class Notification
    {
        //Primary Key
        public int Id { get; set; }

        //Foreign Key
        [DisplayName("Ticket")]
        public int TicketId { get; set; }

        [DisplayName("Title")]
        public string? Title { get; set; }

        [DisplayName("Message")]
        public string? Message { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Name")]
        public DateTimeOffset Created { get; set; }

        [DisplayName("Recipient")]
        public string? RecipientId { get; set; }

        [DisplayName("Sender")]
        public string? SenderId { get; set; }

        [DisplayName("Has been viewed")]
        public bool Viewed { get; set; }

        //Navigation properties

        public virtual Ticket Ticket { get; set; }
        public virtual BTUser Recipient { get; set; }
        public virtual BTUser Sender { get; set; }
    }
}
