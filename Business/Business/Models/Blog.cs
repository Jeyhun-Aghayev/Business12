using Business.Models.Base;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class Blog:BaseAudiTableEntity
    {
        public string ImgUrl { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(40)]
        public string Title { get; set; }
        [Required]
        [MinLength(10)]
        public string Description { get; set; }
    }
}
