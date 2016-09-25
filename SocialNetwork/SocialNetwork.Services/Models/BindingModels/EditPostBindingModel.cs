namespace SocialNetwork.Services.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class EditPostBindingModel
    {
        [MinLength(5)]
        [Required]
        public string Content { get; set; }
    }
}