namespace SourceControlSystem.Api.Models.SoftwareProjects.RequestModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Common.Constants;
    using Data.Models;
    using Infrastructure.Mapping;

    public class SaveSoftwareProjectRequestModel : IMapTo<SoftwareProject>
    {
        [MaxLength(ValidationConstants.MaxProjectName)]
        [Required]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaxProjectDescription)]
        public string Description { get; set; }

        [DefaultValue(false)]
        public bool IsPrivate { get; set; }
    }
}