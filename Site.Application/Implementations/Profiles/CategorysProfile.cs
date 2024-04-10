using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Profiles
{
    internal class CategorysProfile: Profile
    {
        public CategorysProfile()
        {
            #region Category

            CreateMap<Category, CategoryDto>().ReverseMap();

            #endregion


            #region CategoryBrand

            CreateMap<CategoryBrand, CategoryBrandDto>().ReverseMap();

            #endregion
        }
    }
}
