using System.ComponentModel.DataAnnotations;

namespace TestTaskPTMK.Models
{
    public class Sex
    {
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(31, ErrorMessage = "Max length is 31")]
        [MinLength(1, ErrorMessage = "Min length is 1")]
        public string Name { get; set; }
    }
}