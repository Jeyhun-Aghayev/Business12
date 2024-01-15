using AutoMapper;
using Business.Areas.Manage.ViewModels.Blog;
using Business.Models;

namespace Business.Helpers
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<GetBlogVm,Blog>().ReverseMap();
            CreateMap<UpdateBlogVm,Blog>().ReverseMap();
            CreateMap<CreateBlogVm,Blog>().ReverseMap();
        }
    }
}
