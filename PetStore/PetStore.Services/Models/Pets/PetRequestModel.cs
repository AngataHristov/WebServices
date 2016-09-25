namespace PetStore.Services.Models.Pets
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PetRequestModel //: IValidatableObject
    {
        [MinLength(2)]
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        public string Another { get; set; }

        [Range(18, 120)]
        public int Age { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (this.Age > 18 && this.Name != "Pesho")
        //    {

        //        return new List<ValidationResult>()
        //        {
        //            new ValidationResult("some error")
        //        };
        //    }

        //    return null;
        //}
    }
}