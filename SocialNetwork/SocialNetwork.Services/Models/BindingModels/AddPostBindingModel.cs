namespace SocialNetwork.Services.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddPostBindingModel
    {
        [MinLength(5)]
        [Required]
        public string Content { get; set; }

        [Required]
        public string WallOwnerUsername { get; set; }
    }
}