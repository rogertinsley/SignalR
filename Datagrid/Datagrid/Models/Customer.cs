using System.ComponentModel.DataAnnotations;

namespace Datagrid.Models
{
    public class Customer
    {
        [Key]
        [ScaffoldColumn(false)] 
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "House #")]
        public int HouseNumber { get; set; }

        [Display(Name = "Street")]
        public string Street { get; set; }

        [Display(Name = "Town")]
        public string Town { get; set; }

        [Display(Name = "Post Code")]
        public string PostCode { get; set; }
    }
}