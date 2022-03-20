using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webbutveckling_med_.NET___Projekt.Models
{
    public class Dog
    {
        public int DogId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Age { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        public string? Breed { get; set; }

        public string? Pic { get; set; }

        [NotMapped]
        public IFormFile? Upload { get; set; }

        public string? Description { get; set; }

        public DateTime Added { get; set; } = DateTime.Now;

        [Required]
        public bool Reserved { get; set; }

        [Required]
        public bool Adopted { get; set; }    

        public Person? Person { get; set; }

    }
}
