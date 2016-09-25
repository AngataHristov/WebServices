namespace SocialNetwork.Services.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddCommentBindingModel
    {
        [MinLength(5)]
        [Required]
        public string Content { get; set; }
    }
}