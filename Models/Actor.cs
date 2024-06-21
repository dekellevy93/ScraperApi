using System.ComponentModel.DataAnnotations;

namespace ScraperApi.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Rank { get; set; }

        [MaxLength(500)]
        public string Bio { get; set; }

        [MaxLength(100)]
        public string BestMovie { get; set; }
    }
}
