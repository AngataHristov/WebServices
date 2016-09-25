namespace SocialNetwork.Services.Models.BindingModels
{
    public class UserSearchBindingModel
    {
        public string Name { get; set; }

        public int? MinAge { get; set; }

        public int? MaxAge { get; set; }

        public string Location { get; set; }
    }
}