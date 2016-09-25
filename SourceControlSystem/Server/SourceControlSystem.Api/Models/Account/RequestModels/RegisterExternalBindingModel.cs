namespace SourceControlSystem.Api.Models.Account.RequestModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterExternalBindingModel
    {
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}