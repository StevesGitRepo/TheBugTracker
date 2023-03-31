using System.ComponentModel;
using System.Net.Sockets;

namespace TheBugTracker.Models
{
    public class TicketComment
    {
        //Id
        public int Id { get; set; }
        //Comment
        [DisplayName("Comment")]
        public string Comment { get; set; }
        //Created
        [DisplayName("Create Date")]
        public DateTimeOffset Created { get; set; }
        //TicketId
        [DisplayName("Ticket")]
        public int TicketId { get; set; }
        //User Id
        [DisplayName("Team Member")]
        public string UserId { get; set; }

        //Navigation Properties
        public virtual Ticket Ticket { get; set; }
        public virtual BTUser User { get; set; }
    }
}
