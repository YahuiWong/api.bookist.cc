using Anet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Bookist.Core.Dtos
{
    public class BookRequestDto
    {
        [Display(Name = "标题")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(80)]
        public string Title { get; set; }

        [Display(Name = "副标题")]
        [StringLength(100)]
        public string Subtitle { get; set; }

        [Display(Name = "作者")]
        [StringLength(50)]
        public string Author { get; set; }

        [Display(Name = "封面")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(100)]
        public string Cover { get; set; }

        [Display(Name = "发布日期")]
        public DateTime PublishDate { get; set; }

        [Display(Name = "出版商")]
        [StringLength(30)]
        public string Publisher { get; set; }

        [Display(Name = "ISBN")]
        [StringLength(20)]
        public string Isbn { get; set; }

        [Display(Name = "页数")]
        public int Pages { get; set; }

        [Display(Name = "版本")]
        public int Edition { get; set; } = 1;

        [Display(Name = "介绍")]
        [StringLength(1000)]
        public string Intro { get; set; }

        [Display(Name = "目录")]
        [StringLength(2000)]
        public string Toc { get; set; }

        [Display(Name = "标签")]
        [Required(ErrorMessage = "{0}不能为空")]
        public IEnumerable<string> Tags { get; set; } = new List<string>();

        [Display(Name = "下载地址")]
        [Required(ErrorMessage = "{0}不能为空")]
        public IEnumerable<LinkRequestDto> Links { get; set; } = new List<LinkRequestDto>();

        public void Sanitize()
        {
            // 标签检查和去重
            if (Tags == null || Tags.Count() == 0)
                throw new BadRequestException("至少要有一个标签！");
            Tags = Tags.DistinctBy(x => x.ToLower());

            // 过滤空链接
            if (Links.Count() > 0)
                Links = Links.Where(x => !string.IsNullOrWhiteSpace(x.Url));
        }
    }

    public class BookResponseDto : BookRequestDto
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public new IEnumerable<TagResponseDto> Tags { get; set; } = new List<TagResponseDto>();
        public new IEnumerable<LinkResponseDto> Links { get; set; } = new List<LinkResponseDto>();
    }
}
