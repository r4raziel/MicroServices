using System.ComponentModel.DataAnnotations;

namespace PaltformService.Models
{

    public class Platform
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int Name { get; set; }
        [Required]
        public int Publisher { get; set; }
        [Required]
        public int Cost { get; set; }

    }

}