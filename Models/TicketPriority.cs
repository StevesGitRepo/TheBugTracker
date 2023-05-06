using System.ComponentModel;

namespace TheBugTracker.Models
{
    public class TicketPriority
    {
        //Primary Key
        public int Id { get; set; }

        [DisplayName("Priority Name")]
        public string? Name { get; set; }
    }
}
