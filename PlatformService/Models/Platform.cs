using System.ComponentModel.DataAnnotations;

namespace PaltformService.Models
{

    public class Paltform
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
        public int Publisher { get; set; }
        public int Cost { get; set; }

    }

}