using Business.Models.Base;
using Microsoft.Build.Framework;

namespace Business.Models
{
    public class Setting:BaseEntity
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
