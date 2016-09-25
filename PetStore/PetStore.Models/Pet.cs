namespace PetStore.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Pet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
