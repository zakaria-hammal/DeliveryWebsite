using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class Order
    {
        public int OrderID{get; set;}

        [StringLength(25)]
        public string? ClientFirstName{get; set;}

        [StringLength(25)]
        public string? ClientLastName{get; set;}

        [StringLength(25)]
        public string? ClientEmail{get; set;}

        [StringLength(10)]
        public string? ClientPhoneNumber{get; set;}

        [Column(TypeName = "ntext")]
        public string? ClientAddress{get; set;}

        public int ProductId{get; set;}

        [StringLength(40)]
        public string? ProductName{get; set;}

        [Column(TypeName = "ntext")]
        public string? Comments{get; set;}

        public bool Completed {get; set;}
    }
}