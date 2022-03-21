using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webbutveckling_med_.NET___Projekt.Models
{
    public class Person
    {
        public int PersonId { get; set; } 

        [Required]
        public string? Firstname { get; set; }

        [Required]
        public string? Lastname { get; set; }

        [Required]
        public string? PhoneNr { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? Description { get; set; }

        public DateTime? Reserved { get; set; } = DateTime.Now;
        public ICollection<Dog>? Dog { get; set; } = new List<Dog>();

        [NotMapped]
        public int DogId { get; set; }
    }
}
