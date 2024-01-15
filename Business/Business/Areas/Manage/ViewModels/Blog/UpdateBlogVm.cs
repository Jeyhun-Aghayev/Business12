﻿using Business.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Business.Areas.Manage.ViewModels.Blog
{
    public class UpdateBlogVm:BaseAudiTableEntity
    {
        public IFormFile? Image { get; set; }
        public string? imageUrl { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(40)]
        public string Title { get; set; }
        [Required]
        [MinLength(10)]
        public string Description { get; set; }
    }
}
