namespace SourceControlSystem.Api.Models.SoftwareProjects.ResponseModels
{
    using Data.Models;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper;
    using Infrastructure.Mapping;

    public class SoftwareProjectDetailsResponseModel : IMapFrom<SoftwareProject>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public int TotalUsers { get; set; }

        //public static Expression<Func<SoftwareProject, SoftwareProjectDetailsResponseModel>> FromModel
        //{
        //    get
        //    {
        //        return p => new SoftwareProjectDetailsResponseModel()
        //        {
        //            Id = p.Id,
        //            Name = p.Name,
        //            CreatedOn = p.CreatedOn,
        //            TotalUsers = p.Users.Count()
        //        };
        //    }
        //}

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration
                .CreateMap<SoftwareProject, SoftwareProjectDetailsResponseModel>()
                .ForMember(m => m.TotalUsers,
                    opt => opt.MapFrom(pr => pr.Users.Count()))
                .ReverseMap();
        }
    }
}