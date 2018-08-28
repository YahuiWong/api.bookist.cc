using Anet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookist.Core.Entities
{
    public class Book : EntityAudit
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [Varchar(80)]
        public string Title { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        [Varchar(100)]
        public string Subtitle { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [Varchar(50)]
        public string Author { get; set; }

        /// <summary>
        /// 封面
        /// </summary>
        [Required]
        [Varchar(100)]
        public string Cover { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// 出版商
        /// </summary>
        [Varchar(30)]
        public string Publisher { get; set; }

        /// <summary>
        /// ISBN
        /// </summary>
        [Varchar(20)]
        public string Isbn { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public int Edition { get; set; } = 1;

        /// <summary>
        /// 介绍
        /// </summary>
        [Text]
        public string Intro { get; set; }

        /// <summary>
        /// 目录
        /// </summary>
        [Text]
        public string Toc { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 下载量
        /// </summary>
        public int Downloads { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public BookStatus Status { get; set; }

        /// <summary>
        /// 备用字段1
        /// </summary>
        public int Reserved1 { get; set; }

        /// <summary>
        /// 备用字段2
        /// </summary>
        [Varchar(200)]
        public string Reserved2 { get; set; }

        public ICollection<BookTag> BookTags { get; set; } = new List<BookTag>();
        public ICollection<Link> Links { get; set; } = new List<Link>();
    }
}
