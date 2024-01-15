using Business.Models.Base;

namespace Business.Areas.Manage.ViewModels.Blog
{
    public class GetBlogVm:BaseAudiTableEntity
    {
        public string ImgUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
