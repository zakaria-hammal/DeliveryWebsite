using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class Feedback
    {
        public int FeedbackID{get; set;}

        [StringLength(25)]
        public string? ClientFirstName{get; set;}

        [StringLength(25)]
        public string? ClientLastName{get; set;}

        [StringLength(25)]
        public string? ClientEmail{get; set;}

        [StringLength(10)]
        public string? ClientPhoneNumber{get; set;}

        [Column(TypeName = "ntext")]
        public string? Problem{get; set;}
    }
}