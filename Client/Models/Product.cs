using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models
{
    public class Product
    {
        public int ProductID{get; set;}

        [StringLength(40)]
        public string? ProductName{get; set;}

        [Column(TypeName = "ntext")]
        public string? Description{get; set;}

        public double? Price{get; set;}

        [StringLength(40)]
        public string? CategoryName{get; set;}
    }
}