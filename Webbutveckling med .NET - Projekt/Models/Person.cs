using System.ComponentModel.DataAnnotations.Schema;

namespace Webbutveckling_med_.NET___Projekt.Models
{
    public class Person
    {
        public int PersonId { get; set; } 
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? PhoneNr { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }

        public string? Description { get; set; }

        public DateTime? Reserved { get; set; } = DateTime.Now;
        public ICollection<Dog>? Dog { get; set; } = new List<Dog>();

        [NotMapped]
        public int DogId { get; set; }
    }
}
