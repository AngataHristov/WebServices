namespace OnlineShop.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using OnlineShop.Models;
    using System.Linq;

    public class AdViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Type { get; set; }

        public DateTime PostedOn { get; set; }

        public UserViewModel Owner { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public static Expression<Func<Ad, AdViewModel>> Create
        {
            get
            {
                return ad => new AdViewModel()
                {
                    Id = ad.Id,
                    Name = ad.Name,
                    Description = ad.Description,
                    Price = ad.Price,
                    PostedOn = ad.PostedOn,
                    Type = ad.Type.Name,
                    Categories = ad.Categories
                        .Select(c => new CategoryViewModel()
                        {
                            Id = c.Id,
                            Name = c.Name
                        }),
                    Owner = new UserViewModel()
                    {
                        Id = ad.Owner.Id,
                        Name = ad.Owner.UserName
                    },
                };
            }
        }
    }
}