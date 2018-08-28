using System.ComponentModel.DataAnnotations;

namespace Bookist.Core.Dtos
{
    public class LinkRequestDto
    {
        [Display(Name = "下载链接")]
        [StringLength(200)]
        public string Url { get; set; }

        [Display(Name = "访问代码")]

        [StringLength(50)]
        public string Code { get; set; }

        [Display(Name = "图书格式")]
        public BookFormat Format { get; set; }
    }

    public class LinkResponseDto : LinkRequestDto
    {
        public long Id { get; set; }
    }
}
